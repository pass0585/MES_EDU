USE [KFQS_MES_2021]
GO
/****** Object:  StoredProcedure [dbo].[00WM_StockWM_U1]    Script Date: 2021-06-22 오후 5:40:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		동상현
-- Create date: 2021-05-07
-- Description:	제품 재고 상차 등록
-- =============================================
CREATE PROCEDURE [dbo].[00WM_StockWM_U1]
   @PLANTCODE VARCHAR(10),  -- 공장
   @LOTNO     VARCHAR(30),  -- LOTNO
   @CARNO     VARCHAR(20),  -- 차량 번호
   @CUSTCODE  VARCHAR(20),  -- 거래처 코드
   @WORKER    VARCHAR(20),  -- 작업자.
   @ITEMCODE  VARCHAR(30),  -- 품목코드
   @SHIPQTY   FLOAT,		-- 상차 수량
   @SHIPNO    VARCHAR(30),  -- 상차번호
   @MAKER	  VARCHAR(20),  -- 등록자 ㅁ

   @LANG      VARCHAR(10) = 'KO',
   @RS_CODE   VARCHAR(1)   OUTPUT,
   @RS_MSG    VARCHAR(200) OUTPUT
AS
BEGIN
	

	-- 현재 시간 정의
	DECLARE @LD_NOWDATE DATETIME
	       ,@LS_NOWDATE VARCHAR(10)
	   SET @LD_NOWDATE = GETDATE()
	   SET @LS_NOWDATE = CONVERT(VARCHAR,@LD_NOWDATE,23)


	-- 등록된 작업자 인지 확인
	IF (SELECT COUNT(*)
	  FROM TB_WorkerList WITH(NOLOCK)
	 WHERE PLANTCODE = @PLANTCODE
	   AND WORKERID  = @WORKER) <> 1
	BEGIN
		 SET @RS_CODE = 'E'
		 SET @RS_MSG  = '등록 된 작업자 정보가 아닙니다.'
		 RETURN;
	END

	-- 거래처 존재 여부 확인
	--IF (SELECT COUNT(*)
	--      FROM TB_CustMaster


	-- 재고 상태에 있는지 확인
	IF (SELECT COUNT(*)
	      FROM TB_StockWM WITH(NOLOCK)
		 WHERE PLANTCODE             = @PLANTCODE
		   AND LOTNO	             = @LOTNO) <> 1
	BEGIN
		 SET @RS_CODE = 'E'
		 SET @RS_MSG  = '제품 창고에 존재하지 않는 LOT 입니다. : '  + @LOTNO
		 RETURN;
	END	 


	-- 이미 상차된 내역인지 확인.
	IF (SELECT COUNT(*)
	      FROM TB_StockWM WITH(NOLOCK)
		 WHERE PLANTCODE             = @PLANTCODE
		   AND LOTNO	             = @LOTNO
		   AND ISNULL(SHIPFLAG,'N')  = 'Y') = 1
	BEGIN
		 SET @RS_CODE = 'E'
		 SET @RS_MSG  = '이미 상차 처리된 LOT 입니다. : '  + @LOTNO
		 RETURN;
	END	 
	
	
	-- 상차 내역 UPDATE
	UPDATE TB_StockWM
	   SET SHIPFLAG  = 'Y'
	      ,EDITDATE  = @LD_NOWDATE
		  ,EDITOR    = @WORKER
	 WHERE PLANTCODE = @PLANTCODE
	   AND LOTNO	 = @LOTNO
    
	-- 상차 이력 테이블에 등록
	-- 상차 번호 채번 
	DECLARE @LS_SHIPNO  VARCHAR(30)
	       ,@LI_SHIPSEQ INT

	IF (ISNULL(@SHIPNO,'') = '')
		BEGIN
		-- 첫행 등록 시 헤더 테이블과 상세 테이블 등록
	    SET @LS_SHIPNO = 'SHIP' 
		                + REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR,GETDATE(),120),'-',''),':',''),' ','')
		
		--  첫행 등록 시  공통 테이블등록
		INSERT INTO TB_SHIPWM (PLANTCODE,   SHIPNO,     CARNO,  SHIPDATE,    CUSTCODE,   WORKER, MAKEDATE,    MAKER	)
					   VALUES (@PLANTCODE,  @LS_SHIPNO, @CARNO, @LS_NOWDATE, @CUSTCODE,  @WORKER,@LD_NOWDATE, @MAKER)
	END

	-- 신규 채번 LOT 또는 기존 LOT 을 @LS_FSHIPNO 에 등록 후 상세 내역 등록.
	DECLARE @LS_FSHIPNO VARCHAR(30)
	SET @LS_FSHIPNO = ISNULL(@LS_SHIPNO,@SHIPNO)

	-- 상차 실적 상세 등록
	SELECT @LI_SHIPSEQ = ISNULL(MAX(SHIPSEQ),0) + 1
	  FROM TB_SHIPWM_B WITH(NOLOCK)
	 WHERE PLANTCODE = @PLANTCODE
	   AND SHIPNO    = @LS_FSHIPNO

	INSERT INTO TB_SHIPWM_B (PLANTCODE,   SHIPNO,      SHIPSEQ,		 LOTNO,   ITEMCODE,   SHIPQTY,   MAKEDATE,     MAKER)
	               VALUES (@PLANTCODE,    @LS_FSHIPNO, @LI_SHIPSEQ,	 @LOTNO,  @ITEMCODE,  @SHIPQTY,  @LD_NOWDATE,  @MAKER)


	
	
	SET @RS_CODE = 'S'
	SET @RS_MSG  = @LS_FSHIPNO;

	
END
GO
