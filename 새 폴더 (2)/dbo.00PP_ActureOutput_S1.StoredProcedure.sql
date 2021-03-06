USE [KFQS_MES_2021]
GO
/****** Object:  StoredProcedure [dbo].[00PP_ActureOutput_S1]    Script Date: 2021-06-22 오후 5:40:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		동상현
-- Create date: 2021-06-10
-- Description:	생산 대상. 작업지시 조회
-- =============================================
CREATE PROCEDURE [dbo].[00PP_ActureOutput_S1]
	@PLANTCODE      VARCHAR(10)
   ,@WORKCENTERCODE VARCHAR(10)
   ,@STARTDATE      VARCHAR(10)
   ,@ENDDATE        VARCHAR(10)
   ,@ORDERNO        VARCHAR(30)

   ,@LANG           VARCHAR(10) = 'KO'
   ,@RS_CODE	    VARCHAR(1)    OUTPUT
   ,@RS_MSG			VARCHAR(200)  OUTPUT
	
AS
BEGIN
	SELECT A.PLANTCODE                                              AS PLANTCODE      -- 공장   
	      ,A.ORDERNO   		                                        AS ORDERNO		  -- 작업지시 번호
		  ,A.ITEMCODE		                                        AS ITEMCODE	      -- 품목 코드
		  ,A.PLANQTY		                                        AS PLANQTY		  -- 계획수량
		  ,B.PRODQTY                                                AS PRODQTY		  -- 양품수량
		  ,B.BADQTY                                                 AS BADQTY         -- 불량수량
		  ,A.UNITCODE 		                                        AS UNITCODE	      -- 단위
		  ,B.INLOTNO												AS MATLOTNO       -- 투입LOT
		  ,B.COMPONENT		                                        AS COMPONENT	  -- 투입품목
		  ,B.COMPONENTQTY	                                        AS COMPONENTQTY   -- 투입 수량
		  ,B.CUNITCODE		                                        AS CUNITCODE	  -- 투입단위
		  ,A.WORKCENTERCODE	                                        AS WORKCENTERCODE -- 작업장
		  ,B.STATUS			                                        AS WORKSTATUSCODE -- 가동/비가동 상태
		  ,CASE WHEN B.STATUS = 'R' THEN '가동중' ELSE '비가동' END AS WORKSTATUS     -- 가동/비가동 상태
		  ,B.WORKER		                                            AS WORKER         -- 작업자
		  ,DBO.FN_WORKERNAME(B.WORKER)                              AS WORKERNAME     -- 작업자명
		  ,B.ORDSTARTDATE                                           AS STARTDATE      -- 최초 가동 시작 시간
		  ,B.ORDENDDATE                                             AS ENDDATE        -- 작업지시  종료 시간
	  FROM  TB_ProductPlan A WITH(NOLOCK) LEFT JOIN TP_WorkcenterStatus B WITH(NOLOCK)
	                                             ON A.PLANTCODE      = B.PLANTCODE
												AND A.ORDERNO        = B.ORDERNO
												AND A.WORKCENTERCODE = B.WORKCENTERCODE
	 WHERE  A.ORDERFLAG                =  'Y'
	   AND ISNULL(A.ORDERCLOSEFLAG,'') <> 'Y'
	   AND A.PLANTCODE                 LIKE '%' + @PLANTCODE      + '%'
	   AND A.WORKCENTERCODE            LIKE '%' + @WORKCENTERCODE + '%'
	   AND A.ORDERNO				   LIKE '%' + @ORDERNO        + '%'
	   AND A.ORDERDATE				   BETWEEN @STARTDATE AND @ENDDATE
END
GO
