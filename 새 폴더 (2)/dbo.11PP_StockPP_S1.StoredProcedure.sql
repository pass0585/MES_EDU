USE [KFQS_MES_2021]
GO
/****** Object:  StoredProcedure [dbo].[11PP_StockPP_S1]    Script Date: 2021-06-22 오후 5:40:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		박상섭
-- Create date: 2021-06-10
-- Description:	공정 창고 출고 관리 조회
-- =============================================
CREATE PROCEDURE [dbo].[11PP_StockPP_S1]
(
      @PLANTCODE                VARCHAR(10)                
    , @ITEMTYPE					VARCHAR(30)         
	, @LOTNO					VARCHAR(30)         
	   
	, @LANG                     VARCHAR(10)='KO'
	, @RS_CODE                  VARCHAR(1)   OUTPUT
    , @RS_MSG                   VARCHAR(200) OUTPUT 
)
AS
BEGIN
BEGIN TRY
	BEGIN
		-- 공정 재고 조회
		SELECT 0								AS CHK
			 , A.PLANTCODE						AS PLANTCODE
			 , B.ITEMCODE						AS ITEMCODE
			 , B.ITEMNAME						AS ITEMNAME
			 , B.ITEMTYPE						AS ITEMTYPE
			 , A.MATLOTNO						AS LOTNO
			 , A.WHCODE							AS WHCODE
			 , A.STOCKQTY						AS STOCKQTY
			 , A.UNITCODE 						AS UNITCODE
	
		FROM TB_STOCKMM A WITH(NOLOCK) LEFT JOIN TB_ItemMaster B
											  ON A.PLANTCODE  = B.PLANTCODE
											 AND A.ITEMCODE   = B.ITEMCODE
		WHERE A.PLANTCODE LIKE '%' + @PLANTCODE + '%'
		  AND B.ITEMTYPE LIKE '%' + @ITEMTYPE + '%'
		  AND A.MATLOTNO LIKE '%' + @LOTNO + '%'
		  AND ISNULL(A.STOCKQTY,0)<>0
		
	END
SELECT @RS_CODE = 'S'
     
END TRY                                
BEGIN CATCH
   SELECT @RS_CODE = 'E'
   SELECT @RS_MSG 	= ERROR_MESSAGE()
END CATCH   
		   
END
GO
