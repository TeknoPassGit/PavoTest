using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PavoTest
{
    public partial class Form1 : Form
    {
        PavoPay myPay;

        [DllImport("kernel32")]
        private static extern long WritePrivateProfileString(string section,
            string key, string val, string filePath);
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section,
                 string key, string def, StringBuilder retVal,
            int size, string filePath);
        
        public DataTable db_pavo = new DataTable();

        public Form1()
        {
            InitializeComponent();
            TopluTahsilatHazirla();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            myPay = new PavoPay();
            myPay.prmUrlYanitKaydet = true;
            myPay.prmLogKlasor = Application.StartupPath + "";

            db_init();

            // Tasarımcıdaki örnek IP/seri no'lar, hafızadaki cihazlardan sonra listede kalır.
            varsayilanIpler = edIP.Items.Cast<object>().Select(x => x.ToString()).ToList();
            varsayilanSeriNolar = edSeriNo.Items.Cast<object>().Select(x => x.ToString()).ToList();
            edIP.SelectedIndexChanged += edIP_SelectedIndexChanged;
            edIP.KeyDown += edIP_KeyDown;

            IniFile("READ");
            CsvListesiniYukle(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data.csv"));
        }

        #region Cihaz hafızası

        // Yanıt veren her cihaz (IP + seri no) listenin en üstüne eklenir ve PavoTest.ini [cihazlar] bölümüne yazılır.
        // Program tekrar açıldığında açılır listelerde bu cihazlar gelir; listeden IP seçilince seri no da dolar.

        private class Cihaz
        {
            public string Ip;
            public string SeriNo;
        }

        private const int MaxCihaz = 20;
        private const string CihazBolum = "cihazlar";
        private readonly List<Cihaz> cihazlar = new List<Cihaz>();
        private List<string> varsayilanIpler = new List<string>();
        private List<string> varsayilanSeriNolar = new List<string>();
        private bool hafizaVar;   // INI'de [cihazlar] bölümü var mı (yoksa ilk açılış)

        private string IniDosyasi => Path.Combine(Application.StartupPath, "PavoTest.ini");

        private void CihazListesiOku(string filepath)
        {
            cihazlar.Clear();
            string adetStr = File.Exists(filepath) ? IniReadValue(filepath, CihazBolum, "adet") : "";
            hafizaVar = adetStr != "";

            if (hafizaVar)
            {
                int adet;
                int.TryParse(adetStr, out adet);

                for (int i = 1; i <= Math.Min(adet, MaxCihaz); i++)
                {
                    string[] parca = IniReadValue(filepath, CihazBolum, "cihaz" + i).Split('|');
                    CihazEkle(parca[0], parca.Length > 1 ? parca[1] : "");
                }
            }
            else
            {
                // İlk açılış veya eski sürümden kalan INI: son kullanılan cihaz + tasarımcıdaki örnekler hafızaya alınır.
                // Bundan sonra listenin tek kaynağı INI'dir (silinen örnekler geri gelmez).
                if (File.Exists(filepath))
                    CihazEkle(IniReadValue(filepath, "db", "ipdres"), IniReadValue(filepath, "db", "serino"));
                foreach (string ip in varsayilanIpler) CihazEkle(ip, "");
            }

            CihazListesiDoldur();
        }

        private void CihazEkle(string ip, string seriNo)
        {
            ip = (ip ?? "").Trim();
            if (ip == "" || cihazlar.Count >= MaxCihaz || cihazlar.Any(c => AyniIp(c.Ip, ip))) return;
            cihazlar.Add(new Cihaz { Ip = ip, SeriNo = (seriNo ?? "").Trim() });
        }

        private void CihazListesiYaz(string filepath)
        {
            hafizaVar = true;
            IniWriteValue(filepath, CihazBolum, "adet", cihazlar.Count.ToString());
            for (int i = 1; i <= MaxCihaz; i++)
            {
                // Listede olmayan eski satırlar silinir (null değer anahtarı kaldırır).
                string deger = i <= cihazlar.Count ? cihazlar[i - 1].Ip + "|" + cihazlar[i - 1].SeriNo : null;
                IniWriteValue(filepath, CihazBolum, "cihaz" + i, deger);
            }
        }

        private void CihazListesiDoldur()
        {
            string ip = edIP.Text;
            string seriNo = edSeriNo.Text;

            edIP.BeginUpdate();
            edIP.Items.Clear();
            foreach (Cihaz c in cihazlar) edIP.Items.Add(c.Ip);
            edIP.EndUpdate();

            // Seri no listesi: hafızadaki cihazların seri no'ları (ilk açılışta tasarımcı örnekleri de).
            edSeriNo.BeginUpdate();
            edSeriNo.Items.Clear();
            foreach (string s in cihazlar.Select(c => c.SeriNo).Concat(hafizaVar ? Enumerable.Empty<string>() : varsayilanSeriNolar))
            {
                if (s != "" && !edSeriNo.Items.Contains(s)) edSeriNo.Items.Add(s);
            }
            edSeriNo.EndUpdate();

            edIP.Text = ip;
            edSeriNo.Text = seriNo;
        }

        /// <summary>Cihazı listenin en üstüne alır ve INI'ye hemen yazar.</summary>
        private void CihazHatirla(string ip, string seriNo)
        {
            ip = (ip ?? "").Trim();
            seriNo = (seriNo ?? "").Trim();
            if (ip == "") return;

            if (cihazlar.Count > 0 && AyniIp(cihazlar[0].Ip, ip) && cihazlar[0].SeriNo == seriNo) return;

            cihazlar.RemoveAll(c => AyniIp(c.Ip, ip));
            cihazlar.Insert(0, new Cihaz { Ip = ip, SeriNo = seriNo });
            if (cihazlar.Count > MaxCihaz) cihazlar.RemoveRange(MaxCihaz, cihazlar.Count - MaxCihaz);

            CihazListesiDoldur();
            IniFile("WRITE");
        }

        private static bool AyniIp(string a, string b)
        {
            return string.Equals((a ?? "").Trim(), (b ?? "").Trim(), StringComparison.OrdinalIgnoreCase);
        }

        private void edIP_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cihaz c = cihazlar.FirstOrDefault(x => AyniIp(x.Ip, edIP.SelectedItem?.ToString()));
            if (c != null && c.SeriNo != "") edSeriNo.Text = c.SeriNo;
        }

        // Liste açıkken bir cihazın üzerindeyken Delete: cihazı hafızadan siler.
        private void edIP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Delete || !edIP.DroppedDown || edIP.SelectedIndex < 0) return;

            string ip = edIP.Items[edIP.SelectedIndex].ToString();
            cihazlar.RemoveAll(c => AyniIp(c.Ip, ip));
            edIP.DroppedDown = false;
            CihazListesiDoldur();

            // Silinen cihaz kutuda kalmasın (yoksa son kullanılan olarak tekrar kaydedilir).
            if (AyniIp(edIP.Text, ip))
            {
                edIP.Text = cihazlar.Count > 0 ? cihazlar[0].Ip : "";
                edSeriNo.Text = cihazlar.Count > 0 ? cihazlar[0].SeriNo : "";
            }
            IniFile("WRITE");
            LogYaz("Cihaz listeden silindi: " + ip);
            e.Handled = true;
        }

        #endregion

        public void db_init()
        {
            db_pavo.Clear();
            db_pavo.Columns.Add("SATISNO", typeof(System.String));
            db_pavo.Columns.Add("FATURANO", typeof(System.String));
            db_pavo.Columns.Add("TARIHSAAT", typeof(System.String));
            db_pavo.Columns.Add("URUNADI", typeof(System.String));
            db_pavo.Columns.Add("TUTAR", typeof(System.Double));
            db_pavo.Columns.Add("KDV", typeof(System.Double));
            db_pavo.Columns.Add("BANKAADI", typeof(System.String));
            db_pavo.Columns.Add("SERINO", typeof(System.String));
            db_pavo.Columns.Add("PLAKA", typeof(System.String));
            db_pavo.Columns.Add("QRLINK", typeof(System.String));
            db_pavo.Columns.Add("KARTNO", typeof(System.String));
            db_pavo.Columns.Add("BATCHNO", typeof(System.String));                      
        }

            private void btOdemeAl_Click(object sender, EventArgs e)
        {
            int islem = 0;
            if (rbOdemeKK.Checked) islem = 1;
            if (rbOdemeHgs.Checked) islem = PavoPay.HgsIslemTipi;
            if (rbOdemeGunSonu.Checked) islem = 2;
            if (rbOdemeIptal.Checked) islem = 3;
            if (rbOdemeParametre.Checked) islem = 4;
            if (rbOdemeReboot.Checked) islem = 5;
            if (rbAvansOdeme.Checked) islem = 8;
            if (rbOdemeGunSonuDetayli.Checked) islem = 9;
            if (rbAskidaSatislar.Checked) islem = 10;
            if (rbExitKiosk.Checked) islem = 11;
            if (rbEnterKiosk.Checked) islem = 12;
            if (rbFisYaz.Checked) islem = 13;

            double tahsilatTutari = myPay.StrToFloatDef(edTutar.Text, 0);
            myPay.SatisKalemleri = new List<PavoPay.SatisKalemi>();
            if (islem == 0 || islem == 1 || islem == PavoPay.HgsIslemTipi)
            {
                try
                {
                    List<PavoPay.SatisKalemi> kalemler;
                    if (topluTahsilat.Checked)
                    {
                        tahsilatListesi.EndEdit();
                        kalemler = KalemleriOku();
                    }
                    else
                    {
                        decimal tutar;
                        if (string.IsNullOrWhiteSpace(edIslemAdi.Text) || !TutarOku(edTutar.Text, out tutar))
                            throw new ArgumentException("İşlem adı ve sıfırdan büyük, en fazla iki ondalıklı tutar giriniz.");
                        kalemler = new List<PavoPay.SatisKalemi> { new PavoPay.SatisKalemi(edIslemAdi.Text.Trim(), tutar) };
                    }
                    tahsilatTutari = (double)kalemler.Sum(x => x.Tutar);
                    myPay.SatisKalemleri = kalemler;
                }
                catch (Exception ex) when (ex is ArgumentException || ex is OverflowException)
                {
                    MessageBox.Show(ex is OverflowException ? "Toplam tutar çok büyük." : ex.Message, "Tahsilat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            myPay.toplamhatasayac = 3;
            myPay.prmpavoislemucreti = edIslemAdi.Text;
            myPay.prmAsama = "PAIR";
            myPay.prmislemadi = edIslemAdi.Text;
            myPay.prmTranDate = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + ".000001";
            myPay.prmTranNo = 1;       
            myPay.fisyazitems.Clear();
            myPay.fisyazitems = new List<string>(edFisYazdir.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None));


            if (chMusMuhtelif.Checked)
            {
                myPay.secilenCari.KNO = 0;
                myPay.secilenCari.CARIADI = "";
                myPay.secilenCari.ULKE = "";
                myPay.secilenCari.IL = "";
                myPay.secilenCari.ILCE = "";
                myPay.secilenCari.ADRES = "";
                myPay.secilenCari.TEL1 = "";
                myPay.secilenCari.VERGIDAIRESI = "";
                myPay.secilenCari.VERGINO = "";
            }
            else
            {
                myPay.secilenCari.MUSTERITURU = 2;
                if(chGercekKisi.Checked) myPay.secilenCari.MUSTERITURU = 1;
                myPay.secilenCari.KNO = 1;
                myPay.secilenCari.CARIADI = edCARIADI.Text;
                myPay.secilenCari.ADI = edCARIADI.Text;
                myPay.secilenCari.SOYADI = edCARIADI.Text;
                myPay.secilenCari.ULKE = edULKE.Text;
                myPay.secilenCari.IL = edIL.Text;
                myPay.secilenCari.ILCE = edIlce.Text;
                myPay.secilenCari.ADRES = edADRES.Text;
                myPay.secilenCari.TEL1 = edTEL1.Text;
                myPay.secilenCari.VERGIDAIRESI = edVERGIDAIRESI.Text;
                myPay.secilenCari.VERGINO = edVergiNo.Text;
                myPay.secilenCari.EFATURA = chEFATURA.Checked;
            }

            myPay.OdemeAl(edIP.Text, islem, tahsilatTutari, edSeriNo.Text, PavoYanit);
        }

        public void IniWriteValue(string path, string Section, string Key, string Value)
        {
            WritePrivateProfileString(Section, Key, Value, path);
        }

        public string IniReadValue(string path, string Section, string Key)
        {
            StringBuilder temp = new StringBuilder(255);
            int i = GetPrivateProfileString(Section, Key, "", temp,
                                            255, path);
            return temp.ToString();
        }

        public void IniFile(string protype)
        {
            string filepath = IniDosyasi;

            if (protype == "READ")
            {
                if (File.Exists(filepath))
                {
                    string s = "";
                    s = IniReadValue(filepath, "db", "serino");
                    if (s != "") edSeriNo.Text = s;

                    s = IniReadValue(filepath, "db", "ipdres");
                    if (s != "") edIP.Text = s;

                    s = IniReadValue(filepath, "db", "islemadi");
                    if (s != "") edIslemAdi.Text = s;
                }
                else
                {
                    LogYaz("Ayar dosyası yok, ilk kapanışta oluşturulacak: " + filepath);
                }

                CihazListesiOku(filepath);
            }

            // Not: Eski sürüm dosya yoksa yazmayı da atlıyordu; yeni kurulumda ayarlar hiç kaydedilmiyordu.
            // WritePrivateProfileString dosyayı kendisi oluşturur.
            if (protype == "WRITE")
            {
                IniWriteValue(filepath, "db", "serino", edSeriNo.Text);
                IniWriteValue(filepath, "db", "ipdres", edIP.Text);
                IniWriteValue(filepath, "db", "islemadi", edIslemAdi.Text);
                CihazListesiYaz(filepath);
            }
        }


        private void PavoYanit(string gelen)
        {                      
            edJsonGiden.Invoke((MethodInvoker)(() => edJsonGiden.Text += "\n" ));
            edJsonGiden.Invoke((MethodInvoker)(() => edJsonGiden.Text += myPay.prmGidenJson));
            edJsonGelen.Invoke((MethodInvoker)(() => edJsonGelen.Text += "\n"));
            edJsonGelen.Invoke((MethodInvoker)(() => edJsonGelen.Text += gelen));

            if(gelen.Contains("GIDEN;"))
            {
                return;
            }

            // Cihaz yanıt verdiyse (cihaz hata dönse bile) gerçek bir cihazdır: hafızaya al.
            // Bu metot arka plan iş parçacığından çağrılır; liste işlemleri UI iş parçacığında yapılır.
            if (!myPay.IletisimHatasi)
            {
                string ip = myPay.prmCihazIp, seriNo = myPay.prmCihazSeriNo;
                edIP.BeginInvoke((MethodInvoker)(() => CihazHatirla(ip, seriNo)));
            }

            string GonderimHata = myPay.prmAsama + " : " + (myPay.SonSonuc.HasError == false ? "ILETISIM_OK;" : "ILETISIM_HATA;") + myPay.SonSonuc.Message;
            string SatisDetay = "";

            if(myPay.prmAsama == "ODEME")
            {
                if(myPay.SonSonuc.Data != null)
                {
                    SatisDetay = "ODEME_HATA;0;0";

                    if (myPay.SonSonuc.Data.FinancialDocuments != null)
                    {
                        if (myPay.SonSonuc.Data.FinancialDocuments.Count > 0)
                        {
                            SatisDetay = "ODEME_OK;" + myPay.SonSonuc.Data.FinancialDocuments[0].Id.ToString() + ";" + myPay.SonSonuc.Data.FinancialDocuments[0].InvoiceNo;
                        }
                    }

                    if (myPay.SonSonuc.Data.AddedPayments != null)
                    {
                        if(myPay.SonSonuc.Data.AddedPayments.Count > 0)
                        {
                            if(myPay.SonSonuc.Data.AddedPayments[0].OnlinePayment != null)
                            {
                                if(myPay.SonSonuc.Data.AddedPayments[0].OnlinePayment.ResponseCode != null)
                                {
                                    SatisDetay += ";" + myPay.SonSonuc.Data.AddedPayments[0].OnlinePayment.ResponseCode;
                                    SatisDetay += ";" + myPay.SonSonuc.Data.AddedPayments[0].OnlinePayment.ResponseDescription;                                   
                                }
                            }
                        }
                    }

                    if (myPay.SonSonuc.Data.Errors != null)
                    {
                        if (myPay.SonSonuc.Data.Errors.Count > 0)
                        {
                            GonderimHata += myPay.SonSonuc.Data.Errors[0] + ";0";
                        }
                    }


                    if (myPay.SonSonuc.Data.CustomerReceiptImage != null)
                    {
                        if (myPay.SonSonuc.Data.CustomerReceiptImage != "")
                        {
                            byte[] bytes = Convert.FromBase64String(myPay.SonSonuc.Data.CustomerReceiptImage);

                            using (MemoryStream ms = new MemoryStream(bytes))
                            {
                                imgFis.Image = Image.FromStream(ms);
                            }
                        }
                    }
                }

                GonderimHata += (myPay.SonSonuc.Data == null ? "0;0" : myPay.SonSonuc.Data.Id.ToString() + ";" + myPay.SonSonuc.Data.SaleNumber);
            }

            LogYaz(GonderimHata + " " + SatisDetay);
        }

        private void LogYaz(string gelen)
        {
            mmLog.Invoke((MethodInvoker)(() => mmLog.AppendText("[" + DateTime.Now.ToString() + "] " + gelen + Environment.NewLine) ));
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            IniFile("WRITE");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            myPay.prmpavoislemucreti = edIslemAdi.Text;
            myPay.prmAsama = "PAIR";
            myPay.prmTranDate = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + ".000001";
            myPay.prmTranNo = 1;
            myPay.prmvkn = edVergiNo.Text;

            myPay.OdemeAl(edIP.Text, 6, 1, edSeriNo.Text, PavoYanit);
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {            
        }

        private void chMusMuhtelif_Click(object sender, EventArgs e)
        {
            pnKurumsal.Enabled = chMusKurumsal.Checked;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if(edIptalFisNo.Text == "")
            {
                MessageBox.Show("İptal edilecek fatura numarasını giriniz!");
                return;
            }

            myPay.prmpavoislemucreti = edIslemAdi.Text;
            myPay.prmAsama = "PAIR";
            myPay.prmTranDate = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + ".000001";
            myPay.prmTranNo = 1;
            myPay.iptalFaturano = edIptalFisNo.Text;

            myPay.OdemeAl(edIP.Text, 7, 1, edSeriNo.Text, PavoYanit);
        }
    }


}
