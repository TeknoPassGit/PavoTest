using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace PavoTest
{
    public partial class Form1
    {
        private DataGridView tahsilatListesi;
        private CheckBox topluTahsilat;
        private Label toplamEtiketi;
        private RadioButton rbOdemeHgs;

        private void TopluTahsilatHazirla()
        {
            SuspendLayout();
            ClientSize = new Size(ClientSize.Width, ClientSize.Height + 250);
            MinimumSize = Size;
            panel3.Height += 250;
            rbOdemeHgs = new RadioButton { Text = "HGS", AutoSize = true, Location = new Point(117, 73) };
            panel2.Controls.Add(rbOdemeHgs);
            var grup = new GroupBox
            {
                Text = "Toplu tahsilat kalemleri", Location = new Point(8, 275),
                Size = new Size(panel3.Width - 16, 240),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            topluTahsilat = new CheckBox { Text = "Listeyi kullan (nakit / kredi kartı / HGS)", AutoSize = true, Location = new Point(10, 20) };
            toplamEtiketi = new Label { AutoSize = true, Location = new Point(10, 215) };
            tahsilatListesi = new DataGridView
            {
                Name = "tahsilatListesi", Location = new Point(10, 45), Size = new Size(grup.Width - 20, 163),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                AllowUserToAddRows = true, AllowUserToDeleteRows = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                BackgroundColor = Color.White, Enabled = false, RowHeadersWidth = 28
            };
            tahsilatListesi.Columns.Add(new DataGridViewTextBoxColumn { Name = "IslemAdi", HeaderText = "İşlem adı", FillWeight = 60, SortMode = DataGridViewColumnSortMode.NotSortable });
            tahsilatListesi.Columns.Add(new DataGridViewTextBoxColumn { Name = "Tutar", HeaderText = "Tutar (TL)", FillWeight = 25, SortMode = DataGridViewColumnSortMode.NotSortable });
            tahsilatListesi.Columns.Add(new DataGridViewTextBoxColumn { Name = "Kdv", HeaderText = "KDV (%)", FillWeight = 15, SortMode = DataGridViewColumnSortMode.NotSortable });
            tahsilatListesi.RowsAdded += (s, e) =>
            {
                for (int i = e.RowIndex; i < e.RowIndex + e.RowCount; i++)
                    if (tahsilatListesi.Rows[i].Cells[2].Value == null)
                        tahsilatListesi.Rows[i].Cells[2].Value = 20;
            };
            for (int i = 0; i < 6; i++) tahsilatListesi.Rows.Add();
            var sil = new Button { Text = "Seçileni sil", Location = new Point(10, 17), Size = new Size(95, 24), Anchor = AnchorStyles.Top | AnchorStyles.Right };
            sil.Left = grup.Width - 210;
            var temizle = new Button { Text = "Listeyi temizle", Location = new Point(grup.Width - 110, 17), Size = new Size(100, 24), Anchor = AnchorStyles.Top | AnchorStyles.Right };
            sil.Click += (s, e) =>
            {
                foreach (DataGridViewRow row in tahsilatListesi.SelectedRows.Cast<DataGridViewRow>().ToArray())
                    if (!row.IsNewRow) tahsilatListesi.Rows.Remove(row);
            };
            temizle.Click += (s, e) => tahsilatListesi.Rows.Clear();
            topluTahsilat.CheckedChanged += (s, e) =>
            {
                tahsilatListesi.Enabled = sil.Enabled = temizle.Enabled = topluTahsilat.Checked;
                edTutar.Enabled = edIslemAdi.Enabled = !topluTahsilat.Checked;
            };
            sil.Enabled = temizle.Enabled = false;
            tahsilatListesi.CellValueChanged += (s, e) => ToplamGuncelle();
            tahsilatListesi.RowsRemoved += (s, e) => ToplamGuncelle();
            grup.Controls.AddRange(new Control[] { topluTahsilat, tahsilatListesi, toplamEtiketi, sil, temizle });
            panel3.Controls.Add(grup);
            ToplamGuncelle();
            ResumeLayout(true);
        }

        internal static bool TutarOku(string text, out decimal tutar)
        {
            // Binlik ayırıcı kabul edilmez; hem 12,50 hem 12.50 aynı tutardır.
            return decimal.TryParse((text ?? "").Trim().Replace(',', '.'),
                NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign,
                CultureInfo.InvariantCulture, out tutar)
                && tutar > 0 && decimal.Round(tutar, 2) == tutar;
        }

        private List<PavoPay.SatisKalemi> KalemleriOku()
        {
            var kalemler = new List<PavoPay.SatisKalemi>();
            foreach (DataGridViewRow row in tahsilatListesi.Rows)
            {
                if (row.IsNewRow) continue;
                string ad = Convert.ToString(row.Cells[0].Value).Trim();
                string text = Convert.ToString(row.Cells[1].Value).Trim();
                if (ad == "" && text == "") continue;
                decimal tutar;
                if (ad == "" || !TutarOku(text, out tutar))
                    throw new ArgumentException((row.Index + 1) + ". satır: işlem adı ve sıfırdan büyük, en fazla iki ondalıklı tutar giriniz.");
                int kdv;
                if (!int.TryParse(Convert.ToString(row.Cells[2].Value).Trim(), NumberStyles.None, CultureInfo.InvariantCulture, out kdv) || kdv < 0 || kdv > 100)
                    throw new ArgumentException((row.Index + 1) + ". satır: KDV değerini 0 ile 100 arasında tam sayı olarak giriniz.");
                kalemler.Add(new PavoPay.SatisKalemi(ad, tutar, kdv));
            }
            if (kalemler.Count == 0) throw new ArgumentException("Toplu tahsilat için en az bir kayıt giriniz.");
            return kalemler;
        }

        private void ToplamGuncelle()
        {
            try
            {
                var kalemler = KalemleriOku();
                toplamEtiketi.Text = kalemler.Count + " kayıt — Toplam: " + kalemler.Sum(x => x.Tutar).ToString("N2") + " TL";
            }
            catch (ArgumentException) { toplamEtiketi.Text = "İşlem adı ve tutar giriniz. Boş satırlar gönderilmez."; }
            catch (OverflowException) { toplamEtiketi.Text = "Toplam tutar çok büyük."; }
        }
    }
}
