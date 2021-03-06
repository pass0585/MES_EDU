USE [KFQS_MES_2021]
GO
/****** Object:  StoredProcedure [dbo].[00MM_StockOut_M_I1]    Script Date: 2021-06-22 오후 5:40:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*---------------------------------------------------------------------
    프로시져명: MM_StockOut_I1
    개     요 : 생산출고 등록 / 취소
    작성자 명 : 동상현 
    작성 일자 : 2021-03-04
    수정자 명 :
    수정 일자 :
    수정 사유 :  
    수정 내용 : 
----------------------------------------------------------------------*/

CREATE PROCEDURE [dbo].[00MM_StockOut_M_I1]                         
(                               
   @PlantCode      VARCHAR(10)  
  ,@LotNo          VARCHAR(30)
  ,@ItemCode       VARCHAR(30)
  ,@Qty            FLOAT
  ,@UnitCode       VARCHAR(5)
  ,@WhCode         VARCHAR(5)
  ,@StorageLocCode VARCHAR(10)
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
   
   DECLARE @INOUTSEQ INT 

   --생산 출고 이력 순번 채번  
    SELECT @INOUTSEQ = ISNULL(MAX(INOUTSEQ),0) +1 
      FROM TB_StockMMrec WITH(NOLOCK)
     WHERE PlantCode = @PlantCode
	   AND INOUTDATE = @LS_NOWDATE

	--생산출고 등록시
	BEGIN

		-- 자재 출고 이력 생성
		INSERT INTO TB_StockMMrec (PLANTCODE,   MATLOTNO,    INOUTCODE,       INOUTQTY,   INOUTDATE,      INOUTWORKER, 
		                           INOUTSEQ,    WHCODE,      STORAGELOCCODE,  INOUTFLAG,  MAKER,          MAKEDATE)
		                   VALUES (@PLANTCODE,  @LotNo,      '20',            @Qty,       @LS_NOWDATE,    @WorkerId,     
						           @INOUTSEQ,   @WhCode,     @StorageLocCode, 'OUT',      @WorkerId,      @LD_NOWTIME)
 

		 --자재재고 삭제
		 DELETE 
		   FROM TB_StockMM
		  WHERE PLANTCODE = @PlantCode
		    AND MatLotNo  = @LotNo
		    AND ItemCode  = @ItemCode

		--공정재고 생성
		INSERT INTO TB_STOCKPP (PLANTCODE		, LOTNO			, ITEMCODE  	, WHCODE		, STORAGELOCCODE
							   ,STOCKQTY		, NOWQTY		, UNITCODE      , MAKEDATE		, MAKER )
						VALUES (@PlantCode      , @LotNo        , @ItemCode     , @WhCode	    , @StorageLocCode
								,@Qty			, @Qty          , @UnitCode     , @LD_NOWTIME   , @WorkerId )	     

		--공정창고 입고이력 추가 
		-- 일자 별 입고 이력 SEQ 채번 
		SELECT @INOUTSEQ = ISNULL(MAX(INOUTSEQ),0) + 1
		  FROM TB_StockPPrec WITH(NOLOCK)
		 WHERE PLANTCODE = @PlantCode
		   AND RECDATE   = @LS_NOWDATE

		INSERT INTO TB_StockPPrec
		(
			  PLANTCODE		  , INOUTSEQ      , RECDATE   , LOTNO  	    , ITEMCODE	, WHCODE
			, STORAGELOCCODE  , INOUTFLAG	  , INOUTQTY  , UNITCODE    , INOUTCODE 
			, MAKEDATE        , MAKER
		)
		VALUES
		(
			@PlantCode        , @INOUTSEQ     , @LS_NOWDATE   , @LotNo    , @ItemCode   , @WhCode	    
		  , @StorageLocCode , 'IN'          , @Qty	      , @UnitCode , '20' 
		  , @LD_NOWTIME	  , @WorkerId
		)	
		
	END
	 
	SELECT @RS_CODE = 'S'
END TRY                           

BEGIN CATCH

     SELECT  @RS_MSG = ERROR_MESSAGE()
     SELECT  @RS_CODE = 'E'                 
END CATCH    





















GO
