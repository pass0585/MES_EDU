#region < HEADER AREA >
// *---------------------------------------------------------------------------------------------*
//   Form ID      : WM_StockWM
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
    public partial class WM_StockWM : DC00_WinForm.BaseMDIChildForm
    {

        #region < MEMBER AREA >
        DataTable rtnDtTemp = new DataTable(); // 
        UltraGridUtil _GridUtil = new UltraGridUtil();  //그리드 객체 생성
        Common _Common = new Common();
        string plantCode = LoginInfo.PlantCode;

        #endregion


        #region < CONSTRUCTOR >
        public WM_StockWM()
        {
            InitializeComponent();
        }
        #endregion


        #region < FORM EVENTS >
        private void WM_StockWM_Load(object sender, EventArgs e)
        {
            #region ▶ GRID ◀
            _GridUtil.InitializeGrid(this.grid1, true, true, false, "", false);
            _GridUtil.InitColumnUltraGrid(grid1, "CHK", "상차등록", true, GridColDataType_emu.CheckBox, 70, 100, Infragistics.Win.HAlign.Center, true, true);
            _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE", "공장", true, GridColDataType_emu.VarChar, 120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "SHIPFLAG", "상차여부", true, GridColDataType_emu.VarChar, 140, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE", "품목", true, GridColDataType_emu.VarChar, 140, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMNAME", "품목명", true, GridColDataType_emu.VarChar, 120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "LOTNO", "LOTNO", true, GridColDataType_emu.VarChar, 120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WHCODE", "입고창고", true, GridColDataType_emu.VarChar, 100, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "STOCKQTY", "재고수량", true, GridColDataType_emu.Double, 100, 120, Infragistics.Win.HAlign.Right, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "UNITCODE", "단위", true, GridColDataType_emu.VarChar, 100, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "INDATE", "입고일자", true, GridColDataType_emu.VarChar, 100, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "MAKEDATE", "등록일시", true, GridColDataType_emu.VarChar, 100, 120, Infragistics.Win.HAlign.Right, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "MAKER", "등록자", true, GridColDataType_emu.VarChar, 100, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.SetInitUltraGridBind(grid1);
            #endregion


            #region ▶ COMBOBOX ◀
            rtnDtTemp = _Common.Standard_CODE("PLANTCODE");  // 공장
            Common.FillComboboxMaster(this.cboPlantCode, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
            UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE", rtnDtTemp, "CODE_ID", "CODE_NAME");
            this.cboPlantCode.Value = "1000";

            rtnDtTemp = _Common.Standard_CODE("YESNO");
            UltraGridUtil.SetComboUltraGrid(this.grid1, "SHIPFLAG", rtnDtTemp, "CODE_ID", "CODE_NAME");
            Common.FillComboboxMaster(this.cboShipFlag, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");

            rtnDtTemp = _Common.Standard_CODE("UNITCODE");     //단위
            UltraGridUtil.SetComboUltraGrid(this.grid1, "UNITCODE", rtnDtTemp, "CODE_ID", "CODE_NAME");

            rtnDtTemp = _Common.Standard_CODE("WHCODE");     //입고 창고
            UltraGridUtil.SetComboUltraGrid(this.grid1, "WHCODE", rtnDtTemp, "CODE_ID", "CODE_NAME");


            // 품목코드 
            //FP  : 완제품
            //OM  : 외주가공품
            //R/M : 원자재
            //S/M : 부자재(H / W)
            //SFP : 반제품

            #endregion

            #region ▶ POP-UP ◀
            BizTextBoxManager btbman = new BizTextBoxManager();
            btbman.PopUpAdd(txtItemCode, txtItemName, "ITEM_MASTER", new object[] { "1000", "" });
            btbman.PopUpAdd(txtWorkerID, txtWorkerName, "WORKER_MASTER", new object[] { "", "", "", "", "" });
            btbman.PopUpAdd(txtCustID, txtCustName, "CUST_MASTER", new object[] { cboPlantCode, "", "", "", "" });


            #endregion

            #region ▶ ENTER-MOVE ◀
            cboPlantCode.Value = plantCode;
            #endregion
        }
        #endregion


        #region < TOOL BAR AREA >
        public override void DoInquire()
        {
            base.DoInquire();
            DBHelper helper = new DBHelper(false);

            try
            {
                string sPlantcode = Convert.ToString(cboPlantCode.Value);
                string sItemcode = Convert.ToString(txtItemCode.Value);
                string sStartDate = string.Format("{0:yyyy-MM-01}", dtpStart.Value);
                string sEndDate = string.Format("{0:yyyy-MM-dd}", dtpEnd.Value);
                string sLOTNo = Convert.ToString(txtLOTNo.Text);
                string sShipFlag = Convert.ToString(cboShipFlag.Value);

                DataTable dtTemp = new DataTable();
                dtTemp = helper.FillTable("11WM_StockWM_S1", CommandType.StoredProcedure
                                           , helper.CreateParameter("PLANTCODE", sPlantcode, DbType.String, ParameterDirection.Input)
                                           , helper.CreateParameter("ITEMCODE", sItemcode, DbType.String, ParameterDirection.Input)
                                           , helper.CreateParameter("STARTDATE", sStartDate, DbType.String, ParameterDirection.Input)
                                           , helper.CreateParameter("ENDDATE", sEndDate, DbType.String, ParameterDirection.Input)
                                           , helper.CreateParameter("LOTNO", sLOTNo, DbType.String, ParameterDirection.Input)
                                           , helper.CreateParameter("SHIPFLAG", sShipFlag, DbType.String, ParameterDirection.Input)
                                          );
                this.ClosePrgForm();
                if (dtTemp.Rows.Count > 0)
                {
                    grid1.DataSource = dtTemp;
                    grid1.DataBinds(dtTemp);
                }
                else
                {
                    _GridUtil.Grid_Clear(grid1);
                    ShowDialog("조회할 데이터가 없습니다.", DC00_WinForm.DialogForm.DialogType.OK);
                }
            }
            catch (Exception ex)
            {
                ShowDialog(ex.Message, DC00_WinForm.DialogForm.DialogType.OK);
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
            DataTable dt = new DataTable();

            dt = grid1.chkChange();
            if (dt == null)
                return;

            if (this.txtCarNo.Text == "")
            {
                this.ShowDialog("차량번호를 입력하세요", DialogForm.DialogType.OK);
                return;
            }
            DBHelper helper = new DBHelper("", false);
            
            try
            {
                //base.DoSave();

                if (this.ShowDialog("C:Q00009") == System.Windows.Forms.DialogResult.Cancel)
                {
                    CancelProcess = true;
                    return;
                }
                


                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToString(dt.Rows[i]["CHK"]) == "0") continue;

                    helper.ExecuteNoneQuery("11WM_StockWM_U1"
                                            , CommandType.StoredProcedure
                                            , helper.CreateParameter("PLANTCODE"    , Convert.ToString(cboPlantCode.Value)          , DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("WORKERID"     , Convert.ToString(txtWorkerID.Value)           , DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("CUSTCODE"     , Convert.ToString(txtCustID.Value)             , DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("ITEMCODE"     , Convert.ToString(dt.Rows[i]["ITEMCODE"])      , DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("LOTNO"        , Convert.ToString(dt.Rows[i]["LOTNO"])         , DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("CARNO"        , Convert.ToString(txtCarNo.Value)              , DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("MAKER"        , LoginInfo.UserID                              , DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("SHIPQTY"      , Convert.ToString(dt.Rows[i]["STOCKQTY"])      , DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("I"            , i                                       , DbType.String, ParameterDirection.Input)
                                            );
                    if (helper.RSCODE != "S")
                    {
                        this.ShowDialog(helper.RSMSG, DialogForm.DialogType.OK);
                        helper.Rollback();
                        return;
                    }
                    
                }

                helper.Commit();
                this.ShowDialog("데이터가 저장 되었습니다.", DialogForm.DialogType.OK);
                this.ClosePrgForm();
                DoInquire();
            }
            catch (Exception ex)
            {
                helper.Rollback();
                this.ShowDialog(ex.ToString());
            }
            finally
            {
                helper.Close();
            }
        }
    }
    #endregion
}