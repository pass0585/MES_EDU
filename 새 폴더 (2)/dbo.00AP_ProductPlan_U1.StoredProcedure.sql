USE [KFQS_MES_2021]
GO
/****** Object:  StoredProcedure [dbo].[00AP_ProductPlan_U1]    Script Date: 2021-06-22 오후 5:40:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		동상현
-- Create date: 2021-06-09
-- Description:	생산 계획 별 작업지시 확정 및 취소
-- =============================================
CREATE PROCEDURE [dbo].[00AP_ProductPlan_U1]
	  @PLANTCODE            VARCHAR(10) -- 공장   
	 ,@PLANNO				VARCHAR(20) -- 계획번호
	 ,@ORDERFLAG            VARCHAR(1)  -- 확정, 취소 여부
	 ,@EDITOR				VARCHAR(10) -- 수정자
     
     ,@LANG                 VARCHAR(10)='KO'
     ,@RS_CODE              VARCHAR(1)   OUTPUT
     ,@RS_MSG               VARCHAR(200) OUTPUT
AS
BEGIN
		IF (@ORDERFLAG = 'N') -- 확정 내역을 취소 할 경우
		BEGIN
			UPDATE TB_ProductPlan
			   SET ORDERNO     = NULL
			      ,ORDERDATE   = NULL
				  ,ORDERWORKER = NULL
				  ,ORDERFLAG   = NULL
				  ,EDITDATE    = GETDATE()
				  ,EDITOR      = @EDITOR
			 WHERE PLANTCODE = @PLANTCODE
			   AND PLANNO    = @PLANNO
		END


		ELSE IF (@ORDERFLAG = 'Y')
		BEGIN
		    DECLARE @LS_ORDERNO VARCHAR(20)
			    SET @LS_ORDERNO = 'OR' + CONVERT(VARCHAR,GETDATE(),112) + RIGHT(@PLANNO,4)

			UPDATE TB_ProductPlan
			   SET ORDERFLAG = 'Y'
			      ,ORDERWORKER = @EDITOR
				  ,ORDERDATE   = CONVERT(VARCHAR,GETDATE(),23)
				  ,ORDERNO     = @LS_ORDERNO
				  ,EDITDATE    = GETDATE()
				  ,EDITOR      = @EDITOR
			WHERE PLANTCODE    = @PLANTCODE
			  AND PLANNO       = @PLANNO
		END	
    SET @RS_CODE = 'S'
END
GO
