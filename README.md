# PavoTest

TeknoPass PAVO ödeme cihazı test aracı (Windows Forms, .NET Framework 4.8).
Cihazla yerel ağ üzerinden HTTP/JSON ile konuşur (`http://<cihaz-ip>:4568/<Endpoint>`).

## Özellikler

- Satış: nakit, kredi kartı, avans ödeme; muhtelif veya faturalı (kurumsal / gerçek kişi) müşteri
- Gün sonu, detaylı gün sonu (X rapor), parametre al, yeniden başlat
- İşlem iptal (AbortCurrentPayment), fatura iptal (CancelSale)
- Turmob sorgulama (VKN/TCKN), askıdaki satışlar, kiosk moduna gir/çık, fiş yazdırma
- Giden/gelen JSON görüntüleme, fiş görüntüsü (base64)
- **Cihaz hafızası:** yanıt veren her cihaz (IP + seri no) açılır listenin en üstüne eklenir ve
  program kapatılıp açıldığında listede gelir. Listeden IP seçilince seri no otomatik dolar.
  Liste açıkken ok tuşlarıyla bir cihazın üzerine gelip `Delete` tuşuna basmak onu listeden siler.

Her işlem önce `/Pairing` ile eşleşir, ardından asıl işlemi gönderir. Sıra numarası hatasında
(72/73) cihazın döndüğü sıra numarasıyla tekrar dener.

## Derleme

Visual Studio 2019 veya sonrası ile `PavoTest.sln` açılıp derlenir. NuGet paketleri
(Newtonsoft.Json) derlemede otomatik iner.

Komut satırından:

```powershell
& "C:\Program Files\Microsoft Visual Studio\18\Community\MSBuild\Current\Bin\MSBuild.exe" PavoTest.sln -restore -p:Configuration=Release
```

Çıktı: `PavoTest\bin\Release\` (`PavoTest.exe`, `PavoTest.exe.config`, `Newtonsoft.Json.dll`).
Hedef makinede .NET Framework 4.8 gerekir (Windows 10 1903+ ve Windows 11'de hazır gelir).

## Çalışma zamanı dosyaları (exe'nin yanında)

| Dosya | İçerik |
|---|---|
| `PavoTest.ini` | `[db]` son kullanılan cihaz ve işlem adı, `[cihazlar]` cihaz listesi (en fazla 20) |
| `{AŞAMA}_komut_*.txt` | Cihaza giden JSON |
| `{AŞAMA}_gelen_*.txt` | Cihazdan gelen JSON |

Bu dosyalar makineye özeldir ve git'e eklenmez.

## Toplu tahsilat

Ekrandaki **Listeyi kullan (nakit / kredi kartı / HGS)** seçeneğini işaretleyin.
Altı boş satıra işlem adı ve tutar ve KDV girilebilir; en alttaki yeni satırdan daha fazla kayıt eklenebilir.
Tutarlar `12,50` veya `12.50` biçiminde girilir (binlik ayırıcı kullanılmaz).
Seçilen kayıtlar **Seçileni sil**, tüm kayıtlar **Listeyi temizle** ile kaldırılır.
Tamamen boş satırlar atlanır; eksik ad, geçersiz/sıfır/negatif veya ikiden fazla ondalıklı tutar gönderimi engeller.

Nakit, Kredi Kartı veya HGS seçip **İşlem Yap** düğmesine basın.
Bütün kayıtlar tek `/CompleteSale` isteğinde `Sale.AddedSaleItems` dizisine ayrı kalemler olarak aktarılır.
`GrossPrice`, `TotalPrice` ve tek ödemenin `Amount` değeri kalem toplamıdır.
HGS, mevcut Otopark entegrasyonundaki `Mediator: 14`, `Brand: 225` eşlemesiyle mali fiş akışıdır;
bu araç ayrıca bir HGS servisinden bakiye çekmez. Cihazda ilgili ödeme türü tanımlı olmalıdır.
Liste seçeneği kapalıyken üstteki işlem adı ve tutarla tek kalemli satış yapılır.

Cihazdan bağımsız doğrulama (önce Release derleyin):

```powershell
powershell.exe -NoProfile -ExecutionPolicy Bypass -File tests\Verify-Batch.ps1
```

Test; altı kalemli nakit/KK/HGS JSON isteklerini, toplamları, Türkçe/İngilizce kültürlerde
ondalık tutarları, özel karakterleri, müşteri bilgisini, tek kalemli satışı ve liste doğrulamasını denetler.

## Açılışta CSV yükleme

EXE'nin bulunduğu klasörde `data.csv` varsa açılışta toplu tahsilat listesine yüklenir
ve **Listeyi kullan** etkinleşir. Dosya yoksa boş liste korunur; CSV otomatik tahsilat başlatmaz.
`data.example.csv` dosyasını örnek olarak kullanabilirsiniz.

```csv
IslemAdi;Tutar;Kdv
Otopark;100,00;20
Yıkama;150,00;10
```

Dosya UTF-8 biçiminde ve başlık satırlı olmalıdır. `;` veya `,` ayırıcı desteklenir.
Sütun sırası serbesttir: `IslemAdi` / `İşlem Adı`, `Tutar` / `Tutar (TL)`, `Kdv` / `KDV (%)`.
KDV sütunu yoksa veya hücresi boşsa 20 kullanılır. Virgül ayırıcılı dosyalarda
virgüllü tutarı çift tırnak içine alın (`"12,50"`). Başka sütunlar göz ardı edilir.
Hatalı kayıt varsa hiçbir kayıt yüklenmez ve hata gösterilir; CSV dosyası değiştirilmez.
