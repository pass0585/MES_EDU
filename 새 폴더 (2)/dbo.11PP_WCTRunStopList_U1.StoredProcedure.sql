USE [KFQS_MES_2021]
GO
/****** Object:  StoredProcedure [dbo].[11PP_WCTRunStopList_U1]    Script Date: 2021-06-22 오후 5:40:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		박상섭
-- Create date: 2021-06-15
-- Description:	작업장 비가동 현황 및 사유 저장
-- =============================================
CREATE PROCEDURE [dbo].[11PP_WCTRunStopList_U1]
	  @PLANTCODE		VARCHAR(10)
	, @WORKCENTERCODE	VARCHAR(30)
	, @REMARK			VARCHAR(200)
	, @ORDERNO			VARCHAR(30)
	, @EDITOR			VARCHAR(20)
	, @RSSTARTDATE		VARCHAR(19)
	, @RSENDDATE		VARCHAR(19)
	, @RSSEQ			INT
	
	, @LANG				VARCHAR(10) = 'KO'
    , @RS_CODE			VARCHAR(1)	 OUTPUT
    , @RS_MSG			VARCHAR(200) OUTPUT


AS
BEGIN
	-- 사유, 수정자 및 수정일자 수정
	UPDATE TP_WorkcenterStatusRec
	   SET REMARK	= @REMARK
		 , EDITOR	= @EDITOR
		 , EDITDATE	= GETDATE()
	 WHERE PLANTCODE		= @PLANTCODE
	   AND WORKCENTERCODE	= @WORKCENTERCODE
	   AND ORDERNO			= @ORDERNO
	   AND RSSEQ			= @RSSEQ
								 
									
	SET @RS_CODE = 'S'
END
GO
