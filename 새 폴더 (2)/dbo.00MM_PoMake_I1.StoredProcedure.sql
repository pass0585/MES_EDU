USE [KFQS_MES_2021]
GO
/****** Object:  StoredProcedure [dbo].[00MM_PoMake_I1]    Script Date: 2021-06-22 오후 5:40:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		동상현
-- Create date: 2021-06-07
-- Description:	발주 내역 등록
-- =============================================
CREATE PROCEDURE [dbo].[00MM_PoMake_I1]
	 @PLANTCODE  VARCHAR(10)          -- 공장 코드
	,@ITEMCODE   VARCHAR(30)		  -- 품목코드
	,@POQTY      FLOAT                -- 발주수량
	,@UNITCODE   VARCHAR(10)          -- 단위
	,@CUSTCODE   VARCHAR(10)          -- 거래처 코드
	,@MAKER      VARCHAR(20)          -- 발주 등록자

	,@LANG      VARCHAR(10)  = 'KO'   -- 언어
	,@RS_CODE   VARCHAR(10)  OUTPUT   -- 성공 여부
	,@RS_MSG    VARCHAR(200) OUTPUT   -- 성공 관련 메세지
AS
BEGIN
	DECLARE @LI_SEQ    INT
	       ,@LS_PONO   VARCHAR(20)
		   ,@LS_PODATE VARCHAR(10)
		   ,@LD_NOWDATE DATETIME	
	SET @LS_PODATE  = CONVERT(VARCHAR,GETDATE(),23)
	SET @LD_NOWDATE = GETDATE()

	-- 발주 번호 채번
	SELECT @LI_SEQ = COUNT(*) + 1
	  FROM TB_POMake WITH(NOLOCK)
	 WHERE PLANTCODE = @PLANTCODE
	   AND PODATE    = @LS_PODATE
	  
	  SET @LI_SEQ = ISNULL(@LI_SEQ,1)
	
	SET @LS_PONO = 'PO' + REPLACE(CONVERT(VARCHAR,GETDATE(),114),':','')
	              + RIGHT('00000' + CONVERT(VARCHAR,@LI_SEQ),4)

	INSERT INTO TB_POMake (PLANTCODE,  PONO,    ITEMCODE,  POQTY,    UNITCODE,   PODATE,
						   CUSTCODE,   MAKER,   MAKEDATE)
					VALUES(@PLANTCODE, @LS_PONO,@ITEMCODE, @POQTY,   @UNITCODE,  @LS_PODATE,
					       @CUSTCODE,  @MAKER,  @LD_NOWDATE)
	
	SET @RS_CODE = 'S'
END
GO
