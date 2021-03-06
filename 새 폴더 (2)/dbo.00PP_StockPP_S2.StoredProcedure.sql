USE [KFQS_MES_2021]
GO
/****** Object:  StoredProcedure [dbo].[00PP_StockPP_S2]    Script Date: 2021-06-22 오후 5:40:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		동상현
-- Create date: 2021-06-16
-- Description:	완제품 바코드 발행 데이터 조회
-- =============================================
CREATE PROCEDURE [dbo].[00PP_StockPP_S2]
	@PLANTCODE VARCHAR(10)  -- 공장
   ,@LOTNO     VARCHAR(30)  -- 완제품 LOTNO

   ,@LANG      VARCHAR(10)  = 'KO'
   ,@RS_CODE   VARCHAR(1)   OUTPUT
   ,@RS_MSG    VARCHAR(200) OUTPUT
AS
BEGIN
	SELECT A.LOTNO										  AS LOTNO
	      ,A.ITEMCODE									  AS ITEMCODE
		  ,B.ITEMNAME									  AS ITEMNAME
		  ,B.ITEMSPEC									  AS ITEMSPEC	
		  ,CONVERT(VARCHAR,A.MAKEDATE,120)				  AS PRODDATE
		  ,CONVERT(VARCHAR,A.STOCKQTY) + ' ' + B.BASEUNIT AS LOTQTY
	  FROM TB_StockPP A WITH(NOLOCK) LEFT JOIN TB_ItemMaster B WITH(NOLOCK)
											ON A.PLANTCODE = B.PLANTCODE
										   AND A.ITEMCODE  = B.ITEMCODE
	 WHERE A.PLANTCODE = @PLANTCODE
	   AND A.LOTNO     = @LOTNO

END
GO
