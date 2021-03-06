USE [KFQS_MES_2021]
GO
/****** Object:  StoredProcedure [dbo].[11PP_WorkerPerProd_S1]    Script Date: 2021-06-22 오후 5:40:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		박상섭
-- Create date: 2021-06-15
-- Description:	생산자 일별 생산실적 조회
-- =============================================
CREATE PROCEDURE [dbo].[11PP_WorkerPerProd_S1]
	  @PLANTCODE	VARCHAR(10)
	, @WORKERCODE	VARCHAR(30)
	, @STARTDATE	VARCHAR(10)
	, @ENDDATE		VARCHAR(10)
	  
	, @LANG			VARCHAR(10) = 'KO'
    , @RS_CODE		VARCHAR(1)	 OUTPUT
    , @RS_MSG		VARCHAR(200) OUTPUT
AS
BEGIN
	SELECT A.PLANTCODE														AS PLANTCODE			-- 공장
		 , DBO.FN_WORKERNAME(A.WORKER)										AS WORKERNAME			-- 작업자
		 , CONVERT(VARCHAR, B.MAKEDATE, 23)									AS MAKEDATE				-- 생산일자
		 , A.WORKCENTERCODE													AS WORKCENTERCODE		-- 작업장
		 , DBO.FN_WORKCENTERNAME(A.WORKCENTERCODE,A.PLANTCODE,'KO')			AS WORKCENTERNAME		-- 작업장 명
		 , B.ITEMCODE														AS ITEMCODE				-- 품목 코드
		 , DBO.FN_ITEMNAME(B.ITEMCODE,B.PLANTCODE,@LANG)					AS ITEMNAME				-- 품목 명
		 , B.PRODQTY														AS PRODQTY				-- 양품수량
		 , B.BADQTY															AS BADQTY				-- 불량수량
		 , B.TOTALQTY														AS TOTALQTY				-- 총생산량
		 , CONVERT(VARCHAR,CASE WHEN ISNULL(B.PRODQTY,0) = 0 THEN 0
					ELSE ROUND((B.BADQTY/B.TOTALQTY*100),2) END) +'%'		AS DEFECTRATE			-- 불량율
		 , CONVERT(VARCHAR(19),B.MAKEDATE,25)								AS MAKETIME				-- 생산일시


													
	  FROM TP_WorkcenterStatus A WITH (NOLOCK)
					LEFT JOIN TP_WorkcenterPerProd B WITH(NOLOCK)
						   ON A.WORKCENTERCODE	= B.WORKCENTERCODE
						  AND A.PLANTCODE		= B.PLANTCODE
							
	 WHERE A.PLANTCODE							LIKE '%' + @PLANTCODE + '%'
	   AND A.WORKER								LIKE '%' + @WORKERCODE  + '%'
	   AND CONVERT(VARCHAR, B.MAKEDATE, 23)		BETWEEN @STARTDATE AND @ENDDATE
	 ORDER BY MAKEDATE, WORKERNAME
END
GO
