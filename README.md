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
