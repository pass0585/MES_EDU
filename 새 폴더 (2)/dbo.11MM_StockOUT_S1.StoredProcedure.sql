USE [KFQS_MES_2021]
GO
/****** Object:  StoredProcedure [dbo].[11MM_StockOUT_S1]    Script Date: 2021-06-22 오후 5:40:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		박상섭
-- Create date: 2021-06-10
-- Description:	자재 생산 출고 관리 조회
-- =============================================
CREATE PROCEDURE [dbo].[11MM_StockOUT_S1]
	@PLANTCODE VARCHAR(10),				-- 공장
	@ITEMCODE VARCHAR(20),				-- 품목
	@MATLOTNO VARCHAR(20),				-- LOTNO
	@STARTDATE DATETIME,				-- 출고날자 시작
	@ENDDATE DATETIME					-- 출고날짜 종료

AS
BEGIN
	SELECT 0								AS CHK
		 , A.PLANTCODE						AS PLANTCODE
		 , CONVERT(VARCHAR,A.MAKEDATE,23)	AS MAKEDATE
		 , B.ITEMCODE						AS ITEMCODE
		 , B.ITEMNAME						AS ITEMNAME
		 , A.MATLOTNO						AS MATLOTNO
		 , A.STOCKQTY						AS STOCKQTY
		 , A.UNITCODE 						AS UNITCODE
		 , A.WHCODE							AS WHCODE
		 , DBO.FN_WORKERNAME(A.MAKER)		AS MAKER 
		 
		 

	FROM TB_STOCKMM A WITH(NOLOCK) LEFT JOIN TB_ItemMaster B
										  ON A.PLANTCODE  = B.PLANTCODE
										 AND A.ITEMCODE   = B.ITEMCODE
	                             
	
	WHERE A.PLANTCODE LIKE '%' + @PLANTCODE + '%'
	  AND B.ITEMCODE LIKE '%' + @ITEMCODE + '%'
	  AND A.MATLOTNO LIKE '%' + @MATLOTNO + '%'
	  AND CONVERT(VARCHAR, A.MAKEDATE,23) BETWEEN @STARTDATE AND @ENDDATE
	  AND ISNULL(A.STOCKQTY,0)<>0
	
END
GO
