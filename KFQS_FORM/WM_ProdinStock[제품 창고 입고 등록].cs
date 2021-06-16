﻿#region < HEADER AREA >
// *---------------------------------------------------------------------------------------------*
//   Form ID      : 
//   Form Name    : 생산출고 등록/취소
//   Name Space   : 
//   Created Date : 
//   Made By      : 
//   Description  : 
// *---------------------------------------------------------------------------------------------*
#endregion

#region <USING AREA>
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DC00_assm;
using DC_POPUP;
using DC00_WinForm;

#endregion

namespace KFQS_Form
{
    public partial class WM_ProdinStock : DC00_WinForm.BaseMDIChildForm
    {
        #region <MEMBER AREA>

        DataTable table         = new DataTable();
        DataTable rtnDtTemp     = new DataTable();
        UltraGridUtil _GridUtil = new UltraGridUtil();
        #endregion

        #region < CONSTRUCTOR >

        public WM_ProdinStock()
        {
            InitializeComponent();
            //BizTextBoxManager btbManager = new BizTextBoxManager();

            //btbManager.PopUpAdd(txtItemCode_H, txtItemName_H, "ITEM_MASTER", new object[] { cboPlantCode, "" });


        }
        #endregion

        #region  WM_ProdinStock
        private void WM_ProdinStock_Load(object sender, EventArgs e)
        {
            //그리드 객체 생성
            #region 
            
            _GridUtil.InitializeGrid(this.grid1, false, true, false, "", false);
            _GridUtil.InitColumnUltraGrid(grid1, "CHK"          , "선택"      , true, GridColDataType_emu.CheckBox, 70, 100, Infragistics.Win.HAlign.Center, true, true);
            _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE"    , "공장"      , true, GridColDataType_emu.VarChar, 110, 100, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "LOTNO"        , "LOTNO"     , true, GridColDataType_emu.VarChar, 110, 100, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE"     , "품목"      , true, GridColDataType_emu.VarChar, 110, 100, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMNAME"     , "품목명"    , true, GridColDataType_emu.VarChar, 170, 100, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMTYPE"     , "품목타입"  , true, GridColDataType_emu.VarChar, 110, 100, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WHCode"       , "창고코드"  , true, GridColDataType_emu.VarChar, 100, 100, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "STOCKQTY"     , "LOT수량"   , true, GridColDataType_emu.Double,  70, 100, Infragistics.Win.HAlign.Right,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "UNITCODE"     , "단위"      , true, GridColDataType_emu.VarChar,  50, 100, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKER"       , "등록자"    , true, GridColDataType_emu.VarChar, 100, 100, Infragistics.Win.HAlign.Left,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "MAKEDATE"     , "등록일자"  , true, GridColDataType_emu.DateTime, 100, 100, Infragistics.Win.HAlign.Left,  true, false);

            _GridUtil.SetInitUltraGridBind(grid1);
            #endregion

            #region 콤보박스
            Common _Common = new Common();
            DataTable rtnDtTemp = _Common.Standard_CODE("PLANTCODE");  //사업장
            Common.FillComboboxMaster(this.cboPlantCode, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
            UltraGridUtil.SetComboUltraGrid(this.grid1, "PlantCode", rtnDtTemp, "CODE_ID", "CODE_NAME");

            rtnDtTemp = _Common.GET_ItemCodeFERT_Code("FERT");  // 품목
            Common.FillComboboxMaster(this.cboItemCode, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");

            string sPlantCode = Convert.ToString(this.cboPlantCode.Value);
            this.cboPlantCode.Value = "1000";
        }
        #endregion  WM_ProdinStock_Load

        #region <TOOL BAR AREA >




        public override void DoInquire()
        {   
            if (!CheckData())
            {
                return;
            }
            
            this._GridUtil.Grid_Clear(grid1);

            DBHelper helper = new DBHelper(false);

            try
            {

                string sPlantCode = Convert.ToString(cboPlantCode.Value);
                string sSrart     = string.Format("{0:yyyy-MM-dd}", dtStart_H.Value);
                string sEnd       = string.Format("{0:yyyy-MM-dd}", dtEnd_H.Value);
                string sItemCode  = cboItemCode.Value.ToString();

                rtnDtTemp = helper.FillTable("11WM_ProdinStock_S1", CommandType.StoredProcedure
                                              , helper.CreateParameter("PLANTCODE",  sPlantCode,      DbType.String, ParameterDirection.Input)
                                              , helper.CreateParameter("STARTDATE",  sSrart,          DbType.String, ParameterDirection.Input)
                                              , helper.CreateParameter("ENDDATE",    sEnd,            DbType.String, ParameterDirection.Input)
                                              , helper.CreateParameter("ITEMCODE",   sItemCode,       DbType.String, ParameterDirection.Input)
                                              );
                
                grid1.DataSource = rtnDtTemp;
                grid1.DataBinds();
                this.ClosePrgForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally 
            {
                helper.Close();
            }
        }
        #endregion

        private void dtStart_H_TextChanged(object sender, EventArgs e)
        {
            CheckData();
        }
        private bool CheckData()
        {
            int sSrart = Convert.ToInt32(string.Format("{0:yyyyMMdd}", dtStart_H.Value));
            int sEnd   = Convert.ToInt32(string.Format("{0:yyyyMMdd}", dtEnd_H.Value));
            if (sSrart > sEnd)
            {
                this.ShowDialog("조회 시작일자가 종료일자보다 큽니다.", DialogForm.DialogType.OK);
                return false;
            }
            return true;
        }
        #region <METHOD AREA>
        #endregion
        public override void DoSave()
        {

            DataTable dt = new DataTable();

                dt = grid1.chkChange();
                if (dt == null)
                    return;

 
            DBHelper helper = new DBHelper("", false);

            try
            {
                //base.DoSave();

                if (this.ShowDialog("C:Q00009") == System.Windows.Forms.DialogResult.Cancel)
                {
                    CancelProcess = true;
                    return;
                }

                for (int i = 0; i < dt.Rows.Count; i++ )
                {
                    if (Convert.ToString(dt.Rows[i]["CHK"]) == "0") continue; 

                    helper.ExecuteNoneQuery("11WM_ProdinStock_U1"
                                            , CommandType.StoredProcedure
                                            , helper.CreateParameter("PLANTCODE",       Convert.ToString(dt.Rows[i]["PLANTCODE"])   , DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("LOTNO",           Convert.ToString(dt.Rows[i]["LOTNO"])       , DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("ITEMCODE",        Convert.ToString(dt.Rows[i]["ITEMCODE"])    , DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("INOUTQTY",        Convert.ToString(dt.Rows[i]["STOCKQTY"])    , DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("UNITCODE",        Convert.ToString(dt.Rows[i]["UNITCODE"])    , DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("MAKER",           LoginInfo.UserID                            , DbType.String, ParameterDirection.Input));

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
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                helper.Close();
            }
        }
        #endregion
    }
}