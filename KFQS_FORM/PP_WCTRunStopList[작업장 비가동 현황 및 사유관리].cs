#region < HEADER AREA >
// *---------------------------------------------------------------------------------------------*
//   Form ID      : PP_WCTRunStopList
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
    public partial class PP_WCTRunStopList : DC00_WinForm.BaseMDIChildForm
    {

        #region < MEMBER AREA >
        DataTable rtnDtTemp        = new DataTable(); // 
        UltraGridUtil _GridUtil    = new UltraGridUtil();  //그리드 객체 생성
        Common _Common             = new Common();
        string plantCode           = LoginInfo.PlantCode;

        #endregion


        #region < CONSTRUCTOR >
        public PP_WCTRunStopList()
        {
            InitializeComponent();
        }
        #endregion


        #region < FORM EVENTS >
        private void PP_WCTRunStopList_Load(object sender, EventArgs e)
        {
            #region ▶ GRID ◀
            _GridUtil.InitializeGrid(this.grid1, true, true, false, "", false);
            _GridUtil.InitColumnUltraGrid(grid1, "PLANTCODE",       "공장",               true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left,    true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKCENTERCODE",  "작업장",             true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left,    true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKCENTERNAME",  "작업장명",           true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left,    true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ORDERNO",         "작업지시번호",       true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left,    true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "RSSEQ",           "RSSEQ",              true, GridColDataType_emu.VarChar,    120, 120, Infragistics.Win.HAlign.Left,    false, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMCODE",        "품목코드",           true, GridColDataType_emu.VarChar,    140, 120, Infragistics.Win.HAlign.Left,    true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "ITEMNAME",        "품명",               true, GridColDataType_emu.VarChar,    140, 120, Infragistics.Win.HAlign.Left,    true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKERNAME",      "작업자",             true, GridColDataType_emu.VarChar,    140, 120, Infragistics.Win.HAlign.Left,    true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "WORKSTATUS",      "가동/비가동",        true, GridColDataType_emu.VarChar,    140, 120, Infragistics.Win.HAlign.Left,    true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "RSSTARTDATE",     "시작일시",           true, GridColDataType_emu.VarChar,    140, 120, Infragistics.Win.HAlign.Left,    true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "RSENDDATE",       "종료일시",           true, GridColDataType_emu.VarChar,    140, 120, Infragistics.Win.HAlign.Left,    true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "RUNTIME",         "소요시간(분)",       true, GridColDataType_emu.VarChar,    140, 120, Infragistics.Win.HAlign.Right,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "PRODQTY",         "양품수량",           true, GridColDataType_emu.VarChar,    140, 120, Infragistics.Win.HAlign.Right,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "BADQTY",          "불량수량",           true, GridColDataType_emu.VarChar,    140, 120, Infragistics.Win.HAlign.Right,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "REMARK",          "사유",               true, GridColDataType_emu.VarChar,    140, 120, Infragistics.Win.HAlign.Right,   true, true);
            _GridUtil.InitColumnUltraGrid(grid1, "MAKER",           "등록자",             true, GridColDataType_emu.VarChar,    140, 120, Infragistics.Win.HAlign.Right,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "MAKEDATE",        "등록일시",           true, GridColDataType_emu.VarChar,    140, 120, Infragistics.Win.HAlign.Right,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "EDITOR",          "수정자",             true, GridColDataType_emu.VarChar,    140, 120, Infragistics.Win.HAlign.Right,   true, false);
            _GridUtil.InitColumnUltraGrid(grid1, "EDITDATE",        "수정일시",           true, GridColDataType_emu.VarChar,    140, 120, Infragistics.Win.HAlign.Right,   true, false);
            _GridUtil.SetInitUltraGridBind(grid1);
            #endregion

            //
            this.grid1.DisplayLayout.Override.MergedCellContentArea = MergedCellContentArea.VirtualRect;
            this.grid1.DisplayLayout.Bands[0].Columns["PLANTCODE"].MergedCellStyle = MergedCellStyle.Always;
            this.grid1.DisplayLayout.Bands[0].Columns["WORKCENTERCODE"].MergedCellStyle = MergedCellStyle.Always;
            this.grid1.DisplayLayout.Bands[0].Columns["WORKCENTERNAME"].MergedCellStyle = MergedCellStyle.Always;
            this.grid1.DisplayLayout.Bands[0].Columns["ORDERNO"].MergedCellStyle = MergedCellStyle.Always;
            this.grid1.DisplayLayout.Bands[0].Columns["ITEMCODE"].MergedCellStyle = MergedCellStyle.Always;
            this.grid1.DisplayLayout.Bands[0].Columns["ITEMNAME"].MergedCellStyle = MergedCellStyle.Always;

            #region ▶ COMBOBOX ◀
            rtnDtTemp = _Common.Standard_CODE("PLANTCODE");  // 공장
            Common.FillComboboxMaster(this.cboPlantCode, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName, rtnDtTemp.Columns["CODE_NAME"].ColumnName, "ALL", "");
            UltraGridUtil.SetComboUltraGrid(this.grid1, "PLANTCODE", rtnDtTemp, "CODE_ID", "CODE_NAME");

            rtnDtTemp = _Common.GET_Workcenter_Code();  // 작업장 번호 
            Common.FillComboboxMaster(this.cboWorkcenterCode, rtnDtTemp, rtnDtTemp.Columns["CODE_ID"].ColumnName/*밸류 멤버*/, rtnDtTemp.Columns["CODE_NAME"].ColumnName/*디스플레이 멤버*/, "ALL", "");
            UltraGridUtil.SetComboUltraGrid(this.grid1, "WORKCENTERCODE", rtnDtTemp, "CODE_ID", "CODE_NAME");
            #endregion

            #region ▶ POP-UP ◀
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
                string sPlantCode       = Convert.ToString(cboPlantCode.Value);
                string sWorkcenterCode  = Convert.ToString(cboWorkcenterCode.Value);
                string sStartdate       = string.Format("{0:yyyy-MM-01}", dtpStart.Value);
                string sEnddate         = string.Format("{0:yyyy-MM-dd}", dtpEnd.Value);
                rtnDtTemp = helper.FillTable("11PP_WCTRunStopList_S1", CommandType.StoredProcedure
                                    , helper.CreateParameter("PLANTCODE",           sPlantCode,  DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("WORKCENTERCODE",      sWorkcenterCode,  DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("STARTDATE",           sStartdate,   DbType.String, ParameterDirection.Input)
                                    , helper.CreateParameter("ENDDATE",             sEnddate,      DbType.String, ParameterDirection.Input)
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
                    string sEditor = Convert.ToString(LoginInfo.UserID);

                    helper.ExecuteNoneQuery("11PP_WCTRunStopList_U1", CommandType.StoredProcedure
                                          , helper.CreateParameter("PLANTCODE"       , drRow["PLANTCODE"     ].ToString(), DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("WORKCENTERCODE"  , drRow["WORKCENTERCODE"].ToString(), DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("ORDERNO"         , drRow["ORDERNO"       ].ToString(), DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("REMARK"          , drRow["REMARK"        ].ToString(), DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("RSSTARTDATE"     , drRow["RSSTARTDATE"   ].ToString(), DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("RSENDDATE"       , drRow["RSENDDATE"     ].ToString(), DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("RSSEQ"           , drRow["RSSEQ"     ].ToString(), DbType.String, ParameterDirection.Input)
                                          , helper.CreateParameter("EDITOR"          , sEditor, DbType.String, ParameterDirection.Input)

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

        private void grid1_InitializeLayout(object sender, InitializeLayoutEventArgs e)
        {
            // 그리드 컬럼간 머지 합병 기능 적용
            CustomMergedCellEvalutor CM1 = new CustomMergedCellEvalutor("ORDERNO", "ITEMCODE");
            e.Layout.Bands[0].Columns["ITEMCODE"].MergedCellEvaluator = CM1;
            e.Layout.Bands[0].Columns["ITEMNAME"].MergedCellEvaluator = CM1;
        }
    }
}




