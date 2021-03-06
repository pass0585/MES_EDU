USE [KFQS_MES_2021]
GO
/****** Object:  StoredProcedure [dbo].[00MM_PoMake_D1]    Script Date: 2021-06-22 오후 5:40:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		동상현
-- Create date: 2021-06-07
-- Description:	발주 내역 삭제
-- =============================================
CREATE PROCEDURE [dbo].[00MM_PoMake_D1]
	@PLANTCODE  VARCHAR(10),          -- 공장 코드
	@PONO       VARCHAR(30),          -- 발주번호

	@LANG      VARCHAR(10)  = 'KO',  -- 언어
	@RS_CODE   VARCHAR(10)  OUTPUT,	 -- 성공 여부
	@RS_MSG    VARCHAR(200) OUTPUT   -- 성공 관련 메세지
AS
BEGIN
	IF (SELECT ISNULL(INFLAG,'N') 
	      FROM TB_POMake WITH(NOLOCK)
		 WHERE PLANTCODE = @PLANTCODE
		   AND PONO      = @PONO)  = 'Y'
	BEGIN
		SET @RS_CODE = 'E'
		SET @RS_MSG  = '이미 입고된 발주 입니다.'
		RETURN	;
	END

	DELETE TB_POMake
	 WHERE PLANTCODE = @PLANTCODE
	   AND PONO = @PONO;

	SET @RS_CODE = 'S'
	
END
GO
