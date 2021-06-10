#region < HEADER AREA >
// *---------------------------------------------------------------------------------------------*
//   Form ID      : MM_StockOUT
//   Form Name    : 자재 재고관리 
//   Name Space   : KFQS_Form
//   Created Date : 2020/08
//   Made By      : DSH
//   Description  : 
// *---------------------------------------------------------------------------------------------*
#endregion

#region < USING AREA >
using System;
using System.Data;
using DC_POPUP;

using DC00_assm;
using DC00_WinForm;

using Infragistics.Win.UltraWinGrid;
#endregion

namespace KFQS_Form
{
    public partial class MM_StockOUT : DC00_WinForm.BaseMDIChildForm
    {

        #region < MEMBER AREA >
        DataTable rtnDtTemp        = new DataTable(); // 
        UltraGridUtil _GridUtil    = new UltraGridUtil();  //그리드 객체 생성
        Common _Common             = new Common();
        string plantCode           = LoginInfo.PlantCode;

        #endregion


        #region < CONSTRUCTOR >
        public MM_StockOUT()
        {
            InitializeComponent();
        }
        #endregion


        #region < FORM EVENTS >
        private void MM_StockOUT_Load(object sender, EventArgs e)
        {
            #region ▶ GRID ◀
            _GridUtil.InitializeGrid(this.grid1, true, true, false, "", false);
            _GridUtil.InitColumnUltraGrid(grid1, "CHK"              , "선택"      , true, GridColDataType_emu.CheckBox, 120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE"        , "공장"      , true, GridColDataType_emu.VarChar, 120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "MAKEDATE"         , "입고일자"  , true, GridColDataType_emu.DateTime24, 120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE"         , "품목"      , true, GridColDataType_emu.VarChar, 300, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMNAME"         , "품목명"    , true, GridColDataType_emu.VarChar, 100, 120, Infragistics.Win.HAlign.Right, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "MATLOTNO"         , "LOTNO"     , true, GridColDataType_emu.VarChar, 100, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "STOCKQTY"         , "수량"      , true, GridColDataType_emu.Double, 200, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "UNITCODE"         , "단위"      , true, GridColDataType_emu.VarChar, 100, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WHCODE"           , "창고"      , true, GridColDataType_emu.VarChar, 120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "MAKER"            , "입고자"    , true, GridColDataType_emu.VarChar, 120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.SetInitUltraGridBind(grid1);
            #endregion

            #region ▶ COMBOBOX ◀
            rtnDtTemp = _Common.Standard_CODE("PLANTCODE");  // 사업장
            Common.FillComboboxMaster(this.cboPlantCode, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
            UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE", rtnDtTemp, "CODE_ID", "CODE_NAME");

            rtnDtTemp = _Common.Standard_CODE("STORAGELOCCODE", "RELCODE1 = 'WH003'");     // 출고저장위치
            Common.FillComboboxMaster(this.cboStorageLoc, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
                     
            rtnDtTemp = _Common.Standard_CODE("WHCODE", "MINORCODE = 'WH003'");     // 출고 창고
            Common.FillComboboxMaster(this.cboWH, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
           
            rtnDtTemp = _Common.GET_ItemCodeFERT_Code("ROH");     // 품목
            Common.FillComboboxMaster(this.cboItemCode, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");

            #endregion

            #region ▶ ENTER-MOVE ◀
            cboPlantCode.Value = plantCode;
            #endregion
        }
        #endregion


        #region < TOOL BAR AREA >
        public override void DoInquire()
        {
            DoFind();
        }
        private void DoFind()
        {
            DBHelper helper = new DBHelper(false);
            try
            {
                base.DoInquire();
                _GridUtil.Grid_Clear(grid1);
                string sPlantcode       = Convert.ToString(cboPlantCode.Value);
                string sItemCode        = Convert.ToString(cboItemCode.Value);
                string sLotNo           = Convert.ToString(txtLotNo.Text);
                string sInOutStart      = string.Format("{0:yyyy-MM-dd}", dtpStart.Value);
                string sInOutEnd        = string.Format("{0:yyyy-MM-dd}", dtpEnd.Value);

                rtnDtTemp = helper.FillTable("11MM_StockOUT_S1", CommandType.StoredProcedure
                                    , helper.CreateParameter("PLANTCODE",           sPlantcode          , DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("ITEMCODE",            sItemCode           , DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("MATLOTNO",            sLotNo              , DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("STARTDATE",           sInOutStart         , DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("ENDDATE",             sInOutEnd           , DbType.String, ParameterDirection.Input)
                                    );                                                       

               this.ClosePrgForm();
                this.grid1.DataSource = rtnDtTemp;
            }
            catch (Exception ex)
            {
                ShowDialog(ex.ToString(),DialogForm.DialogType.OK);    
            }
            finally
            {
                helper.Close();
            }
        }
        /// <summary>
        /// ToolBar의 신규 버튼 클릭
        /// </summary>
        public override void DoNew()
        {
           
        }
        /// <summary>
        /// ToolBar의 삭제 버튼 Click
        /// </summary>
        public override void DoDelete()
        {
            
        }
        /// <summary>
        /// ToolBar의 저장 버튼 Click
        /// </summary>
        public override void DoSave()
        {
            // 그리드에 표현된 내용을 소스 바인딩에 포함한다.
            this.grid1.UpdateData();

            DataTable dtTemp = new DataTable();
            dtTemp = grid1.chkChange();

            if (dtTemp == null) return;

            DBHelper helper = new DBHelper("", true);
            try
            {
                this.Focus();

                if (this.ShowDialog("등록한 데이터를 저장 하시겠습니까?") == System.Windows.Forms.DialogResult.Cancel)
                {
                    CancelProcess = true;
                    return;
                }

                base.DoSave();

                foreach (DataRow drRow in dtTemp.Rows)
                {

                    #region 수정
                    string CHK = string.Empty;
                    if (Convert.ToString(drRow["CHK"]).ToUpper() == "1") CHK = "Y";
                    else CHK = "N";

                    helper.ExecuteNoneQuery("11MM_StockOUT_U1", CommandType.StoredProcedure
                                          , helper.CreateParameter("PLANTCODE",         drRow["PLANTCODE"].ToString(), DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("MATLOTNO",          drRow["MATLOTNO"].ToString(), DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("ITEMCODE",          drRow["ITEMCODE"].ToString(), DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("WHCODE",            drRow["WHCODE"].ToString(), DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("STORAGELOCCODE",    drRow["STORAGELOCCODE"].ToString(), DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("STOCKQTY",          drRow["STOCKQTY"].ToString(), DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("UNITCODE",          drRow["UNITCODE"].ToString(), DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("MAKEDATE",          drRow["MAKEDATE"].ToString(), DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("CHK", CHK, DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("MAKER", LoginInfo.UserID, DbType.String, ParameterDirection.Input)
                                          );


                    #endregion


                }
                if (helper.RSCODE != "S")
                {
                    this.ClosePrgForm();
                    helper.Rollback();
                    this.ShowDialog(helper.RSMSG, DialogForm.DialogType.OK);
                    return;
                }
                helper.Commit();
                this.ClosePrgForm();
                this.ShowDialog("R00002", DialogForm.DialogType.OK);    // 데이터가 저장 되었습니다.
                DoInquire();
            }
            catch (Exception ex)
            {
                CancelProcess = true;
                helper.Rollback();
                ShowDialog(ex.ToString());
            }
            finally
            {
                helper.Close();
            }
        }
        #endregion

        
    }
}




