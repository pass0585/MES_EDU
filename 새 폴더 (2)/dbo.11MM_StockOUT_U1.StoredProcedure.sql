USE [KFQS_MES_2021]
GO
/****** Object:  StoredProcedure [dbo].[11MM_StockOUT_U1]    Script Date: 2021-06-22 오후 5:40:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		박상섭
-- Create date: 2021-06-10
-- Description:	자재 생산 출고 관리 저장
-- =============================================
CREATE PROCEDURE [dbo].[11MM_StockOUT_U1]
	   @PLANTCODE		VARCHAR(10)				-- 공장
	 , @MATLOTNO		VARCHAR(20)				-- LOTNO
	 , @ITEMCODE		VARCHAR(20)				-- 품목
	 , @STOCKQTY		FLOAT 
	 , @UnitCode		VARCHAR(5) 
	 , @WhCode			VARCHAR(5) 
	 , @StorageLocCode	VARCHAR(10) 
	 , @WorkerId		VARCHAR(10) 
	   
	 , @LANG			VARCHAR(10) = 'KO'
	 , @RS_CODE			VARCHAR(1)		OUTPUT
	 , @RS_MSG			VARCHAR(200)	OUTPUT

AS
BEGIN
	DECLARE @INOUTSEQ INT
	 SELECT @INOUTSEQ = ISNULL(MAX(INOUTSEQ),0) +1 
	   FROM TB_StockMMrec WITH(NOLOCK)
	  WHERE PlantCode = @PlantCode 
		AND INOUTDATE = GETDATE()

		-- 생산 출고 이력 등록
		INSERT TB_StockMMrec	(PLANTCODE, MATLOTNO,	INOUTCODE,		INOUTQTY,	INOUTDATE,	INOUTWORKER, 
								 INOUTSEQ,	WHCODE,		STORAGELOCCODE, INOUTFLAG,	MAKER,		MAKEDATE) 
					     VALUES (@PLANTCODE, @MATLOTNO,	'20',			@STOCKQTY,		GETDATE(),	@WorkerId, 
								 @INOUTSEQ,	@WhCode,	@StorageLocCode, 'OUT',		@WorkerId,		GETDATE())
		-- 자재 재고 삭제
		DELETE TB_StockMM 
		 WHERE PLANTCODE = @PLANTCODE 
		   AND MATLOTNO = @MATLOTNO

		-- 공정 재고 생성
		INSERT TB_STOCKPP (PLANTCODE,	LOTNO,		ITEMCODE ,	WHCODE, STORAGELOCCODE 
						  ,STOCKQTY,	UNITCODE ,	MAKEDATE,	MAKER )
			       VALUES (@PLANTCODE,	@MATLOTNO,	@ITEMCODE ,	@WhCode, @StorageLocCode 
						  ,@STOCKQTY,	@UnitCode ,	GETDATE(),	@WorkerId )
		-- 공정 창고 입고 이력 추가
	DECLARE @INOUTSEQ_PP INT 
	 SELECT @INOUTSEQ_PP = ISNULL(MAX(INOUTSEQ),0) + 1 
	   FROM TB_StockPPrec WITH(NOLOCK) 
	  WHERE PLANTCODE = @PlantCode 
		AND RECDATE = GETDATE() 
	 INSERT INTO TB_StockPPrec ( PLANTCODE , INOUTSEQ , RECDATE , LOTNO , ITEMCODE, WHCODE
								,STORAGELOCCODE , INOUTFLAG , INOUTQTY , UNITCODE , INOUTCODE , MAKEDATE , MAKER ) 
					    VALUES ( @PLANTCODE , @INOUTSEQ , GETDATE() , @MATLOTNO , @ITEMCODE, @WhCode
								,@StorageLocCode , 'IN' , @STOCKQTY , @UnitCode , '20' , GETDATE() , @WorkerId )
	SET @RS_CODE = 'S'
END
GO
