USE [KFQS_MES_2021]
GO
/****** Object:  StoredProcedure [dbo].[11PP_ActureOutput_U1]    Script Date: 2021-06-22 오후 5:40:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		박상섭
-- Create date: 2021-06-11
-- Description:	가동/비가동 등록
-- =============================================
CREATE PROCEDURE [dbo].[11PP_ActureOutput_U1]
	 @PLANTCODE      VARCHAR(10)
	,@WORKCENTERCODE VARCHAR(10)
	,@ORDERNO        VARCHAR(20)
	,@ITEMCODE       VARCHAR(30)
	,@UNITCODE       VARCHAR(10)
	,@STATUS         VARCHAR(1)


	,@LANG            VARCHAR(10)  ='KO'
    ,@RS_CODE         VARCHAR(1)   OUTPUT
    ,@RS_MSG          VARCHAR(200) OUTPUT


AS
BEGIN
	-- 현재 시간 정의
	DECLARE @LD_NOWDATE DATETIME
	       ,@LS_NOWDATE VARCHAR(10)
		   ,@LS_WORKER	VARCHAR(20)
	   SET @LD_NOWDATE = GETDATE()
	   SET @LS_NOWDATE = CONVERT(VARCHAR,@LD_NOWDATE,23)

	-- 작업자 등록 여부 가져오기
	SELECT @LS_WORKER = WORKER
	  FROM TP_WorkcenterStatus WITH(NOLOCK)
	 WHERE PLANTCODE		= @PLANTCODE
	   AND WORKCENTERCODE	= @WORKCENTERCODE
	   AND ORDERNO			= @ORDERNO
	IF(ISNULL(@LS_WORKER,'') = '')
	BEGIN
		SET @RS_CODE	= 'E'
		SET @RS_MSG		= '투입 작업자의 정보가 없습니다.'
		RETURN;
	END

	-- 작업장 가동 여부
	IF(SELECT COUNT(*)
		 FROM TP_WorkcenterStatus WITH(NOLOCK)
		WHERE PLANTCODE			= @PLANTCODE
		  AND WORKCENTERCODE	= @WORKCENTERCODE
		  AND ORDERNO			<> @ORDERNO
		  AND STATUS			= 'R') <> 0
	BEGIN
		SET @RS_CODE	= 'E'
		SET @RS_MSG		= '해당 작업장에 가동이 진행중인 작업지시가 있습니다.'
		RETURN;
	END

	-- 최초 가동 시작 일 경우 가동 시간 등록
	UPDATE TP_WorkcenterStatus	-- 작업장 작업지시 별 상태 테이블
	   SET STATUS = @STATUS
	     , ORDSTARTDATE = CASE WHEN ORDSTARTDATE IS NULL 
							   THEN @LS_NOWDATE ELSE ORDSTARTDATE END
	 WHERE PLANTCODE		= @PLANTCODE
	   AND WORKCENTERCODE	= @WORKCENTERCODE
	   AND ORDERNO			= @ORDERNO

	-- 작업장 별 가동 현황 테이블 TP_WorkcenterStatusRec
	DECLARE @LI_RSSEQ INT
	 SELECT @LI_RSSEQ = ISNULL(MAX(RSSEQ),0)
	   FROM TP_WorkcenterStatusRec WITH(NOLOCK)
	  WHERE PLANTCODE	= @PLANTCODE
	    AND WORKCENTERCODE	= @WORKCENTERCODE
	    AND ORDERNO			= @ORDERNO

	-- 이전 가동 정보 업데이트
	UPDATE TP_WorkcenterStatusRec
	   SET RSENDDATE		= @LD_NOWDATE
	     , EDITDATE		= @LD_NOWDATE
	     , EDITOR			= @LS_WORKER
	 WHERE PLANTCODE		= @PLANTCODE
	   AND WORKCENTERCODE	= @WORKCENTERCODE
	   AND ORDERNO			= @ORDERNO
	   AND RSSEQ			= @LI_RSSEQ

	-- 새로운 가동 상태 인서트
	   SET @LI_RSSEQ = @LI_RSSEQ +1
	   INSERT INTO TP_WorkcenterStatusRec ( PLANTCODE,	WORKCENTERCODE, ORDERNO,	RSSEQ,
											WORKER,		ITEMCODE,		STATUS,		RSSTARTDATE,
											MAKER,      MAKEDATE)
								   VALUES ( @PLANTCODE,	@WORKCENTERCODE,	@ORDERNO,	@LI_RSSEQ,
										    @LS_WORKER,	@ITEMCODE,			@STATUS,	@LD_NOWDATE,
											@LS_WORKER, @LD_NOWDATE)
	   SET @RS_CODE = 'S'

END
GO
