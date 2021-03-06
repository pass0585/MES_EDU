USE [KFQS_MES_2021]
GO
/****** Object:  StoredProcedure [dbo].[00PP_ActureOutput_I1]    Script Date: 2021-06-22 오후 5:40:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		동상현
-- Create date: 2021-06-11
-- Description:	생산 을 위한 자재 LOT  투입 등록
-- =============================================
CREATE PROCEDURE [dbo].[00PP_ActureOutput_I1]
	 @PLANTCODE      VARCHAR(10)
	,@ITEMCODE       VARCHAR(30)
	,@LOTNO          VARCHAR(30)
	,@WORKCENTERCODE VARCHAR(10)
	,@ORDERNO        VARCHAR(20)
	,@UNITCODE       VARCHAR(10)
	,@INFLAG         VARCHAR(3)
	,@MAKER			 VARCHAR(20)

	,@LANG            VARCHAR(10)  ='KO'
    ,@RS_CODE         VARCHAR(1)   OUTPUT
    ,@RS_MSG          VARCHAR(200) OUTPUT
AS
BEGIN
	-- 현재 시간 정의
	DECLARE @LD_NOWDATE DATETIME
	       ,@LS_NOWDATE VARCHAR(10)
	   SET @LD_NOWDATE = GETDATE()
	   SET @LS_NOWDATE = CONVERT(VARCHAR,@LD_NOWDATE,23)

	DECLARE @LS_ITEMCODE  VARCHAR(30)
	       ,@LF_STOCKQTY  FLOAT	
		   ,@LS_UNITCODE  VARCHAR(10)
		   ,@INOUTSEQ	  INT	
		   ,@LS_WORKER	  VARCHAR(20)

	-- 공정 투입 등록
	IF (@INFLAG = 'IN')
	BEGIN
		-- 작업자 등록 여부 확인
		IF (ISNULL(@MAKER,'') = '')
		BEGIN
			SET @RS_CODE = 'E'
			SET @RS_MSG  = '작업자를 등록하지 않았습니다.'
			RETURN;
		END	

		-- LOT 존재 여부 확인
	    SELECT @LS_ITEMCODE = ITEMCODE
		      ,@LF_STOCKQTY = STOCKQTY
			  ,@LS_UNITCODE = UNITCODE
		  FROM TB_StockPP WITH(NOLOCK)
		 WHERE PLANTCODE = @PLANTCODE
		   AND LOTNO     = @LOTNO
	   IF (ISNULL(@LS_ITEMCODE,'') = '')
	   BEGIN
			SET @RS_CODE = 'E'
			SET @RS_MSG  = '투입 대상 LOT 가 존재하지 않습니다.'
			RETURN;
	   END

	   IF (SELECT COUNT(*)
	         FROM TB_BomMaster WITH(NOLOCK)
			WHERE PLANTCODE = @PLANTCODE
			  AND ITEMCODE  = @ITEMCODE
			  AND COMPONENT = @LS_ITEMCODE) = 0
		BEGIN
			SET @RS_CODE = 'E'
			SET @RS_MSG  = '선택 작업지시에 투입될 대상 품목이 아닙니다.'
			RETURN;
		END

		-- 작업장 상태 테이블에 현재 투입 LOT 의 정보 등록
		UPDATE TP_WorkcenterStatus
		   SET INLOTNO       = @LOTNO
		      ,COMPONENT     = @LS_ITEMCODE
			  ,COMPONENTQTY  = @LF_STOCKQTY
			  ,CUNITCODE     = @LS_UNITCODE
			  ,EDITDATE      = @LD_NOWDATE
			  ,EDITOR        = @MAKER
		WHERE PLANTCODE      = @PLANTCODE
		  AND WORKCENTERCODE = @WORKCENTERCODE
		  AND ORDERNO		 = @ORDERNO

		-- 공정 재고 삭제
		DELETE TB_StockPP
		 WHERE PLANTCODE = @PLANTCODE
		   AND LOTNO     = @LOTNO
		

		-- 공정 재고 입출고 이력 등록
		SELECT @INOUTSEQ = ISNULL(MAX(INOUTSEQ),0) + 1
		  FROM TB_StockPPrec WITH(NOLOCK)
		 WHERE PLANTCODE = @PLANTCODE
		   AND RECDATE   = @LS_NOWDATE

	   INSERT INTO TB_StockPPrec (PLANTCODE, INOUTSEQ,    RECDATE,     LOTNO, ITEMCODE,     WHCODE,    INOUTCODE,  INOUTFLAG, INOUTQTY,     UNITCODE,    MAKEDATE,    MAKER)
			               VALUES(@PLANTCODE,@INOUTSEQ,   @LS_NOWDATE, @LOTNO,@LS_ITEMCODE, 'WH004',   '30',       'OUT',     @LF_STOCKQTY, @UNITCODE,   @LD_NOWDATE, @LS_WORKER)

		-- 재공 재고 등록
		INSERT INTO TB_StockHALB (PLANTCODE,  LOTNO,  ITEMCODE,    WORKCENTERCODE,  STOCKQTY,      UNITCODE,  MAKEDATE,    MAKER)
		                   VALUES(@PLANTCODE, @LOTNO, @LS_ITEMCODE,@WORKCENTERCODE, @LF_STOCKQTY,  @UNITCODE, @LD_NOWDATE, @LS_WORKER)
		
		
		-- 재공 재고 투입 이력 등록
		-- 일자 별 입고 이력 SEQ 채번
		SELECT @INOUTSEQ = ISNULL(MAX(INOUTSEQ),0) + 1
		  FROM TB_StockHALBrec WITH(NOLOCK)
		 WHERE PLANTCODE = @PlantCode
		   AND RECDATE   = @LS_NOWDATE
		INSERT INTO TB_StockHALBrec (PLANTCODE, INOUTSEQ,  RECDATE,     LOTNO, ITEMCODE,     WORKCENTERCODE,  INOUTCODE, INOUTFLAG, INOUTQTY,     UNITCODE,    MAKEDATE,    MAKER)
		                      VALUES(@PLANTCODE,@INOUTSEQ, @LS_NOWDATE, @LOTNO,@LS_ITEMCODE, @WORKCENTERCODE, '30',      'IN',      @LF_STOCKQTY, @UNITCODE,   @LD_NOWDATE, @LS_WORKER)
			 SELECT @RS_CODE = 'S'

	END
    IF (@INFLAG = 'OUT')
	BEGIN
		-- LOT 존재 여부 확인
      SELECT @LS_ITEMCODE = ITEMCODE 
            ,@LF_STOCKQTY = STOCKQTY
            ,@LS_UNITCODE = UNITCODE
        FROM TB_StockHALB WITH (NOLOCK)
       WHERE PLANTCODE = @PLANTCODE
         AND LOTNO     = @LOTNO


      -- 작업장 상태 테이블에 현재 투입 LOT 의 정보 삭제
      UPDATE TP_WorkcenterStatus
            SET INLOTNO      = NULL
               ,COMPONENT    = NULL
               ,COMPONENTQTY = NULL
               ,CUNITCODE    = NULL
              ,EDITDATE     = @LD_NOWDATE
              ,EDITOR       = @LS_WORKER
         WHERE PLANTCODE      = @PLANTCODE
           AND WORKCENTERCODE = @WORKCENTERCODE
           AND ORDERNO        = @ORDERNO 

	  IF (ISNULL(@LS_ITEMCODE,'') <> '')
	  BEGIN
			-- 재공 재고 삭제
			DELETE TB_StockHALB 
			 WHERE PLANTCODE = @PLANTCODE 
			   AND LOTNO     = @LOTNO
			
			-- 재공 재고 취소 이력 등록
			-- 일자 별 출고 이력 SEQ 채번
			SELECT @INOUTSEQ = ISNULL(MAX(INOUTSEQ),0) + 1
			  FROM TB_StockHALBrec WITH(NOLOCK)
			 WHERE PLANTCODE = @PlantCode
			   AND RECDATE   = @LS_NOWDATE
			
			INSERT INTO TB_StockHALBrec (PLANTCODE, INOUTSEQ,  RECDATE,     LOTNO, ITEMCODE,     WORKCENTERCODE,  INOUTCODE, INOUTFLAG,  INOUTQTY,     UNITCODE,    MAKEDATE,    MAKER)
			                      VALUES(@PLANTCODE,@INOUTSEQ, @LS_NOWDATE, @LOTNO,@LS_ITEMCODE, @WORKCENTERCODE,'35',       'OUT',      @LF_STOCKQTY, @UNITCODE,   @LD_NOWDATE, @LS_WORKER)
			
			
			-- 공정 재고 등록
			INSERT INTO TB_StockPP  (PLANTCODE,  LOTNO,  ITEMCODE,     WHCODE,          STOCKQTY,      UNITCODE,  MAKEDATE,    MAKER)
			                   VALUES(@PLANTCODE, @LOTNO, @LS_ITEMCODE,@WORKCENTERCODE, @LF_STOCKQTY,  @UNITCODE, @LD_NOWDATE, @LS_WORKER)
			
			
			
			
			-- 공정 재고 입고 이력 등록
			-- 일자 별 입고 이력 SEQ 채번
			
			SELECT @INOUTSEQ = ISNULL(MAX(INOUTSEQ),0) + 1
			  FROM TB_StockPPrec WITH(NOLOCK)
			 WHERE PLANTCODE = @PlantCode
			   AND RECDATE   = @LS_NOWDATE
			
			INSERT INTO TB_StockPPrec (PLANTCODE, INOUTSEQ,    RECDATE,     LOTNO, ITEMCODE,     WHCODE,  INOUTFLAG,   INOUTCODE, INOUTQTY,     UNITCODE,    MAKEDATE,    MAKER)
			                    VALUES(@PLANTCODE,@INOUTSEQ,   @LS_NOWDATE, @LOTNO,@LS_ITEMCODE, 'WH003', 'IN',        '35',      @LF_STOCKQTY, @UNITCODE,   @LD_NOWDATE, @LS_WORKER)
			
			
			
		END	
        SET @RS_MSG = '투입 취소를 완료 하였습니다.'
        SELECT @RS_CODE = 'S'
	END
END
GO
