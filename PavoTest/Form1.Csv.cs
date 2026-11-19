using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PavoTest
{
    public partial class Form1
    {
        private static string KolonAdi(string ad)
        {
            return string.Concat(ad.Replace('ı', 'i').ToUpperInvariant().Replace('İ', 'I').Replace('Ş', 'S')
                .Replace('Ü', 'U').Replace('Ö', 'O').Replace('Ç', 'C').Replace('Ğ', 'G')
                .Where(char.IsLetterOrDigit));
        }

        internal static List<PavoPay.SatisKalemi> CsvKalemleriOku(string dosya)
        {
            string ilkSatir;
            using (var reader = new StreamReader(dosya, Encoding.UTF8, true))
            {
                do { ilkSatir = reader.ReadLine(); } while (ilkSatir != null && string.IsNullOrWhiteSpace(ilkSatir));
            }
            if (ilkSatir == null) throw new ArgumentException("data.csv boş.");
            using (var parser = new TextFieldParser(dosya, Encoding.UTF8, true))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(ilkSatir.Contains(";") ? ";" : ",");
                parser.HasFieldsEnclosedInQuotes = true;
                parser.TrimWhiteSpace = true;
                string[] basliklar = parser.ReadFields().Select(KolonAdi).ToArray();
                int ad = Array.FindIndex(basliklar, x => x == "ISLEMADI" || x == "ISLEM" || x == "AD" || x == "NAME");
                int tutar = Array.FindIndex(basliklar, x => x == "TUTAR" || x == "TUTARTL" || x == "AMOUNT");
                int kdv = Array.FindIndex(basliklar, x => x == "KDV" || x == "KDVORANI" || x == "VAT");
                if (ad < 0 || tutar < 0)
                    throw new ArgumentException("data.csv başlığında İşlem Adı ve Tutar sütunları bulunmalıdır. KDV sütunu isteğe bağlıdır.");
                var sonuc = new List<PavoPay.SatisKalemi>();
                while (!parser.EndOfData)
                {
                    long satir = parser.LineNumber;
                    string[] alanlar = parser.ReadFields();
                    if (alanlar.All(string.IsNullOrWhiteSpace)) continue;
                    if (alanlar.Length != basliklar.Length)
                        throw new ArgumentException("data.csv satır " + satir + ": sütun sayısı başlıkla eşleşmiyor.");
                    decimal bedel;
                    int oran = 20;
                    if (string.IsNullOrWhiteSpace(alanlar[ad]) || !TutarOku(alanlar[tutar], out bedel))
                        throw new ArgumentException("data.csv satır " + satir + ": işlem adı veya tutar geçersiz.");
                    if (kdv >= 0 && !string.IsNullOrWhiteSpace(alanlar[kdv]) &&
                        (!int.TryParse(alanlar[kdv], NumberStyles.None, CultureInfo.InvariantCulture, out oran) || oran < 0 || oran > 100))
                        throw new ArgumentException("data.csv satır " + satir + ": KDV 0 ile 100 arasında tam sayı olmalıdır.");
                    sonuc.Add(new PavoPay.SatisKalemi(alanlar[ad], bedel, oran));
                }
                if (sonuc.Count == 0) throw new ArgumentException("data.csv içinde tahsilat kaydı bulunamadı.");
                // Toplamın gönderilebilir olduğunu listeyi değiştirmeden doğrula.
                sonuc.Sum(x => x.Tutar);
                return sonuc;
            }
        }

        private void CsvListesiniYukle(string dosya)
        {
            if (!File.Exists(dosya)) return;
            try
            {
                var kalemler = CsvKalemleriOku(dosya);
                tahsilatListesi.Rows.Clear();
                foreach (var kalem in kalemler)
                    tahsilatListesi.Rows.Add(kalem.Ad, kalem.Tutar.ToString(CultureInfo.CurrentCulture), kalem.Kdv);
                topluTahsilat.Checked = true;
                ToplamGuncelle();
            }
            catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException ||
                ex is ArgumentException || ex is MalformedLineException || ex is OverflowException)
            {
                MessageBox.Show("data.csv yüklenemedi: " + ex.Message, "Toplu tahsilat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
