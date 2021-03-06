USE [KFQS_MES_2021]
GO
/****** Object:  StoredProcedure [dbo].[00PP_ActureOutput_U3]    Script Date: 2021-06-22 오후 5:40:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		동상현
-- Create date: 2021-06-14
-- Description:	작업지시 종료 등록
-- =============================================
CREATE PROCEDURE [dbo].[00PP_ActureOutput_U3]
    @PLANTCODE	    VARCHAR(10) -- 공장
   ,@WORKCENTERCODE VARCHAR(10) -- 작업장
   ,@ORDERNO        VARCHAR(30) -- 작업지시 번호

   ,@LANG            VARCHAR(10)    ='KO'
   ,@RS_CODE         VARCHAR(1)     OUTPUT
   ,@RS_MSG		     VARCHAR(200)   OUTPUT


AS
BEGIN
	-- 현재 시간 정의
	DECLARE @LD_NOWDATE DATETIME
           ,@LS_NOWDATE VARCHAR(10)
		SET @LD_NOWDATE = GETDATE()  
        SET @LS_NOWDATE = CONVERT(VARCHAR,@LD_NOWDATE,23)

    -- 현재 작업장이 가동 중인지 확인
	IF (SELECT STATUS
	      FROM TP_WorkcenterStatus WITH(NOLOCK)
		 WHERE PLANTCODE      = @PLANTCODE
		   AND WORKCENTERCODE = @WORKCENTERCODE
		   AND ORDERNO        = @ORDERNO) = 'R'
	BEGIN
		SET @RS_CODE = 'E'
		SET @RS_MSG  = '작업장이 가동중입니다.'
		RETURN;
	END

	
	   DECLARE @LS_MATLOTNO VARCHAR(30)
		SELECT @LS_MATLOTNO = INLOTNO 
		  FROM TP_WorkcenterStatus WITH(NOLOCK)
		 WHERE PLANTCODE      = @PLANTCODE
		   AND WORKCENTERCODE = @WORKCENTERCODE	
		   AND ORDERNO	      = @ORDERNO

		-- 투입 되어있는 LOT 가 있는지 확인
		IF (ISNULL(@LS_MATLOTNO,'') <> '') 
		BEGIN
			IF (SELECT COUNT(*) 
				  FROM TB_StockHALB WITH(NOLOCK)
				 WHERE PLANTCODE      = @PLANTCODE
				   AND LOTNO          = @LS_MATLOTNO) <> 0
			BEGIN 
				SET @RS_CODE = 'E'
				SET @RS_CODE = '투입된 LOT 가 존재합니다.'
				RETURN;
			END
		END

	-- 데이터 등록 작업지시 종료 상태 등록
	UPDATE TB_ProductPlan
	   SET ORDERCLOSEFLAG = 'Y'
	 WHERE PLANTCODE      = @PLANTCODE
	   AND WORKCENTERCODE = @WORKCENTERCODE
	   AND ORDERNO        = @ORDERNO

	-- 작업장 상태 내역 삭제
	DELETE TP_WorkcenterStatus
	 WHERE PLANTCODE      = @PLANTCODE
	   AND WORKCENTERCODE = @WORKCENTERCODE
	   AND ORDERNO		  = @ORDERNO

   -- 작업지시별 작업장 가동 이력 업데이트 
   BEGIN
        DECLARE @LI_SEQ INT
		 SELECT @LI_SEQ = ISNULL(MAX(RSSEQ),0)
		   FROM TP_WorkcenterStatusRec WITH(NOLOCK)
	      WHERE PLANTCODE = @PLANTCODE
		    AND ORDERNO	  = @ORDERNO
			AND STATUS    = 'S'

	   UPDATE TP_WorkcenterStatusRec
	      SET RSENDDATE = @LD_NOWDATE
		WHERE PLANTCODE = @PLANTCODE
		  AND ORDERNO   = @ORDERNO
		  AND STATUS    = 'S'
		  AND RSSEQ     = @LI_SEQ
   END
   SET @RS_CODE = 'S'
END
GO
