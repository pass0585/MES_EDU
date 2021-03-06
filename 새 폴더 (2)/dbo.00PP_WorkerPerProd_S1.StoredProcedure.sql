USE [KFQS_MES_2021]
GO
/****** Object:  StoredProcedure [dbo].[00PP_WorkerPerProd_S1]    Script Date: 2021-06-22 오후 5:40:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		동상현
-- Create date: 21-06-10
-- Description:	작업자 일별 생산실적
-- =============================================
CREATE PROCEDURE [dbo].[00PP_WorkerPerProd_S1]
	@PLANTCODE VARCHAR(10)   -- 공장
   ,@WORKER    VARCHAR(20)   -- 작업자
   ,@STARTDATE VARCHAR(10)   -- 시작일자
   ,@ENDDATE   VARCHAR(10)   -- 종료일자

   ,@LANG            VARCHAR(10)='KO'
   ,@RS_CODE         VARCHAR(1)    OUTPUT
   ,@RS_MSG          VARCHAR(200)  OUTPUT   
   
AS
BEGIN
	SELECT A.PLANTCODE															           AS PLANTCODE      -- 공장
          ,DBO.FN_WORKERNAME(A.MAKER)											           AS WORKER         -- 작업자
	      ,A.PRODDATE															           AS PRODDATE		  -- 생산일자
	      ,A.WORKCENTERCODE  													           AS WORKCENTERCODE -- 작업장
		  ,B.WORKCENTERNAME														           AS WORKCENTERNAME -- 작업장명
	      ,A.ITEMCODE															           AS ITEMCODE		  -- 품목
	      ,DBO.FN_ITEMNAME(A.ITEMCODE,A.PLANTCODE,'KO')							           AS ITEMNAME       -- 품명
	      ,ISNULL(A.PRODQTY,0)															   AS PRODQTY        -- 생산수량
	      ,ISNULL(A.BADQTY,0) 															   AS BADQTY         -- 불량수량
	      ,ISNULL(A.TOTALQTY,0)															   AS TOTALQTY       -- 총생산량
		  ,CASE WHEN ISNULL(A.PRODQTY,0)  = 0 THEN '0 %'
		        WHEN ISNULL(A.BADQTY,0)   = 0 THEN '100 %'
				ELSE CONVERT(VARCHAR,ROUND(((A.BADQTY * 100) / A.PRODQTY),1)) + ' %' END   AS ERRORPERCENT   -- 불량율
	      ,CONVERT(VARCHAR,A.MAKEDATE,120)										           AS MAKEDATE       -- 생산 일시
     FROM TP_WorkcenterPerProd A WITH(NOLOCK) LEFT JOIN TB_WorkCenterMaster B WITH(NOLOCK)
												     ON A.PLANTCODE      = B.PLANTCODE
													AND A.WORKCENTERCODE = B.WORKCENTERCODE
   WHERE A.PLANTCODE LIKE '%' + @PLANTCODE + '%'
     AND A.MAKER     LIKE '%' + @WORKER    + '%'
	 AND A.PRODDATE  BETWEEN @STARTDATE AND @ENDDATE
  ORDER BY A.MAKER, A.PRODDATE, A.WORKCENTERCODE, A.MAKEDATE
END
GO
