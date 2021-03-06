USE [KFQS_MES_2021]
GO
/****** Object:  StoredProcedure [dbo].[11PP_WCTRunStopList_S1]    Script Date: 2021-06-22 오후 5:40:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		박상섭
-- Create date: 2021-06-15
-- Description:	작업장 비가동 현황 및 사유 조회
-- =============================================
CREATE PROCEDURE [dbo].[11PP_WCTRunStopList_S1]
	  @PLANTCODE		VARCHAR(10)
	, @WORKCENTERCODE	VARCHAR(30)
	, @STARTDATE		VARCHAR(10)
	, @ENDDATE			VARCHAR(10)
	  
	, @LANG				VARCHAR(10) = 'KO'
    , @RS_CODE			VARCHAR(1)	 OUTPUT
    , @RS_MSG			VARCHAR(200) OUTPUT
AS
BEGIN
	SELECT PLANTCODE												AS PLANTCODE			-- 공장
		 , WORKCENTERCODE											AS WORKCENTERCODE		-- 작업장
		 , DBO.FN_WORKCENTERNAME(WORKCENTERCODE,PLANTCODE,'KO')		AS WORKCENTERNAME		-- 작업장 명
		 , RSSEQ													AS RSSEQ				-- RSSEQ
		 , ORDERNO													AS ORDERNO				-- 작업지시번호
		 , ITEMCODE													AS ITEMCODE				-- 품목 코드
		 , DBO.FN_ITEMNAME(ITEMCODE,PLANTCODE,'KO')					AS ITEMNAME				-- 품목 명
		 , DBO.FN_WORKERNAME(WORKER)								AS WORKERNAME			-- 작업자
		 , CASE WHEN STATUS = 'R' THEN '가동' ELSE '비가동' END		AS WORKSTATUS			-- 가동/비가동 상태
		 , CONVERT(VARCHAR(19), RSSTARTDATE, 25)					AS RSSTARTDATE			-- 시작일시
		 , CONVERT(VARCHAR(19), RSENDDATE, 25)						AS RSENDDATE			-- 종료일시
		 , DATEDIFF(MI,RSSTARTDATE,RSENDDATE)						AS RUNTIME				-- 소요시간(분)
		 , PRODQTY													AS PRODQTY				-- 양품수량
		 , BADQTY													AS BADQTY				-- 불량수량
		 , REMARK													AS REMARK				-- 사유
		 , DBO.FN_WORKERNAME(MAKER)									AS MAKER				-- 등록자
		 , MAKEDATE													AS MAKEDATE				-- 등록일시
		 , DBO.FN_WORKERNAME(EDITOR)								AS EDITOR				-- 수정자
		 , EDITDATE													AS EDITDATE				-- 수정일시


													
	  FROM TP_WorkcenterStatusRec

	 WHERE PLANTCODE							LIKE '%' + @PLANTCODE + '%'
	   AND WORKCENTERCODE						LIKE '%' + @WORKCENTERCODE  + '%'
	   AND CONVERT(VARCHAR, RSSTARTDATE, 23)	BETWEEN @STARTDATE AND @ENDDATE
	   ORDER BY PLANTCODE,ORDERNO,WORKCENTERCODE, RSSTARTDATE
END
GO
