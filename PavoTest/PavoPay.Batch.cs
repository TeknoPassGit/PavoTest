using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PavoTest
{
    partial class PavoPay
    {
        public const int HgsIslemTipi = 14;
        public sealed class SatisKalemi
        {
            public string Ad { get; private set; }
            public decimal Tutar { get; private set; }
            public int Kdv { get; private set; }
            public SatisKalemi(string ad, decimal tutar) : this(ad, tutar, 20) { }
            public SatisKalemi(string ad, decimal tutar, int kdv)
            {
                if (string.IsNullOrWhiteSpace(ad) || tutar <= 0 || decimal.Round(tutar, 2) != tutar)
                    throw new ArgumentException("Geçersiz satış kalemi.");
                if (kdv < 0 || kdv > 100) throw new ArgumentException("Geçersiz KDV değeri.");
                Kdv = kdv;
                Ad = ad;
                Tutar = tutar;
            }
        }
        public List<SatisKalemi> SatisKalemleri = new List<SatisKalemi>();

        internal string SatisIstegiOlustur(string seriNo, int odemeTipi, decimal tekTutar)
        {
            var kalemler = SatisKalemleri.Count > 0 ? SatisKalemleri.ToArray()
                : new[] { new SatisKalemi(prmislemadi, tekTutar) };
            decimal toplam = kalemler.Sum(x => x.Tutar);
            var odeme = new JObject { ["Mediator"] = odemeTipi == HgsIslemTipi ? 14 : odemeTipi + 1, ["Amount"] = toplam };
            if (odemeTipi == HgsIslemTipi) odeme["Brand"] = 225;
            var sale = new JObject
            {
                ["PaymentInformations"] = new JArray(odeme),
                ["RefererApp"] = "TeknoPass Pay", ["RefererAppVersion"] = "1.0.0",
                ["MainDocumentType"] = 1, ["GrossPrice"] = toplam, ["TotalPrice"] = toplam,
                ["SendPhoneNotification"] = false, ["SendEMailNotification"] = false,
                ["ShowCreditCardMenu"] = false, ["SelectedSlots"] = new JArray(),
                ["AllowDismissCardRead"] = false, ["CardReadTimeout"] = 30,
                ["SkipAmountCash"] = true, ["CancelPaymentLater"] = true, ["AskCustomer"] = false,
                ["SendResponseBeforePrint"] = true, ["TryAgainOnPaymentFailure"] = false,
                ["AddedSaleItems"] = new JArray(kalemler.Select(x => new JObject
                {
                    ["Name"] = x.Ad, ["IsGeneric"] = false, ["UnitCode"] = "C62", ["TaxGroupCode"] = "KDV" + x.Kdv.ToString(System.Globalization.CultureInfo.InvariantCulture),
                    ["ItemQuantity"] = 1, ["UnitPriceAmount"] = x.Tutar,
                    ["GrossPriceAmount"] = x.Tutar, ["TotalPriceAmount"] = x.Tutar,
                    ["ReservedText"] = "TEST0001", ["PriceEffect"] = new JObject()
                })),
                ["ReceiptInformation"] = new JObject { ["ReceiptImageEnabled"] = true, ["ReceiptWidth"] = "80mm" }
            };
            if (!string.IsNullOrEmpty(secilenCari.CARIADI))
                sale["CustomerParty"] = new JObject
                {
                    ["CustomerType"] = secilenCari.MUSTERITURU, ["FirstName"] = secilenCari.ADI,
                    ["MiddleName"] = "", ["FamilyName"] = secilenCari.SOYADI, ["CompanyName"] = secilenCari.CARIADI,
                    ["TaxOfficeCode"] = secilenCari.VERGIDAIRESI, ["TaxNumber"] = secilenCari.VERGINO,
                    ["Phone"] = secilenCari.TEL1, ["EMail"] = secilenCari.EMAIL, ["Country"] = secilenCari.ULKE,
                    ["City"] = secilenCari.IL, ["District"] = secilenCari.ILCE, ["Neighborhood"] = "",
                    ["Address"] = secilenCari.ADRES, ["IsEFaturaMerchant"] = secilenCari.EFATURA
                };
            return new JObject
            {
                ["TransactionHandle"] = new JObject
                {
                    ["SerialNumber"] = seriNo, ["Fingerprint"] = prmFingerPrint,
                    ["TransactionSequence"] = prmTranNo, ["TransactionDate"] = prmTranDate
                },
                ["Sale"] = sale
            }.ToString(Formatting.None);
        }
    }
}
