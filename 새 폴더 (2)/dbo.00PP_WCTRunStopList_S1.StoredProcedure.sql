USE [KFQS_MES_2021]
GO
/****** Object:  StoredProcedure [dbo].[00PP_WCTRunStopList_S1]    Script Date: 2021-06-22 오후 5:40:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/*---------------------------------------------------------------------------------------------*
  PROEDURE ID    : PP_WCTRunStopList_S1
  PROCEDURE NAME : 작업장 기간별 가동/비가동 현황 및 사유 관리 조회
  ALTER DATE     : 2020.08
  MADE BY        : DSH
  DESCRIPTION    : 
  REMARK         : 
*---------------------------------------------------------------------------------------------*
  ALTER DATE     :
  UPDATE BY      :
  REMARK         :
*---------------------------------------------------------------------------------------------*/
CREATE PROCEDURE [dbo].[00PP_WCTRunStopList_S1]
(
      @PLANTCODE       VARCHAR(10)    -- 공장
	 ,@WORKCENTERCODE  VARCHAR(10)    -- 작업장
	 ,@RSSTARTDATE	   VARCHAR(10)    -- 가동시작일자
	 ,@RSENDDATE	   VARCHAR(10)    -- 가동종료일자

     ,@LANG            VARCHAR(10)  ='KO'
     ,@RS_CODE         VARCHAR(1)   OUTPUT
     ,@RS_MSG          VARCHAR(200) OUTPUT
)
AS
BEGIN
    BEGIN TRY
		SELECT A.PLANTCODE		                                        AS PLANTCODE
		      ,A.RSSEQ													AS RSSEQ
		      ,A.WORKCENTERCODE	                                        AS WORKCENTERCODE
			  ,DBO.FN_WORKCENTERNAME(A.WORKCENTERCODE,A.PLANTCODE,'KO') AS WORKCENTERNAME	
			  ,A.ORDERNO			                                    AS ORDERNO
			  ,DBO.FN_WORKERNAME(A.WORKER)			                    AS WORKER
			  ,A.ITEMCODE			                                    AS ITEMCODE
			  ,DBO.FN_ITEMNAME(A.ITEMCODE,A.PLANTCODE,'KO')             AS ITEMNAME
			  ,A.STATUS			                                        AS STATUS
			  ,CASE WHEN A.STATUS = 'R' THEN '가동' ELSE '비가동' END   AS STATUSNAME
			  ,CONVERT(VARCHAR,A.RSSTARTDATE,120)						AS RSSTARTDATE
			  ,CONVERT(VARCHAR,A.RSENDDATE,120)						    AS RSENDDATE
			  ,DATEDIFF(MI,RSSTARTDATE,RSENDDATE)						AS TIMEDIFF
			  ,A.PRODQTY										        AS PRODQTY
			  ,A.BADQTY												    AS BADQTY
			  ,A.REMARK												    AS REMARK
			  ,CONVERT(VARCHAR,A.MAKEDATE,120)						    AS MAKEDATE
			  ,DBO.FN_WORKERNAME(A.MAKER)								AS MAKER
			  ,CONVERT(VARCHAR,A.EDITDATE,120)						    AS EDITDATE
			  ,DBO.FN_WORKERNAME(A.EDITOR)							    AS EDITOR
		  FROM TP_WorkcenterStatusRec A WITH(NOLOCK) 
		 WHERE A.PLANTCODE      LIKE '%' + @PLANTCODE + '%'
		   AND A.WORKCENTERCODE LIKE '%' + @WORKCENTERCODE + '%'
		   AND A.RSSTARTDATE BETWEEN @RSSTARTDATE + ' 00:00:00' AND @RSENDDATE + ' 23:59:59'
      ORDER BY PLANTCODE,ORDERNO,WORKCENTERCODE, RSSTARTDATE  

    END TRY

    BEGIN CATCH
        INSERT INTO ERRORMESSAGE ( NAME, ERROR, ELINE, MESSAGE, DATE )
			SELECT ERROR_PROCEDURE() AS ERRORPROCEDURE
				 , ERROR_NUMBER()    AS ERRORNUMBER
				 , ERROR_LINE()      AS ERRORLINE
				 , ERROR_MESSAGE()   AS ERRORMESSAGE
				 , GETDATE()
		
		SELECT @RS_CODE = 'E'
		SELECT @RS_MSG = ERROR_MESSAGE()
    END CATCH
END





GO
