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
    public partial class PP_StockHALB : DC00_WinForm.BaseMDIChildForm
    {
        #region <MEMBER AREA>

        DataTable table         = new DataTable();
        DataTable rtnDtTemp     = new DataTable();
        UltraGridUtil _GridUtil = new UltraGridUtil();
        #endregion

        #region < CONSTRUCTOR >

        public PP_StockHALB()
        {
            InitializeComponent();
            //BizTextBoxManager btbManager = new BizTextBoxManager();

            //btbManager.PopUpAdd(txtItemCode_H, txtItemName_H, "ITEM_MASTER", new object[] { cboPlantCode, "" });


        }
        #endregion

        #region  PP_StockHALB
        private void PP_StockHALB_Load(object sender, EventArgs e)
        {
            //그리드 객체 생성
            #region 

            _GridUtil.InitializeGrid(this.grid1, false, true, false, "", false);
            _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE"        , "공장"            , true, GridColDataType_emu.VarChar, 110, 100, Infragistics.Win.HAlign.Center, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE"         , "품목"            , true, GridColDataType_emu.VarChar, 110, 100, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMNAME"         , "품목명"          , true, GridColDataType_emu.VarChar, 170, 100, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMTYPE"         , "품목구분"        , true, GridColDataType_emu.VarChar, 170, 100, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "LOTNO"            , "LOTNO"           , true, GridColDataType_emu.VarChar, 110, 100, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKCENTERCODE"   , "작업장"          , true, GridColDataType_emu.VarChar, 120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKCENTERNAME"   , "작업장명"        , true, GridColDataType_emu.VarChar, 120, 120, Infragistics.Win.HAlign.Left, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "STOCKQTY"         , "재고수량"        , true, GridColDataType_emu.VarChar, 100, 100, Infragistics.Win.HAlign.Center, true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "UNITCODE"         , "단위"            , true, GridColDataType_emu.VarChar, 50, 100, Infragistics.Win.HAlign.Center, true, false);
            _GridUtil.SetInitUltraGridBind(grid1);
            #endregion

            #region 콤보박스
            Common _Common = new Common();
            DataTable rtnDtTemp = _Common.Standard_CODE("PLANTCODE");  //사업장
            Common.FillComboboxMaster(this.cboPlantCode, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
            UltraGridUtil.SetComboUltraGrid(this.grid1, "PlantCode", rtnDtTemp, "CODE_ID", "CODE_NAME");


            rtnDtTemp = _Common.Standard_CODE("ITEMTYPE");  // 품목구분
            Common.FillComboboxMaster(this.cboItemType, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
            UltraGridUtil.SetComboUltraGrid(this.grid1, "ITEMTYPE", rtnDtTemp, "CODE_ID", "CODE_NAME");
            this.cboPlantCode.Value = 1000;
            
        }
        #endregion  PP_StockHALB_Load

        #region <TOOL BAR AREA >




        public override void DoInquire()
        {   
            
            this._GridUtil.Grid_Clear(grid1);

            DBHelper helper = new DBHelper(false);

            try
            {

                string sPlantCode = Convert.ToString(cboPlantCode.Value);
                string sLotNo     = Convert.ToString(txtLotNo.Text);
                string sItemType  = Convert.ToString(cboItemType.Value);

                rtnDtTemp = helper.FillTable("11PP_StockHALB_S1", CommandType.StoredProcedure
                                              , helper.CreateParameter("PlantCode",     sPlantCode,      DbType.String, ParameterDirection.Input)
                                              , helper.CreateParameter("ITEMTYPE",      sItemType,       DbType.String, ParameterDirection.Input)
                                              , helper.CreateParameter("LOTNO",         sLotNo,          DbType.String, ParameterDirection.Input)
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


        #region <METHOD AREA>
        #endregion
        public override void DoSave()
        {

            DataTable dt = new DataTable();

                dt = grid1.chkChange();
                if (dt == null)
                    return;

                if (this.cboPlantCode.Value.ToString() == "")
                {
                    this.ShowDialog("공장을 선택하세요", DialogForm.DialogType.OK);
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

                for (int i = 0; i < dt.Rows.Count; i++ )
                {
                    if (Convert.ToString(dt.Rows[i]["CHK"]) == "0") continue;

                    if (Convert.ToString(dt.Rows[i]["ITEMTYPE"]) != "ROH")
                    { 
                        helper.Rollback();
                        MessageBox.Show("원자재만 선택하세요.");
                        return;
                    }

                    helper.ExecuteNoneQuery("11PP_StockHALB_U1"
                                            , CommandType.StoredProcedure
                                            , helper.CreateParameter("PLANTCODE",      Convert.ToString(cboPlantCode.Value), DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("ITEMCODE",       Convert.ToString(dt.Rows[i]["ITEMCODE"]), DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("LOTNO",          Convert.ToString(dt.Rows[i]["LOTNO"]), DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("QTY",            Convert.ToString(dt.Rows[i]["STOCKQTY"]), DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("UnitCode",       Convert.ToString(dt.Rows[i]["UnitCode"]), DbType.String, ParameterDirection.Input)
                                            , helper.CreateParameter("WORKERID",       this.WorkerID, DbType.String, ParameterDirection.Input)
                                            );


                    if (helper.RSCODE == "E")
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