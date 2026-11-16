
namespace PavoTest
{
    partial class Form1
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.mmLog = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.edJsonGelen = new System.Windows.Forms.TextBox();
            this.edJsonGiden = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.edIptalFisNo = new System.Windows.Forms.TextBox();
            this.pnKurumsal = new System.Windows.Forms.Panel();
            this.edULKE = new System.Windows.Forms.TextBox();
            this.chGercekKisi = new System.Windows.Forms.CheckBox();
            this.chEFATURA = new System.Windows.Forms.CheckBox();
            this.edTEL1 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.edVERGINOSU = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.edVERGIDAIRESI = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.edIlce = new System.Windows.Forms.TextBox();
            this.edIL = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.edADRES = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.edCARIADI = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.chMusKurumsal = new System.Windows.Forms.RadioButton();
            this.chMusMuhtelif = new System.Windows.Forms.RadioButton();
            this.edVergiNo = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.edIslemAdi = new System.Windows.Forms.TextBox();
            this.edSeriNo = new System.Windows.Forms.ComboBox();
            this.edIP = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btOdemeAl = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbEnterKiosk = new System.Windows.Forms.RadioButton();
            this.rbExitKiosk = new System.Windows.Forms.RadioButton();
            this.rbAskidaSatislar = new System.Windows.Forms.RadioButton();
            this.rbOdemeGunSonuDetayli = new System.Windows.Forms.RadioButton();
            this.rbAvansOdeme = new System.Windows.Forms.RadioButton();
            this.rbOdemeReboot = new System.Windows.Forms.RadioButton();
            this.rbOdemeParametre = new System.Windows.Forms.RadioButton();
            this.rbOdemeIptal = new System.Windows.Forms.RadioButton();
            this.rbOdemeGunSonu = new System.Windows.Forms.RadioButton();
            this.rbOdemeKK = new System.Windows.Forms.RadioButton();
            this.rbOdemeNakit = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.edTutar = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.imgFis = new System.Windows.Forms.PictureBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.edFisYazdir = new System.Windows.Forms.TextBox();
            this.rbFisYaz = new System.Windows.Forms.RadioButton();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.pnKurumsal.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgFis)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 271);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(716, 318);
            this.tabControl1.TabIndex = 9;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.mmLog);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(708, 292);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Log";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // mmLog
            // 
            this.mmLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mmLog.Location = new System.Drawing.Point(3, 3);
            this.mmLog.Multiline = true;
            this.mmLog.Name = "mmLog";
            this.mmLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.mmLog.Size = new System.Drawing.Size(702, 286);
            this.mmLog.TabIndex = 3;
            this.mmLog.WordWrap = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.edJsonGelen);
            this.tabPage2.Controls.Add(this.edJsonGiden);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(708, 292);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Json Veri";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // edJsonGelen
            // 
            this.edJsonGelen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.edJsonGelen.Location = new System.Drawing.Point(278, 3);
            this.edJsonGelen.Multiline = true;
            this.edJsonGelen.Name = "edJsonGelen";
            this.edJsonGelen.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.edJsonGelen.Size = new System.Drawing.Size(427, 286);
            this.edJsonGelen.TabIndex = 5;
            this.edJsonGelen.WordWrap = false;
            // 
            // edJsonGiden
            // 
            this.edJsonGiden.Dock = System.Windows.Forms.DockStyle.Left;
            this.edJsonGiden.Location = new System.Drawing.Point(3, 3);
            this.edJsonGiden.Multiline = true;
            this.edJsonGiden.Name = "edJsonGiden";
            this.edJsonGiden.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.edJsonGiden.Size = new System.Drawing.Size(275, 286);
            this.edJsonGiden.TabIndex = 4;
            this.edJsonGiden.WordWrap = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.edFisYazdir);
            this.panel3.Controls.Add(this.button2);
            this.panel3.Controls.Add(this.label10);
            this.panel3.Controls.Add(this.edIptalFisNo);
            this.panel3.Controls.Add(this.pnKurumsal);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.edVergiNo);
            this.panel3.Controls.Add(this.button1);
            this.panel3.Controls.Add(this.label6);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.edIslemAdi);
            this.panel3.Controls.Add(this.edSeriNo);
            this.panel3.Controls.Add(this.edIP);
            this.panel3.Controls.Add(this.label4);
            this.panel3.Controls.Add(this.btOdemeAl);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.edTutar);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(716, 271);
            this.panel3.TabIndex = 10;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(424, 93);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(137, 22);
            this.button2.TabIndex = 34;
            this.button2.Text = "Fatura Iptal";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(423, 56);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(121, 13);
            this.label10.TabIndex = 33;
            this.label10.Text = "İptal Edilecek Fatura No";
            // 
            // edIptalFisNo
            // 
            this.edIptalFisNo.Location = new System.Drawing.Point(425, 72);
            this.edIptalFisNo.Name = "edIptalFisNo";
            this.edIptalFisNo.Size = new System.Drawing.Size(136, 20);
            this.edIptalFisNo.TabIndex = 32;
            // 
            // pnKurumsal
            // 
            this.pnKurumsal.Controls.Add(this.edULKE);
            this.pnKurumsal.Controls.Add(this.chGercekKisi);
            this.pnKurumsal.Controls.Add(this.chEFATURA);
            this.pnKurumsal.Controls.Add(this.edTEL1);
            this.pnKurumsal.Controls.Add(this.label13);
            this.pnKurumsal.Controls.Add(this.edVERGINOSU);
            this.pnKurumsal.Controls.Add(this.label11);
            this.pnKurumsal.Controls.Add(this.edVERGIDAIRESI);
            this.pnKurumsal.Controls.Add(this.label12);
            this.pnKurumsal.Controls.Add(this.edIlce);
            this.pnKurumsal.Controls.Add(this.edIL);
            this.pnKurumsal.Controls.Add(this.label9);
            this.pnKurumsal.Controls.Add(this.edADRES);
            this.pnKurumsal.Controls.Add(this.label8);
            this.pnKurumsal.Controls.Add(this.edCARIADI);
            this.pnKurumsal.Controls.Add(this.label7);
            this.pnKurumsal.Enabled = false;
            this.pnKurumsal.Location = new System.Drawing.Point(12, 185);
            this.pnKurumsal.Name = "pnKurumsal";
            this.pnKurumsal.Size = new System.Drawing.Size(621, 77);
            this.pnKurumsal.TabIndex = 31;
            // 
            // edULKE
            // 
            this.edULKE.Location = new System.Drawing.Point(213, 53);
            this.edULKE.Name = "edULKE";
            this.edULKE.Size = new System.Drawing.Size(81, 20);
            this.edULKE.TabIndex = 28;
            this.edULKE.Text = "Turkiye";
            // 
            // chGercekKisi
            // 
            this.chGercekKisi.AutoSize = true;
            this.chGercekKisi.Location = new System.Drawing.Point(420, 10);
            this.chGercekKisi.Name = "chGercekKisi";
            this.chGercekKisi.Size = new System.Drawing.Size(80, 17);
            this.chGercekKisi.TabIndex = 27;
            this.chGercekKisi.Text = "Gerçek Kişi";
            this.chGercekKisi.UseVisualStyleBackColor = true;
            // 
            // chEFATURA
            // 
            this.chEFATURA.AutoSize = true;
            this.chEFATURA.Checked = true;
            this.chEFATURA.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chEFATURA.Location = new System.Drawing.Point(305, 9);
            this.chEFATURA.Name = "chEFATURA";
            this.chEFATURA.Size = new System.Drawing.Size(110, 17);
            this.chEFATURA.TabIndex = 26;
            this.chEFATURA.Text = "E Fatura Müşterisi";
            this.chEFATURA.UseVisualStyleBackColor = true;
            // 
            // edTEL1
            // 
            this.edTEL1.Location = new System.Drawing.Point(505, 7);
            this.edTEL1.Name = "edTEL1";
            this.edTEL1.Size = new System.Drawing.Size(89, 20);
            this.edTEL1.TabIndex = 25;
            this.edTEL1.Text = "08504731606";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(458, 10);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(43, 13);
            this.label13.TabIndex = 24;
            this.label13.Text = "Telefon";
            // 
            // edVERGINOSU
            // 
            this.edVERGINOSU.Location = new System.Drawing.Point(505, 53);
            this.edVERGINOSU.Name = "edVERGINOSU";
            this.edVERGINOSU.Size = new System.Drawing.Size(89, 20);
            this.edVERGINOSU.TabIndex = 23;
            this.edVERGINOSU.Text = "0320529864";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(458, 56);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(48, 13);
            this.label11.TabIndex = 22;
            this.label11.Text = "Vergi No";
            // 
            // edVERGIDAIRESI
            // 
            this.edVERGIDAIRESI.Location = new System.Drawing.Point(362, 53);
            this.edVERGIDAIRESI.Name = "edVERGIDAIRESI";
            this.edVERGIDAIRESI.Size = new System.Drawing.Size(89, 20);
            this.edVERGIDAIRESI.TabIndex = 21;
            this.edVERGIDAIRESI.Text = "016253";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(294, 56);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(66, 13);
            this.label12.TabIndex = 20;
            this.label12.Text = "Vergi Dairesi";
            // 
            // edIlce
            // 
            this.edIlce.Location = new System.Drawing.Point(126, 53);
            this.edIlce.Name = "edIlce";
            this.edIlce.Size = new System.Drawing.Size(81, 20);
            this.edIlce.TabIndex = 19;
            this.edIlce.Text = "Nilüfer";
            // 
            // edIL
            // 
            this.edIL.Location = new System.Drawing.Point(55, 53);
            this.edIL.Name = "edIL";
            this.edIL.Size = new System.Drawing.Size(65, 20);
            this.edIL.TabIndex = 17;
            this.edIL.Text = "Bursa";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(8, 56);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(12, 13);
            this.label9.TabIndex = 16;
            this.label9.Text = "Il";
            // 
            // edADRES
            // 
            this.edADRES.Location = new System.Drawing.Point(55, 29);
            this.edADRES.Name = "edADRES";
            this.edADRES.Size = new System.Drawing.Size(539, 20);
            this.edADRES.TabIndex = 15;
            this.edADRES.Text = "Alaaddinbey Mh. 623. Sk. Sam1 Plaza K:2 No:A19";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 32);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "Adres";
            // 
            // edCARIADI
            // 
            this.edCARIADI.Location = new System.Drawing.Point(55, 6);
            this.edCARIADI.Name = "edCARIADI";
            this.edCARIADI.Size = new System.Drawing.Size(232, 20);
            this.edCARIADI.TabIndex = 13;
            this.edCARIADI.Text = "Akmer Bilişim Otomasyon LTD";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 13);
            this.label7.TabIndex = 12;
            this.label7.Text = "Cari Adı";
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel5.Controls.Add(this.chMusKurumsal);
            this.panel5.Controls.Add(this.chMusMuhtelif);
            this.panel5.Location = new System.Drawing.Point(593, 10);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(116, 58);
            this.panel5.TabIndex = 30;
            // 
            // chMusKurumsal
            // 
            this.chMusKurumsal.AutoSize = true;
            this.chMusKurumsal.Location = new System.Drawing.Point(9, 33);
            this.chMusKurumsal.Name = "chMusKurumsal";
            this.chMusKurumsal.Size = new System.Drawing.Size(105, 17);
            this.chMusKurumsal.TabIndex = 1;
            this.chMusKurumsal.Text = "Kurumsal Müşteri";
            this.chMusKurumsal.UseVisualStyleBackColor = true;
            this.chMusKurumsal.Click += new System.EventHandler(this.chMusMuhtelif_Click);
            // 
            // chMusMuhtelif
            // 
            this.chMusMuhtelif.AutoSize = true;
            this.chMusMuhtelif.Checked = true;
            this.chMusMuhtelif.Location = new System.Drawing.Point(9, 7);
            this.chMusMuhtelif.Name = "chMusMuhtelif";
            this.chMusMuhtelif.Size = new System.Drawing.Size(99, 17);
            this.chMusMuhtelif.TabIndex = 0;
            this.chMusMuhtelif.TabStop = true;
            this.chMusMuhtelif.Text = "Muhtelif Müşteri";
            this.chMusMuhtelif.UseVisualStyleBackColor = true;
            this.chMusMuhtelif.Click += new System.EventHandler(this.chMusMuhtelif_Click);
            // 
            // edVergiNo
            // 
            this.edVergiNo.AutoCompleteCustomSource.AddRange(new string[] {
            "8450336698",
            "1740339632",
            "7810135782",
            "0730414744",
            "7990381785"});
            this.edVergiNo.FormattingEnabled = true;
            this.edVergiNo.Items.AddRange(new object[] {
            "8450336698",
            "1740339632",
            "7810135782",
            "0730414744",
            "7990381785"});
            this.edVergiNo.Location = new System.Drawing.Point(567, 92);
            this.edVergiNo.Name = "edVergiNo";
            this.edVergiNo.Size = new System.Drawing.Size(89, 21);
            this.edVergiNo.TabIndex = 29;
            this.edVergiNo.Text = "7810135782";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(659, 73);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(52, 42);
            this.button1.TabIndex = 28;
            this.button1.Text = "Turmob Sorgula";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(568, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(78, 13);
            this.label6.TabIndex = 27;
            this.label6.Text = "Vergi Numarası";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(422, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(139, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "Faturada Yazacak İşlem Adı";
            // 
            // edIslemAdi
            // 
            this.edIslemAdi.Location = new System.Drawing.Point(425, 22);
            this.edIslemAdi.Name = "edIslemAdi";
            this.edIslemAdi.Size = new System.Drawing.Size(136, 20);
            this.edIslemAdi.TabIndex = 24;
            this.edIslemAdi.Text = "Islem Ucreti";
            // 
            // edSeriNo
            // 
            this.edSeriNo.FormattingEnabled = true;
            this.edSeriNo.Items.AddRange(new object[] {
            "UN20W000973",
            "UN20W001085",
            "UN20W001087",
            "UN20W001083"});
            this.edSeriNo.Location = new System.Drawing.Point(70, 52);
            this.edSeriNo.Name = "edSeriNo";
            this.edSeriNo.Size = new System.Drawing.Size(123, 21);
            this.edSeriNo.TabIndex = 23;
            this.edSeriNo.Text = "UN20W000973";
            // 
            // edIP
            // 
            this.edIP.FormattingEnabled = true;
            this.edIP.Items.AddRange(new object[] {
            "192.168.7.25:4568",
            "192.168.7.26:4568",
            "192.168.7.27:4568",
            "192.168.7.28:4568",
            "192.168.7.29:4568"});
            this.edIP.Location = new System.Drawing.Point(70, 7);
            this.edIP.Name = "edIP";
            this.edIP.Size = new System.Drawing.Size(123, 21);
            this.edIP.TabIndex = 22;
            this.edIP.Text = "192.168.7.25:4568";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Seri No";
            // 
            // btOdemeAl
            // 
            this.btOdemeAl.Location = new System.Drawing.Point(16, 79);
            this.btOdemeAl.Name = "btOdemeAl";
            this.btOdemeAl.Size = new System.Drawing.Size(179, 63);
            this.btOdemeAl.TabIndex = 16;
            this.btOdemeAl.Text = "İşlem Yap";
            this.btOdemeAl.UseVisualStyleBackColor = true;
            this.btOdemeAl.Click += new System.EventHandler(this.btOdemeAl_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(163, 32);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(20, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "TL";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel2.Controls.Add(this.rbFisYaz);
            this.panel2.Controls.Add(this.rbEnterKiosk);
            this.panel2.Controls.Add(this.rbExitKiosk);
            this.panel2.Controls.Add(this.rbAskidaSatislar);
            this.panel2.Controls.Add(this.rbOdemeGunSonuDetayli);
            this.panel2.Controls.Add(this.rbAvansOdeme);
            this.panel2.Controls.Add(this.rbOdemeReboot);
            this.panel2.Controls.Add(this.rbOdemeParametre);
            this.panel2.Controls.Add(this.rbOdemeIptal);
            this.panel2.Controls.Add(this.rbOdemeGunSonu);
            this.panel2.Controls.Add(this.rbOdemeKK);
            this.panel2.Controls.Add(this.rbOdemeNakit);
            this.panel2.Location = new System.Drawing.Point(201, -1);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(218, 150);
            this.panel2.TabIndex = 14;
            // 
            // rbEnterKiosk
            // 
            this.rbEnterKiosk.AutoSize = true;
            this.rbEnterKiosk.Location = new System.Drawing.Point(117, 36);
            this.rbEnterKiosk.Name = "rbEnterKiosk";
            this.rbEnterKiosk.Size = new System.Drawing.Size(79, 17);
            this.rbEnterKiosk.TabIndex = 10;
            this.rbEnterKiosk.Text = "Enter Kiosk";
            this.rbEnterKiosk.UseVisualStyleBackColor = true;
            // 
            // rbExitKiosk
            // 
            this.rbExitKiosk.AutoSize = true;
            this.rbExitKiosk.Location = new System.Drawing.Point(117, 21);
            this.rbExitKiosk.Name = "rbExitKiosk";
            this.rbExitKiosk.Size = new System.Drawing.Size(71, 17);
            this.rbExitKiosk.TabIndex = 9;
            this.rbExitKiosk.Text = "Exit Kiosk";
            this.rbExitKiosk.UseVisualStyleBackColor = true;
            // 
            // rbAskidaSatislar
            // 
            this.rbAskidaSatislar.AutoSize = true;
            this.rbAskidaSatislar.Location = new System.Drawing.Point(116, 5);
            this.rbAskidaSatislar.Name = "rbAskidaSatislar";
            this.rbAskidaSatislar.Size = new System.Drawing.Size(109, 17);
            this.rbAskidaSatislar.TabIndex = 8;
            this.rbAskidaSatislar.Text = "Askıdaki Sat. List.";
            this.rbAskidaSatislar.UseVisualStyleBackColor = true;
            // 
            // rbOdemeGunSonuDetayli
            // 
            this.rbOdemeGunSonuDetayli.AutoSize = true;
            this.rbOdemeGunSonuDetayli.Location = new System.Drawing.Point(9, 110);
            this.rbOdemeGunSonuDetayli.Name = "rbOdemeGunSonuDetayli";
            this.rbOdemeGunSonuDetayli.Size = new System.Drawing.Size(108, 17);
            this.rbOdemeGunSonuDetayli.TabIndex = 7;
            this.rbOdemeGunSonuDetayli.Text = "Detaylı Gün Sonu";
            this.rbOdemeGunSonuDetayli.UseVisualStyleBackColor = true;
            // 
            // rbAvansOdeme
            // 
            this.rbAvansOdeme.AutoSize = true;
            this.rbAvansOdeme.Location = new System.Drawing.Point(9, 95);
            this.rbAvansOdeme.Name = "rbAvansOdeme";
            this.rbAvansOdeme.Size = new System.Drawing.Size(92, 17);
            this.rbAvansOdeme.TabIndex = 6;
            this.rbAvansOdeme.Text = "Avans Ödeme";
            this.rbAvansOdeme.UseVisualStyleBackColor = true;
            // 
            // rbOdemeReboot
            // 
            this.rbOdemeReboot.AutoSize = true;
            this.rbOdemeReboot.Location = new System.Drawing.Point(9, 79);
            this.rbOdemeReboot.Name = "rbOdemeReboot";
            this.rbOdemeReboot.Size = new System.Drawing.Size(96, 17);
            this.rbOdemeReboot.TabIndex = 5;
            this.rbOdemeReboot.Text = "Yeniden Başlat";
            this.rbOdemeReboot.UseVisualStyleBackColor = true;
            // 
            // rbOdemeParametre
            // 
            this.rbOdemeParametre.AutoSize = true;
            this.rbOdemeParametre.Location = new System.Drawing.Point(9, 63);
            this.rbOdemeParametre.Name = "rbOdemeParametre";
            this.rbOdemeParametre.Size = new System.Drawing.Size(85, 17);
            this.rbOdemeParametre.TabIndex = 4;
            this.rbOdemeParametre.Text = "Parametre Al";
            this.rbOdemeParametre.UseVisualStyleBackColor = true;
            // 
            // rbOdemeIptal
            // 
            this.rbOdemeIptal.AutoSize = true;
            this.rbOdemeIptal.Location = new System.Drawing.Point(9, 48);
            this.rbOdemeIptal.Name = "rbOdemeIptal";
            this.rbOdemeIptal.Size = new System.Drawing.Size(72, 17);
            this.rbOdemeIptal.TabIndex = 3;
            this.rbOdemeIptal.Text = "İşlem İptal";
            this.rbOdemeIptal.UseVisualStyleBackColor = true;
            // 
            // rbOdemeGunSonu
            // 
            this.rbOdemeGunSonu.AutoSize = true;
            this.rbOdemeGunSonu.Location = new System.Drawing.Point(9, 33);
            this.rbOdemeGunSonu.Name = "rbOdemeGunSonu";
            this.rbOdemeGunSonu.Size = new System.Drawing.Size(73, 17);
            this.rbOdemeGunSonu.TabIndex = 2;
            this.rbOdemeGunSonu.Text = "Gün Sonu";
            this.rbOdemeGunSonu.UseVisualStyleBackColor = true;
            // 
            // rbOdemeKK
            // 
            this.rbOdemeKK.AutoSize = true;
            this.rbOdemeKK.Checked = true;
            this.rbOdemeKK.Location = new System.Drawing.Point(9, 18);
            this.rbOdemeKK.Name = "rbOdemeKK";
            this.rbOdemeKK.Size = new System.Drawing.Size(73, 17);
            this.rbOdemeKK.TabIndex = 1;
            this.rbOdemeKK.TabStop = true;
            this.rbOdemeKK.Text = "Kredi Kartı";
            this.rbOdemeKK.UseVisualStyleBackColor = true;
            // 
            // rbOdemeNakit
            // 
            this.rbOdemeNakit.AutoSize = true;
            this.rbOdemeNakit.Location = new System.Drawing.Point(9, 3);
            this.rbOdemeNakit.Name = "rbOdemeNakit";
            this.rbOdemeNakit.Size = new System.Drawing.Size(50, 17);
            this.rbOdemeNakit.TabIndex = 0;
            this.rbOdemeNakit.Text = "Nakit";
            this.rbOdemeNakit.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Tutar";
            // 
            // edTutar
            // 
            this.edTutar.Location = new System.Drawing.Point(70, 30);
            this.edTutar.Name = "edTutar";
            this.edTutar.Size = new System.Drawing.Size(89, 20);
            this.edTutar.TabIndex = 11;
            this.edTutar.Text = "1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Cihaz IP";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.imgFis);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(716, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(255, 589);
            this.panel1.TabIndex = 12;
            // 
            // imgFis
            // 
            this.imgFis.BackColor = System.Drawing.Color.White;
            this.imgFis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.imgFis.Location = new System.Drawing.Point(0, 48);
            this.imgFis.Name = "imgFis";
            this.imgFis.Size = new System.Drawing.Size(255, 541);
            this.imgFis.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imgFis.TabIndex = 1;
            this.imgFis.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.pictureBox1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(255, 48);
            this.panel4.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::PavoTest.Properties.Resources.teknopass_x90a;
            this.pictureBox1.Location = new System.Drawing.Point(16, 15);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(90, 19);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // edFisYazdir
            // 
            this.edFisYazdir.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.edFisYazdir.Location = new System.Drawing.Point(424, 121);
            this.edFisYazdir.Multiline = true;
            this.edFisYazdir.Name = "edFisYazdir";
            this.edFisYazdir.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.edFisYazdir.Size = new System.Drawing.Size(283, 61);
            this.edFisYazdir.TabIndex = 35;
            this.edFisYazdir.Text = resources.GetString("edFisYazdir.Text");
            this.edFisYazdir.WordWrap = false;
            // 
            // rbFisYaz
            // 
            this.rbFisYaz.AutoSize = true;
            this.rbFisYaz.Location = new System.Drawing.Point(117, 52);
            this.rbFisYaz.Name = "rbFisYaz";
            this.rbFisYaz.Size = new System.Drawing.Size(70, 17);
            this.rbFisYaz.TabIndex = 11;
            this.rbFisYaz.Text = "Fiş Yazdır";
            this.rbFisYaz.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 589);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "TeknoPass PAVO Cihaz Test v26/1110";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.pnKurumsal.ResumeLayout(false);
            this.pnKurumsal.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgFis)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox mmLog;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox edJsonGelen;
        private System.Windows.Forms.TextBox edJsonGiden;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btOdemeAl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rbOdemeKK;
        private System.Windows.Forms.RadioButton rbOdemeNakit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox edTutar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox edIP;
        private System.Windows.Forms.ComboBox edSeriNo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox imgFis;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RadioButton rbOdemeGunSonu;
        private System.Windows.Forms.RadioButton rbOdemeIptal;
        private System.Windows.Forms.RadioButton rbOdemeParametre;
        private System.Windows.Forms.RadioButton rbOdemeReboot;
        private System.Windows.Forms.TextBox edIslemAdi;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox edVergiNo;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel pnKurumsal;
        private System.Windows.Forms.TextBox edIL;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox edADRES;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox edCARIADI;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.RadioButton chMusKurumsal;
        private System.Windows.Forms.RadioButton chMusMuhtelif;
        private System.Windows.Forms.TextBox edVERGINOSU;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox edVERGIDAIRESI;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox edIlce;
        private System.Windows.Forms.TextBox edTEL1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.CheckBox chEFATURA;
        private System.Windows.Forms.CheckBox chGercekKisi;
        private System.Windows.Forms.TextBox edULKE;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox edIptalFisNo;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.RadioButton rbAvansOdeme;
        private System.Windows.Forms.RadioButton rbOdemeGunSonuDetayli;
        private System.Windows.Forms.RadioButton rbAskidaSatislar;
        private System.Windows.Forms.RadioButton rbExitKiosk;
        private System.Windows.Forms.RadioButton rbEnterKiosk;
        private System.Windows.Forms.TextBox edFisYazdir;
        private System.Windows.Forms.RadioButton rbFisYaz;
    }
}

