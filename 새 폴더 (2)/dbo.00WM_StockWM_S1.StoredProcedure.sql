USE [KFQS_MES_2021]
GO
/****** Object:  StoredProcedure [dbo].[00WM_StockWM_S1]    Script Date: 2021-06-22 오후 5:40:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		동상현
-- Create date: 2021-05-07
-- Description:	제품 재고 현황 조회
-- =============================================
CREATE PROCEDURE [dbo].[00WM_StockWM_S1]
   @PLANTCODE VARCHAR(10),  -- 공장
   @ITEMCODE  VARCHAR(30),  -- 품목
   @LOTNO     VARCHAR(30),  -- LOTNO
   @STARTDATE VARCHAR(10),  -- 입고 일시 (시작)
   @ENDDATE   VARCHAR(10),  -- 입고 일시 (종료)
   @SHIPFLAG  VARCHAR(1),   -- 상차 여부

   @LANG      VARCHAR(10) = 'KO',
   @RS_CODE   VARCHAR(1)   OUTPUT,
   @RS_MSG    VARCHAR(200) OUTPUT
AS
BEGIN
	SELECT CASE WHEN A.SHIPFLAG = 'Y' THEN 1 ELSE 0 END    AS CHK
		  ,A.PLANTCODE									   AS PLANTCODE
          ,A.ITEMCODE									   AS ITEMCODE
    	  ,B.ITEMNAME									   AS ITEMNAME
		  ,ISNULL(A.SHIPFLAG,'N')                          AS SHIPFLAG
    	  ,A.LOTNO										   AS LOTNO
    	  ,A.WHCODE										   AS WHCODE
    	  ,A.STOCKQTY									   AS STOCKQTY
		  ,B.BASEUNIT									   AS UNITCODE
    	  ,SUBSTRING(CONVERT(VARCHAR,A.MAKEDATE,120),1,10) AS INDATE
    	  ,CONVERT(VARCHAR,A.MAKEDATE,120)  			   AS MAKEDATE
    	  ,A.MAKER										   AS MAKER
      FROM TB_StockWM A WITH(NOLOCK) LEFT JOIN TB_ItemMaster B WITH(NOLOCK)
    										ON A.PLANTCODE = B.PLANTCODE
    									   AND A.ITEMCODE  = B.ITEMCODE
     WHERE A.PLANTCODE             LIKE '%' + @PLANTCODE + '%'
       AND A.ITEMCODE              LIKE '%' + @ITEMCODE  + '%'
       AND A.MAKEDATE              BETWEEN @STARTDATE    + ' 00:00:00' AND @ENDDATE + ' 23:59:59'
       AND A.LOTNO                 LIKE '%'  + @LOTNO    + '%'
	   AND ISNULL(A.SHIPFLAG,'N')  LIKE '%'  + @SHIPFLAG + '%'
END
GO
