﻿namespace DC_POPUP
{
    partial class Report_BanChul
    {
        #region Component Designer generated code
        /// <summary>
        /// Required method for telerik Reporting designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Report_BanChul));
            Telerik.Reporting.ReportParameter reportParameter1 = new Telerik.Reporting.ReportParameter();
            Telerik.Reporting.Drawing.StyleRule styleRule1 = new Telerik.Reporting.Drawing.StyleRule();
            this.pageBodySection1 = new Telerik.Reporting.DetailSection();
            this.textBox23 = new Telerik.Reporting.TextBox();
            this.pageHeaderSection1 = new Telerik.Reporting.PageHeaderSection();
            this.textBox17 = new Telerik.Reporting.TextBox();
            this.barcode1 = new Telerik.Reporting.Barcode();
            this.pictureBox1 = new Telerik.Reporting.PictureBox();
            this.textBox5 = new Telerik.Reporting.TextBox();
            this.pageFooterSection1 = new Telerik.Reporting.PageFooterSection();
            this.textBox1 = new Telerik.Reporting.TextBox();
            this.textBox2 = new Telerik.Reporting.TextBox();
            this.textBox3 = new Telerik.Reporting.TextBox();
            this.textBox4 = new Telerik.Reporting.TextBox();
            this.textBox6 = new Telerik.Reporting.TextBox();
            this.textBox7 = new Telerik.Reporting.TextBox();
            this.textBox8 = new Telerik.Reporting.TextBox();
            this.textBox9 = new Telerik.Reporting.TextBox();
            this.textBox10 = new Telerik.Reporting.TextBox();
            this.textBox11 = new Telerik.Reporting.TextBox();
            this.textBox12 = new Telerik.Reporting.TextBox();
            this.textBox13 = new Telerik.Reporting.TextBox();
            this.textBox14 = new Telerik.Reporting.TextBox();
            this.textBox15 = new Telerik.Reporting.TextBox();
            this.textBox16 = new Telerik.Reporting.TextBox();
            this.textBox18 = new Telerik.Reporting.TextBox();
            this.textBox19 = new Telerik.Reporting.TextBox();
            this.textBox20 = new Telerik.Reporting.TextBox();
            this.textBox21 = new Telerik.Reporting.TextBox();
            this.textBox22 = new Telerik.Reporting.TextBox();
            this.textBox24 = new Telerik.Reporting.TextBox();
            this.textBox25 = new Telerik.Reporting.TextBox();
            this.textBox26 = new Telerik.Reporting.TextBox();
            this.textBox27 = new Telerik.Reporting.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // pageBodySection1
            // 
            this.pageBodySection1.Height = Telerik.Reporting.Drawing.Unit.Cm(0.60029977560043335D);
            this.pageBodySection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox23,
            this.textBox15,
            this.textBox16,
            this.textBox18,
            this.textBox19,
            this.textBox20,
            this.textBox21});
            this.pageBodySection1.Name = "pageBodySection1";
            this.pageBodySection1.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.pageBodySection1.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(1D);
            // 
            // textBox23
            // 
            this.textBox23.Bindings.Add(new Telerik.Reporting.Binding("Value", "=Fields.RowNo"));
            this.textBox23.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0.00029972329502925277D));
            this.textBox23.Name = "textBox23";
            this.textBox23.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.1000003814697266D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox23.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox23.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox23.Style.Font.Name = "굴림체";
            this.textBox23.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(10D);
            this.textBox23.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox23.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox23.Value = "";
            // 
            // pageHeaderSection1
            // 
            this.pageHeaderSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(2.8999998569488525D);
            this.pageHeaderSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.textBox17,
            this.barcode1,
            this.textBox5,
            this.textBox2,
            this.textBox3,
            this.textBox4,
            this.textBox6,
            this.textBox7,
            this.textBox8,
            this.textBox9,
            this.textBox10,
            this.textBox11,
            this.textBox12,
            this.textBox13,
            this.textBox14});
            this.pageHeaderSection1.Name = "pageHeaderSection1";
            // 
            // textBox17
            // 
            this.textBox17.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(2.1999990940093994D));
            this.textBox17.Name = "textBox17";
            this.textBox17.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.0999999046325684D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox17.Style.BackgroundColor = System.Drawing.Color.Silver;
            this.textBox17.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox17.Style.Font.Bold = true;
            this.textBox17.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox17.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox17.Value = "순 번";
            // 
            // barcode1
            // 
            this.barcode1.BarAlign = Telerik.Reporting.Drawing.HorizontalAlign.Right;
            this.barcode1.Bindings.Add(new Telerik.Reporting.Binding("Value", "=Fields.TRADINGNO"));
            this.barcode1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(6.7917728424072266D), Telerik.Reporting.Drawing.Unit.Cm(0.20000006258487701D));
            this.barcode1.Name = "barcode1";
            this.barcode1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.8000001907348633D), Telerik.Reporting.Drawing.Unit.Cm(0.89999997615814209D));
            this.barcode1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.barcode1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Bottom;
            this.barcode1.Value = "INVO020210616205046";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(4.60063362121582D), Telerik.Reporting.Drawing.Unit.Cm(0.60019975900650024D));
            this.pictureBox1.MimeType = "image/jpeg";
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.9997994899749756D), Telerik.Reporting.Drawing.Unit.Cm(1.0002995729446411D));
            this.pictureBox1.Sizing = Telerik.Reporting.Drawing.ImageSizeMode.ScaleProportional;
            this.pictureBox1.Value = ((object)(resources.GetObject("pictureBox1.Value")));
            // 
            // textBox5
            // 
            this.textBox5.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(0D), Telerik.Reporting.Drawing.Unit.Cm(0.10000003129243851D));
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(6.6999998092651367D), Telerik.Reporting.Drawing.Unit.Cm(1.2000002861022949D));
            this.textBox5.Style.Font.Bold = true;
            this.textBox5.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(30D);
            this.textBox5.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox5.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox5.Value = "거래 명세표";
            // 
            // pageFooterSection1
            // 
            this.pageFooterSection1.Height = Telerik.Reporting.Drawing.Unit.Cm(2.4997003078460693D);
            this.pageFooterSection1.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pictureBox1,
            this.textBox1,
            this.textBox22,
            this.textBox24,
            this.textBox25,
            this.textBox26,
            this.textBox27});
            this.pageFooterSection1.Name = "pageFooterSection1";
            this.pageFooterSection1.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.pageFooterSection1.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(1D);
            // 
            // textBox1
            // 
            this.textBox1.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(4.4245748519897461D), Telerik.Reporting.Drawing.Unit.Cm(1.7997006177902222D));
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.7916752099990845D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox1.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7D);
            this.textBox1.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox1.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox1.Value = "■차량 번호  :";
            // 
            // textBox2
            // 
            this.textBox2.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(9.9907767435070127E-05D), Telerik.Reporting.Drawing.Unit.Cm(1.2999992370605469D));
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.6999006271362305D), Telerik.Reporting.Drawing.Unit.Cm(0.899800181388855D));
            this.textBox2.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox2.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox2.Style.Font.Name = "굴림체";
            this.textBox2.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(10D);
            this.textBox2.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox2.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox2.Value = "출고 업체";
            // 
            // textBox3
            // 
            this.textBox3.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.7002002000808716D), Telerik.Reporting.Drawing.Unit.Cm(1.299999475479126D));
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.7000031471252441D), Telerik.Reporting.Drawing.Unit.Cm(0.899800181388855D));
            this.textBox3.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox3.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox3.Style.Font.Name = "굴림체";
            this.textBox3.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(10D);
            this.textBox3.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox3.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox3.Value = "한국 품질 재단";
            // 
            // textBox4
            // 
            this.textBox4.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(4.4004030227661133D), Telerik.Reporting.Drawing.Unit.Cm(1.299999475479126D));
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.499497652053833D), Telerik.Reporting.Drawing.Unit.Cm(0.899800181388855D));
            this.textBox4.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox4.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox4.Style.Font.Name = "굴림체";
            this.textBox4.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(10D);
            this.textBox4.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox4.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox4.Value = "발행자";
            // 
            // textBox6
            // 
            this.textBox6.Bindings.Add(new Telerik.Reporting.Binding("Value", "=Fields.MAKER"));
            this.textBox6.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(5.9001007080078125D), Telerik.Reporting.Drawing.Unit.Cm(1.299999475479126D));
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.0998005867004395D), Telerik.Reporting.Drawing.Unit.Cm(0.899800181388855D));
            this.textBox6.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox6.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox6.Style.Font.Name = "굴림체";
            this.textBox6.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(10D);
            this.textBox6.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox6.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox6.Value = "";
            // 
            // textBox7
            // 
            this.textBox7.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(9.0001010894775391D), Telerik.Reporting.Drawing.Unit.Cm(1.299999475479126D));
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.6999006271362305D), Telerik.Reporting.Drawing.Unit.Cm(0.899800181388855D));
            this.textBox7.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox7.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox7.Style.Font.Name = "굴림체";
            this.textBox7.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(10D);
            this.textBox7.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox7.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox7.Value = "생성일시";
            // 
            // textBox8
            // 
            this.textBox8.Bindings.Add(new Telerik.Reporting.Binding("Value", "=Fields.MAKEDATE"));
            this.textBox8.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.700201034545898D), Telerik.Reporting.Drawing.Unit.Cm(1.299999475479126D));
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.0575661659240723D), Telerik.Reporting.Drawing.Unit.Cm(0.899800181388855D));
            this.textBox8.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox8.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox8.Style.Font.Name = "굴림체";
            this.textBox8.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(10D);
            this.textBox8.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox8.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox8.Value = "";
            // 
            // textBox9
            // 
            this.textBox9.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.1001995801925659D), Telerik.Reporting.Drawing.Unit.Cm(2.1999993324279785D));
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.0998005867004395D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox9.Style.BackgroundColor = System.Drawing.Color.Silver;
            this.textBox9.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox9.Style.Font.Bold = true;
            this.textBox9.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox9.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox9.Value = "LOT NO";
            // 
            // textBox10
            // 
            this.textBox10.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(4.200200080871582D), Telerik.Reporting.Drawing.Unit.Cm(2.1999995708465576D));
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.9996997117996216D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox10.Style.BackgroundColor = System.Drawing.Color.Silver;
            this.textBox10.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox10.Style.Font.Bold = true;
            this.textBox10.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox10.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox10.Value = "품번";
            // 
            // textBox11
            // 
            this.textBox11.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(6.2000994682312012D), Telerik.Reporting.Drawing.Unit.Cm(2.2000000476837158D));
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.5997004508972168D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox11.Style.BackgroundColor = System.Drawing.Color.Silver;
            this.textBox11.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox11.Style.Font.Bold = true;
            this.textBox11.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox11.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox11.Value = "품명";
            // 
            // textBox12
            // 
            this.textBox12.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.8000001907348633D), Telerik.Reporting.Drawing.Unit.Cm(2.2000000476837158D));
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.9000005722045898D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox12.Style.BackgroundColor = System.Drawing.Color.Silver;
            this.textBox12.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox12.Style.Font.Bold = true;
            this.textBox12.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox12.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox12.Value = "반출수량";
            // 
            // textBox13
            // 
            this.textBox13.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.700201034545898D), Telerik.Reporting.Drawing.Unit.Cm(2.1999990940093994D));
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.6999009847640991D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox13.Style.BackgroundColor = System.Drawing.Color.Silver;
            this.textBox13.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox13.Style.Font.Bold = true;
            this.textBox13.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox13.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox13.Value = "거래처";
            // 
            // textBox14
            // 
            this.textBox14.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.400301933288574D), Telerik.Reporting.Drawing.Unit.Cm(2.2000000476837158D));
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.3575652837753296D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox14.Style.BackgroundColor = System.Drawing.Color.Silver;
            this.textBox14.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox14.Style.Font.Bold = true;
            this.textBox14.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox14.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox14.Value = "비고";
            // 
            // textBox15
            // 
            this.textBox15.Bindings.Add(new Telerik.Reporting.Binding("Value", "=Fields.LotNo"));
            this.textBox15.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(1.1002002954483032D), Telerik.Reporting.Drawing.Unit.Cm(9.9907767435070127E-05D));
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(3.0998001098632812D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox15.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox15.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox15.Style.Font.Name = "굴림체";
            this.textBox15.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(10D);
            this.textBox15.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox15.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox15.Value = "";
            // 
            // textBox16
            // 
            this.textBox16.Bindings.Add(new Telerik.Reporting.Binding("Value", "=Fields.ItemCode"));
            this.textBox16.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(4.200200080871582D), Telerik.Reporting.Drawing.Unit.Cm(0.00029972329502925277D));
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.9996997117996216D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox16.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox16.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox16.Style.Font.Name = "굴림체";
            this.textBox16.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(10D);
            this.textBox16.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox16.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox16.Value = "";
            // 
            // textBox18
            // 
            this.textBox18.Bindings.Add(new Telerik.Reporting.Binding("Value", "=Fields.ItemName"));
            this.textBox18.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(6.2000994682312012D), Telerik.Reporting.Drawing.Unit.Cm(9.9907767435070127E-05D));
            this.textBox18.Name = "textBox18";
            this.textBox18.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.599700927734375D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox18.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox18.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox18.Style.Font.Name = "굴림체";
            this.textBox18.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(10D);
            this.textBox18.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox18.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox18.Value = "";
            // 
            // textBox19
            // 
            this.textBox19.Bindings.Add(new Telerik.Reporting.Binding("Value", "=Fields.TRADINGQTY"));
            this.textBox19.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.8000001907348633D), Telerik.Reporting.Drawing.Unit.Cm(9.9907767435070127E-05D));
            this.textBox19.Name = "textBox19";
            this.textBox19.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.9000012874603272D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox19.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox19.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox19.Style.Font.Name = "굴림체";
            this.textBox19.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(10D);
            this.textBox19.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox19.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox19.Value = "";
            // 
            // textBox20
            // 
            this.textBox20.Bindings.Add(new Telerik.Reporting.Binding("Value", "=Fields.CUSTNAME"));
            this.textBox20.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.700201034545898D), Telerik.Reporting.Drawing.Unit.Cm(0.00029972329502925277D));
            this.textBox20.Name = "textBox20";
            this.textBox20.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.6999015808105469D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox20.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox20.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox20.Style.Font.Name = "굴림체";
            this.textBox20.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(10D);
            this.textBox20.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox20.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox20.Value = "";
            // 
            // textBox21
            // 
            this.textBox21.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(12.400301933288574D), Telerik.Reporting.Drawing.Unit.Cm(0.00029972329502925277D));
            this.textBox21.Name = "textBox21";
            this.textBox21.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.3575658798217773D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox21.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox21.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox21.Style.Font.Name = "굴림체";
            this.textBox21.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(10D);
            this.textBox21.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox21.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox21.Value = "";
            // 
            // textBox22
            // 
            this.textBox22.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(9.9907767435070127E-05D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox22.Name = "textBox22";
            this.textBox22.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(8.7997007369995117D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox22.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox22.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox22.Style.Font.Name = "굴림체";
            this.textBox22.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(10D);
            this.textBox22.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox22.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox22.Value = "합계";
            // 
            // textBox24
            // 
            this.textBox24.Bindings.Add(new Telerik.Reporting.Binding("Value", "=Fields.CARNO"));
            this.textBox24.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(6.2164497375488281D), Telerik.Reporting.Drawing.Unit.Cm(1.7997006177902222D));
            this.textBox24.Name = "textBox24";
            this.textBox24.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.6917746067047119D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox24.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7D);
            this.textBox24.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox24.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox24.Value = "";
            // 
            // textBox25
            // 
            this.textBox25.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(10.90000057220459D), Telerik.Reporting.Drawing.Unit.Cm(1.7997006177902222D));
            this.textBox25.Name = "textBox25";
            this.textBox25.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(2.6917746067047119D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox25.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7D);
            this.textBox25.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox25.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox25.Value = "=CDate(now())";
            // 
            // textBox26
            // 
            this.textBox26.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(9.1008338928222656D), Telerik.Reporting.Drawing.Unit.Cm(1.7997006177902222D));
            this.textBox26.Name = "textBox26";
            this.textBox26.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.7916752099990845D), Telerik.Reporting.Drawing.Unit.Cm(0.40000000596046448D));
            this.textBox26.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Point(7D);
            this.textBox26.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox26.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox26.Value = "■출력 일시  :";
            // 
            // textBox27
            // 
            this.textBox27.Location = new Telerik.Reporting.Drawing.PointU(Telerik.Reporting.Drawing.Unit.Cm(8.8000001907348633D), Telerik.Reporting.Drawing.Unit.Cm(0D));
            this.textBox27.Name = "textBox27";
            this.textBox27.Size = new Telerik.Reporting.Drawing.SizeU(Telerik.Reporting.Drawing.Unit.Cm(1.9000006914138794D), Telerik.Reporting.Drawing.Unit.Cm(0.60000002384185791D));
            this.textBox27.Style.BorderStyle.Default = Telerik.Reporting.Drawing.BorderType.Solid;
            this.textBox27.Style.BorderWidth.Bottom = Telerik.Reporting.Drawing.Unit.Point(1D);
            this.textBox27.Style.Font.Name = "굴림체";
            this.textBox27.Style.Font.Size = Telerik.Reporting.Drawing.Unit.Pixel(10D);
            this.textBox27.Style.TextAlign = Telerik.Reporting.Drawing.HorizontalAlign.Center;
            this.textBox27.Style.VerticalAlign = Telerik.Reporting.Drawing.VerticalAlign.Middle;
            this.textBox27.Value = "=Sum(Fields.TRADINGQTY)";
            // 
            // Report_BanChul
            // 
            this.DataSource = null;
            this.Items.AddRange(new Telerik.Reporting.ReportItemBase[] {
            this.pageBodySection1,
            this.pageFooterSection1,
            this.pageHeaderSection1});
            this.Name = "Report_BanChul";
            this.PageSettings.Landscape = false;
            this.PageSettings.Margins = new Telerik.Reporting.Drawing.MarginsU(Telerik.Reporting.Drawing.Unit.Mm(15D), Telerik.Reporting.Drawing.Unit.Mm(15D), Telerik.Reporting.Drawing.Unit.Mm(27D), Telerik.Reporting.Drawing.Unit.Mm(30D));
            this.PageSettings.PaperKind = System.Drawing.Printing.PaperKind.A4;
            reportParameter1.Name = "TraPlanDate";
            reportParameter1.Value = "TraPlanDate.Value";
            this.ReportParameters.Add(reportParameter1);
            this.Style.BackgroundColor = System.Drawing.Color.White;
            this.Style.BorderWidth.Default = Telerik.Reporting.Drawing.Unit.Point(1.5D);
            styleRule1.Selectors.AddRange(new Telerik.Reporting.Drawing.ISelector[] {
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.TextItemBase)),
            new Telerik.Reporting.Drawing.TypeSelector(typeof(Telerik.Reporting.HtmlTextBox))});
            styleRule1.Style.Padding.Left = Telerik.Reporting.Drawing.Unit.Point(2D);
            styleRule1.Style.Padding.Right = Telerik.Reporting.Drawing.Unit.Point(2D);
            this.StyleSheet.AddRange(new Telerik.Reporting.Drawing.StyleRule[] {
            styleRule1});
            this.Width = Telerik.Reporting.Drawing.Unit.Cm(13.757867813110352D);
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }
        #endregion

        private Telerik.Reporting.DetailSection pageBodySection1;
        private Telerik.Reporting.TextBox textBox23;
        private Telerik.Reporting.PageHeaderSection pageHeaderSection1;
        private Telerik.Reporting.PageFooterSection pageFooterSection1;
        private Telerik.Reporting.TextBox textBox17;
        private Telerik.Reporting.PictureBox pictureBox1;
        private Telerik.Reporting.Barcode barcode1;
        private Telerik.Reporting.TextBox textBox5;
        private Telerik.Reporting.TextBox textBox15;
        private Telerik.Reporting.TextBox textBox16;
        private Telerik.Reporting.TextBox textBox18;
        private Telerik.Reporting.TextBox textBox19;
        private Telerik.Reporting.TextBox textBox20;
        private Telerik.Reporting.TextBox textBox21;
        private Telerik.Reporting.TextBox textBox2;
        private Telerik.Reporting.TextBox textBox3;
        private Telerik.Reporting.TextBox textBox4;
        private Telerik.Reporting.TextBox textBox6;
        private Telerik.Reporting.TextBox textBox7;
        private Telerik.Reporting.TextBox textBox8;
        private Telerik.Reporting.TextBox textBox9;
        private Telerik.Reporting.TextBox textBox10;
        private Telerik.Reporting.TextBox textBox11;
        private Telerik.Reporting.TextBox textBox12;
        private Telerik.Reporting.TextBox textBox13;
        private Telerik.Reporting.TextBox textBox14;
        private Telerik.Reporting.TextBox textBox1;
        private Telerik.Reporting.TextBox textBox22;
        private Telerik.Reporting.TextBox textBox24;
        private Telerik.Reporting.TextBox textBox25;
        private Telerik.Reporting.TextBox textBox26;
        private Telerik.Reporting.TextBox textBox27;
    }
}