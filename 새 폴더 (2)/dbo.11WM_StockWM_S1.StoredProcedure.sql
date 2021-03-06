USE [KFQS_MES_2021]
GO
/****** Object:  StoredProcedure [dbo].[11WM_StockWM_S1]    Script Date: 2021-06-22 오후 5:40:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		박상섭
-- Create date: 2021-06-16
-- Description:	제품재고 관리 및 상차등록 조회
-- =============================================
CREATE PROCEDURE [dbo].[11WM_StockWM_S1]
	 @PLANTCODE			VARCHAR(10)
	,@ITEMCODE			VARCHAR(30)
	,@STARTDATE			VARCHAR(10)
	,@ENDDATE			VARCHAR(10)
	,@LOTNO				VARCHAR(10)
	,@SHIPFLAG			VARCHAR(1)

	,@LANG				VARCHAR(10) = 'KO'
	,@RS_CODE			VARCHAR(1)		OUTPUT
	,@RS_MSG			VARCHAR(200)	OUTPUT
AS
BEGIN
	SELECT CASE WHEN ISNULL(SHIPFLAG,'N') = 'Y' THEN 1
												ELSE 0 END  AS CHK			-- 상차 등록	
		 , A.PLANTCODE										AS PLANTCODE	-- 공장
		 , A.ITEMCODE										AS ITEMCODE		-- 품목코드
		 , B.ITEMNAME										AS ITEMNAME		-- 품목명
		 , A.SHIPFLAG										AS SHIPFLAG		-- 상차여부
		 , A.LOTNO											AS LOTNO		-- LOTNO
		 , A.WHCODE											AS WHCODE		-- 창고번호
		 , A.STOCKQTY										AS STOCKQTY		-- 재고수량
		 , B.BASEUNIT										AS UNITCODE		-- 단위
		 , CONVERT(varchar,A.MAKEDATE,23)					AS INDATE		-- 입고일자
		 , A.MAKEDATE										AS MAKEDATE		-- 등록일시
		 , A.MAKER											AS MAKER		-- 등록자

	  FROM TB_StockWM A WITH(NOLOCK) LEFT JOIN TB_ItemMaster B
									   ON A.PLANTCODE = B.PLANTCODE
									  AND A.ITEMCODE  = B.ITEMCODE
	 WHERE A.PLANTCODE				LIKE '%' +	@PLANTCODE + '%'
	   AND A.ITEMCODE				LIKE '%' +	@ITEMCODE + '%'
	   AND A.MAKEDATE				BETWEEN		@STARTDATE + ' 00:00:00' AND @ENDDATE + ' 23:59:59'
	   AND A.LOTNO					LIKE '%' +	@LOTNO	   + '%'
	   AND ISNULL(A.SHIPFLAG,'')	LIKE '%' +	@SHIPFLAG  + '%'

END
GO
