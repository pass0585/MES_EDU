USE [KFQS_MES_2021]
GO
/****** Object:  StoredProcedure [dbo].[11PP_StockPP_U1]    Script Date: 2021-06-22 오후 5:40:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		박상섭
-- Create date: 2021-06-10
-- Description:	공정 창고 재고 관리 저장
-- =============================================
CREATE PROCEDURE [dbo].[11PP_StockPP_U1]
	(                               
   @PlantCode      VARCHAR(10)  
  ,@LotNo          VARCHAR(30)
  ,@ItemCode       VARCHAR(30)
  ,@Qty            FLOAT
  ,@UnitCode       VARCHAR(5)
  ,@WorkerId       VARCHAR(10) 

  ,@Lang	       VARCHAR(10)='KO'
  ,@RS_CODE        VARCHAR(1)    OUTPUT
  ,@RS_MSG         VARCHAR(200)  OUTPUT

)                                  
AS                                 
BEGIN TRY    


	--현재시간 정의
    DECLARE @LD_NOWTIME DATETIME
	       ,@LS_NOWDATE VARCHAR(10)
	    SET @LD_NOWTIME = GETDATE()
		SET @LS_NOWDATE = CONVERT(VARCHAR,GETDATE(),23)
   

	--생산출고 취소시
	BEGIN
		-- 공정 재고 삭제
	DELETE 
		   FROM TB_StockPP
		  WHERE PLANTCODE = @PlantCode
		    AND LotNo  = @LotNo

		-- 공정 재고 출고 이력 생성
		DECLARE @INOUTSEQ_PP INT
		SELECT @INOUTSEQ_PP = ISNULL(MAX(INOUTSEQ),0)+1
		  FROM TB_StockPPrec WITH(NOLOCK)
		 WHERE PLANTCODE = @PlantCode
		   AND RECDATE = @LS_NOWDATE
		INSERT INTO TB_StockPPrec
		(
			  PLANTCODE		  , INOUTSEQ      , RECDATE		, LOTNO  	  , ITEMCODE	, WHCODE
			, INOUTFLAG		  , INOUTQTY	  , UNITCODE    , INOUTCODE 
			, MAKEDATE        , MAKER
		)
		VALUES
		(
			  @PlantCode      , @INOUTSEQ_PP   , @LS_NOWDATE	, @LotNo    , @ItemCode   , 'WH003'	    
			, 'OUT'           , @Qty	       , @UnitCode		, '25' 
			, @LD_NOWTIME	  , @WorkerId
		)


		 
    
				-- 채번
		DECLARE @INOUTSEQ INT
		 SELECT @INOUTSEQ = ISNULL(MAX(INOUTSEQ),0)+1
		   FROM TB_StockMMrec WITH(NOLOCK)
		  WHERE PLANTCODE = @PlantCode
		    AND INOUTDATE = @LS_NOWDATE
				-- 자재 입고 이력 등록
		 INSERT TB_StockMMrec (PLANTCODE,   MATLOTNO,    INOUTCODE,       INOUTQTY,   INOUTDATE,      INOUTWORKER,
							   INOUTSEQ,    WHCODE,      INOUTFLAG,  MAKER,          MAKEDATE)
					  VALUES  (@PLANTCODE,  @LotNo,      '25',            @Qty,       @LS_NOWDATE,    @WorkerId,  
							   @INOUTSEQ,   'WH001',     'IN',      @WorkerId,      @LD_NOWTIME)
			    --자재창고 재고 등록
		 INSERT TB_StockMM	(PLANTCODE		, MATLOTNO		, WHCODE  		, ITEMCODE
							,STOCKQTY		, MAKER			, MAKEDATE)
					 VALUES (@PlantCode		, @LOTNO		, 'WH001'  		, @ItemCode
							,@QTY			, @WorkerId		, @LD_NOWTIME)
	END
	 
	SELECT @RS_CODE = 'S'
END TRY                           

BEGIN CATCH

     SELECT  @RS_MSG = ERROR_MESSAGE()
     SELECT @RS_CODE = 'E'                 
END CATCH		
GO
