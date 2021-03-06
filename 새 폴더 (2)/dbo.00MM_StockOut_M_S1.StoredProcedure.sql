USE [KFQS_MES_2021]
GO
/****** Object:  StoredProcedure [dbo].[00MM_StockOut_M_S1]    Script Date: 2021-06-22 오후 5:40:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*---------------------------------------------------------------------
    프로시져명: USP_MMStockOut_S1
    개     요 : 생산출고 등록 / 취소 대상 조회
    작성자 명 : 동상현
    작성 일자 : 2020-08-31
    수정자 명 :
    수정 일자 :
    수정 사유 :  
    수정 내용 : 
----------------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[00MM_StockOut_M_S1]
(
      @PLANTCODE                VARCHAR(10)         
	 ,@STARTDATE				VARCHAR(10)         
	, @ENDDATE					VARCHAR(10)         
    , @ITEMCODE					VARCHAR(30)         
	, @LOTNO					VARCHAR(30)         
	   
	, @LANG                     VARCHAR(10)='KO'
	, @RS_CODE                  VARCHAR(1)   OUTPUT
    , @RS_MSG                   VARCHAR(200) OUTPUT 
)
AS
BEGIN
BEGIN TRY

	--생산출고등록 조회
	BEGIN
      	 SELECT 0 AS CHK
		       ,A.PLANTCODE
		       ,A.MATLOTNO
        	   ,CONVERT(VARCHAR, A.MAKEDATE, 23)											            AS MAKEDATE
        	   ,A.ITEMCODE
			   ,DBO.FN_ITEMNAME(A.ITEMCODE,A.PLANTCODE,@LANG)											AS ITEMNAME
        	   ,A.STOCKQTY
        	   ,A.UNITCODE
        	   ,A.WHCODE
			   ,(SELECT CODENAME FROM TB_Standard WHERE MAJORCODE = 'WHCODE' AND MINORCODE = A.WHCODE)  AS    WHNAME
        	   ,DBO.FN_WORKERNAME(A.MAKER) AS MAKER
			   
		   FROM TB_StockMM A 
          WHERE A.PLANTCODE LIKE @PLANTCODE+ '%'
		    AND A.ITEMCODE  LIKE @ITEMCODE + '%'
			AND A.MATLOTNO  LIKE @LOTNO    + '%'
			AND ISNULL(A.STOCKQTY,0)  <> 0 --재고인 경우만 조회
			AND CONVERT(VARCHAR, A.MAKEDATE, 23) BETWEEN @STARTDATE AND @ENDDATE
	END
	 

SELECT @RS_CODE = 'S'
     
END TRY                                
BEGIN CATCH
   SELECT @RS_CODE = 'E'
   SELECT @RS_MSG 	= ERROR_MESSAGE()
END CATCH   
		   
END










GO
