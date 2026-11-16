using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;

namespace PavoTest
{
    class PavoPay
    {
        public List<string> lstLog = new List<string>();
        public List<string> fisyazitems = new List<string>();
        public string prmCihazIp = "";
        public int prmOdemeTipi = 1;
        public double prmTutar = 0;
        public string prmAsama = "PAIR";
        public string prmFingerPrint = "teknopass";
        public string prmCihazSeriNo = "";
        public string prmGidenJson = "";
        public string prmislemadi = "Islem Ucreti";
        public int prmTranNo = 0;
        public string prmTranDate = DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + ".000001";
        public PavoSonucOdm SonSonuc;
        public int toplamhatasayac = 3;
        public bool prmUrlYanitKaydet = false;
        public bool IletisimHatasi = false;
        public string prmLogKlasor = "";
        public string prmvkn = "";
        public string iptalFaturano = "";
        public string prmpavoislemucreti = "Tuvalet Ucreti";
        public delegate void WorkCompletedCallBack(string result);
        public SecilenCari secilenCari = new SecilenCari();

        public void LogYaz(string gelen)
        {
            lstLog.Add("[" + DateTime.Now.ToString() + "] " + gelen);
        }
        
        public void CevapYaz(string gelen, WorkCompletedCallBack callback)
        {
            Console.WriteLine(prmAsama);
            Console.WriteLine(gelen);

            if(prmUrlYanitKaydet)
            {
                string s = prmLogKlasor + "\\" + prmAsama + "_" + "komut" + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
                File.WriteAllText(s, prmGidenJson);

                s = prmLogKlasor + "\\" + prmAsama + "_" + "gelen" + "_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".txt";
                File.WriteAllText(s, gelen);
            }

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };

            

            SonSonuc = JsonConvert.DeserializeObject<PavoSonucOdm>(gelen, settings);
            prmTranNo = SonSonuc.TransactionHandle.TransactionSequence;
            prmTranDate = (SonSonuc.TransactionHandle.TransactionDate == null) ? DateTime.Now.ToString("yyyy-MM-dd") + "T" + DateTime.Now.ToString("HH:mm:ss") + ".000001" : SonSonuc.TransactionHandle.TransactionDate;

            callback(gelen);

            if(SonSonuc.HasError)
            {
                if (toplamhatasayac < 0) return;

                if(SonSonuc.ErrorCode == 72 || SonSonuc.ErrorCode == 73)
                {
                    OdemeAl(prmCihazIp, prmOdemeTipi, prmTutar, prmCihazSeriNo, callback);
                }

                toplamhatasayac--;
            }
            else
            {
                if(prmAsama == "PAIR" && IletisimHatasi == false)
                {
                    if (prmOdemeTipi == 0) prmAsama = "ODEME";
                    if (prmOdemeTipi == 1) prmAsama = "ODEME";
                    if (prmOdemeTipi == 2) prmAsama = "GUNSONU";
                    if (prmOdemeTipi == 3) prmAsama = "IPTAL";
                    if (prmOdemeTipi == 4) prmAsama = "PARAMETRE";
                    if (prmOdemeTipi == 5) prmAsama = "REBOOT";
                    if (prmOdemeTipi == 6) prmAsama = "TURMOB";
                    if (prmOdemeTipi == 7) prmAsama = "CANCEL";
                    if (prmOdemeTipi == 8) prmAsama = "ADVSALE";
                    if (prmOdemeTipi == 9) prmAsama = "GUNSONUTOPLU";
                    if (prmOdemeTipi == 10) prmAsama = "ASKIDASATIS";
                    if (prmOdemeTipi == 11) prmAsama = "EXITKIOSK";
                    if (prmOdemeTipi == 12) prmAsama = "ENTERKIOSK";
                    if (prmOdemeTipi == 13) prmAsama = "FISYAZDIR";

                    prmTranNo += 1;
                    OdemeAl(prmCihazIp, prmOdemeTipi, prmTutar, prmCihazSeriNo, callback);
                }
            }
        }

        public void OdemeAl(string ip, int odemetipi, double tutar, string CihazSeriNo, WorkCompletedCallBack callback)
        {
            if (tutar == 0) return;

            prmCihazIp = ip;
            prmOdemeTipi = odemetipi;
            prmCihazSeriNo = CihazSeriNo;
            prmTutar = tutar;
            

            string birim = "KGM";
            birim = "C62";

            string ct = "\"";
            string adres = "http://" + ip + "/Pairing";            
            string parametre = "{" + ct + "TransactionHandle" + ct + ":{" + ct + "SerialNumber" + ct + " : " + ct + CihazSeriNo + ct + ", " + ct + "TransactionDate" + ct + ": " + ct + prmTranDate + ct + ", " + ct + "TransactionSequence" + ct + ": " + prmTranNo.ToString() + ", " + ct + "Fingerprint" + ct + ":" + ct + prmFingerPrint + ct + " }}";

            if(prmAsama == "PAIR")
            {
                adres = "http://" + ip + "/Pairing";
                parametre = "{" + ct + "TransactionHandle" + ct + ":{" + ct + "SerialNumber" + ct + " : " + ct + CihazSeriNo + ct + ", " + ct + "TransactionDate" + ct + ": " + ct + prmTranDate + ct + ", " + ct + "TransactionSequence" + ct + ": " + prmTranNo.ToString() + ", " + ct + "Fingerprint" + ct + ":" + ct + prmFingerPrint + ct + " }}";
            }

            /*
            if (prmAsama == "ODEME")
            {               
                adres = "http://" + ip + "/CompleteSale";                
                parametre = "{" + ct + "TransactionHandle" + ct + ": {" + ct + "SerialNumber" + ct + ": " + ct + CihazSeriNo + ct + "," + ct + "Fingerprint" + ct + ": " + ct + prmFingerPrint + ct + "," + ct + "TransactionSequence" + ct + ": " + prmTranNo.ToString() + "," + ct + "TransactionDate" + ct + ": " + ct + prmTranDate + ct + "}," + ct + "Sale" + ct + ": {" + ct + "PaymentInformations" + ct + ": [{" + ct + "Mediator" + ct + ": " + (odemetipi + 1).ToString() + "," + ct + "Amount" + ct + ": " + tutar.ToString() + "}],    " + ct + "RefererApp" + ct + ": " + ct + "TeknoPass Pay" + ct + "," + ct + "RefererAppVersion" + ct + ": " + ct + "1.0.0" + ct + "," + ct + "MainDocumentType" + ct + ": 1," + ct + "GrossPrice" + ct + ": " + tutar.ToString() + "," + ct + "TotalPrice" + ct + ": " + tutar.ToString() + "," + ct + "SendPhoneNotification" + ct + ": false," + ct + "SendEMailNotification" + ct + ": false," + ct + "ShowCreditCardMenu" + ct + ": false," + ct + "SelectedSlots" + ct + ": []," + ct + "AllowDismissCardRead" + ct + ": false," + ct + "CardReadTimeout" + ct + ": 30," + ct + "SkipAmountCash" + ct + ": true," + ct + "CancelPaymentLater" + ct + ": true," + ct + "AskCustomer" + ct + ": false," + ct + "SendResponseBeforePrint" + ct + ": true," + ct + "TryAgainOnPaymentFailure" + ct + ": false," + ct + "AddedSaleItems" + ct + ": [{" + ct + "Name" + ct + ": " + ct + TrSil(prmpavoislemucreti) + ct + "," + ct + "IsGeneric" + ct + ": false," + ct + "UnitCode" + ct + ": " + ct + birim + ct + "," + ct + "TaxGroupCode" + ct + ": " + ct + "KDV20" + ct + "," + ct + "ItemQuantity" + ct + ": 1," + ct + "UnitPriceAmount" + ct + ": " + tutar.ToString() + "," + ct + "GrossPriceAmount" + ct + ": " + tutar.ToString() + "," + ct + "TotalPriceAmount" + ct + ": " + tutar.ToString() + "," + ct + "ReservedText" + ct + ": " + ct + "TEST0001" + ct + "," + ct + "PriceEffect" + ct + ": {}}]," + ct + "ReceiptInformation" + ct + ":{" + ct + "ReceiptImageEnabled" + ct + ":true}}}";
            }
            */

            if (prmAsama == "ODEME")
            {
                string customerpart = "";

                if (secilenCari.CARIADI != "")
                {
                    customerpart = "," + ct + "CustomerParty" + ct + ": {" + ct + "CustomerType" + ct + ": "+ secilenCari.MUSTERITURU.ToString() + "," + ct + "FirstName" + ct + ": " + ct + secilenCari.ADI + ct + "," + ct + "MiddleName" + ct + ": " + ct + "" + ct + "," + ct + "FamilyName" + ct + ": " + ct + secilenCari.SOYADI + ct + "," + ct + "CompanyName" + ct + ": " + ct + secilenCari.CARIADI + ct + "," + ct + "TaxOfficeCode" + ct + ":" + ct + secilenCari.VERGIDAIRESI + ct + "," + ct + "TaxNumber" + ct + ":" + ct + secilenCari.VERGINO + ct + "," + ct + "Phone" + ct + ": " + ct + secilenCari.TEL1 + ct + "," + ct + "EMail" + ct + ":" + ct + secilenCari.EMAIL + ct + "," + ct + "Country" + ct + ":" + ct + secilenCari.ULKE + ct + "," + ct + "City" + ct + ":" + ct + secilenCari.IL + ct + "," + ct + "District" + ct + ":" + ct + secilenCari.ILCE + ct + "," + ct + "Neighborhood" + ct + ":" + ct + "" + ct + "," + ct + "Address" + ct + ":" + ct + secilenCari.ADRES + ct + "," + ct + "IsEFaturaMerchant" + ct + ":" + secilenCari.EFATURA.ToString() + "}";
                    customerpart = customerpart.Replace("True", "true").Replace("False", "false");
                }

                adres = "http://" + ip + "/CompleteSale";
                parametre = "{" + ct + "TransactionHandle" + ct + ": {" + ct + "SerialNumber" + ct + ": " + ct + CihazSeriNo + ct + "," + ct + "Fingerprint" + ct + ": " + ct + prmFingerPrint + ct + "," + ct + "TransactionSequence" + ct + ": " + prmTranNo.ToString() + "," + ct + "TransactionDate" + ct + ": " + ct + prmTranDate + ct + "}," + ct + "Sale" + ct + ": {" + ct + "PaymentInformations" + ct + ": [{" + ct + "Mediator" + ct + ": " + (odemetipi + 1).ToString() + "," + ct + "Amount" + ct + ": " + tutar.ToString() + "}],    " + ct + "RefererApp" + ct + ": " + ct + "TeknoPass Pay" + ct + "," + ct + "RefererAppVersion" + ct + ": " + ct + "1.0.0" + ct + "," + ct + "MainDocumentType" + ct + ": 1," + ct + "GrossPrice" + ct + ": " + tutar.ToString() + "," + ct + "TotalPrice" + ct + ": " + tutar.ToString() + "," + ct + "SendPhoneNotification" + ct + ": false," + ct + "SendEMailNotification" + ct + ": false," + ct + "ShowCreditCardMenu" + ct + ": false," + ct + "SelectedSlots" + ct + ": []," + ct + "AllowDismissCardRead" + ct + ": false," + ct + "CardReadTimeout" + ct + ": 30," + ct + "SkipAmountCash" + ct + ": true," + ct + "CancelPaymentLater" + ct + ": true," + ct + "AskCustomer" + ct + ": false," + ct + "SendResponseBeforePrint" + ct + ": true," + ct + "TryAgainOnPaymentFailure" + ct + ": false," + ct + "AddedSaleItems" + ct + ": [{" + ct + "Name" + ct + ": " + ct + prmislemadi + ct + "," + ct + "IsGeneric" + ct + ": false," + ct + "UnitCode" + ct + ": " + ct + birim + ct + "," + ct + "TaxGroupCode" + ct + ": " + ct + "KDV20" + ct + "," + ct + "ItemQuantity" + ct + ": 1," + ct + "UnitPriceAmount" + ct + ": " + tutar.ToString() + "," + ct + "GrossPriceAmount" + ct + ": " + tutar.ToString() + "," + ct + "TotalPriceAmount" + ct + ": " + tutar.ToString() + "," + ct + "ReservedText" + ct + ": " + ct + "TEST0001" + ct + "," + ct + "PriceEffect" + ct + ": {}}]," + ct + "ReceiptInformation" + ct + ":{" + ct + "ReceiptImageEnabled" + ct + ":true, "+ ct + "ReceiptWidth" + ct + ":" + ct + "80mm" + ct + "} " + customerpart + " }}";
            }


            if (prmAsama == "GUNSONU")
            {
                adres = "http://" + ip + "/PerformEOD";
                parametre = "{" + ct + "TransactionHandle" + ct + ": {" + ct + "SerialNumber" + ct + ": " + ct + CihazSeriNo + ct + "," + ct + "Fingerprint" + ct + ": " + ct + prmFingerPrint + ct + "," + ct + "TransactionSequence" + ct + ": " + prmTranNo.ToString() + "," + ct + "TransactionDate" + ct + ": " + ct + prmTranDate + ct + "}," + ct + "PerformEOD" + ct + ": {" + ct + "AdditionalInfo" + ct + ": {" + ct + "print" + ct + ": " + "true," + ct + "receiptImage" + ct + ": " + "false" + "}}}";
            }

            if (prmAsama == "PARAMETRE")
            {
                adres = "http://" + ip + "/UpdateTerminalData";
                parametre = "{" + ct + "TransactionHandle" + ct + ": {" + ct + "SerialNumber" + ct + ": " + ct + CihazSeriNo + ct + "," + ct + "Fingerprint" + ct + ": " + ct + prmFingerPrint + ct + "," + ct + "TransactionSequence" + ct + ": " + prmTranNo.ToString() + "," + ct + "TransactionDate" + ct + ": " + ct + prmTranDate + ct + "}}";
            }

            if (prmAsama == "IPTAL")
            {
                adres = "http://" + ip + "/AbortCurrentPayment";
                parametre = "{" + ct + "TransactionHandle" + ct + ": {" + ct + "SerialNumber" + ct + ": " + ct + CihazSeriNo + ct + "," + ct + "Fingerprint" + ct + ": " + ct + prmFingerPrint + ct + "," + ct + "TransactionSequence" + ct + ": " + prmTranNo.ToString() + "," + ct + "TransactionDate" + ct + ": " + ct + prmTranDate + ct + "}}";
            }

            if (prmAsama == "REBOOT")
            {
                adres = "http://" + ip + "/RebootDevice";
                parametre = "{" + ct + "TransactionHandle" + ct + ": {" + ct + "SerialNumber" + ct + ": " + ct + CihazSeriNo + ct + "," + ct + "Fingerprint" + ct + ": " + ct + prmFingerPrint + ct + "," + ct + "TransactionSequence" + ct + ": " + prmTranNo.ToString() + "," + ct + "TransactionDate" + ct + ": " + ct + prmTranDate + ct + "}}";
            }

            if (prmAsama == "ENTERKIOSK")
            {
                adres = "http://" + ip + "/EnterKioskMode";
                parametre = "{" + ct + "TransactionHandle" + ct + ": {" + ct + "SerialNumber" + ct + ": " + ct + CihazSeriNo + ct + "," + ct + "Fingerprint" + ct + ": " + ct + prmFingerPrint + ct + "," + ct + "TransactionSequence" + ct + ": " + prmTranNo.ToString() + "," + ct + "TransactionDate" + ct + ": " + ct + prmTranDate + ct + "}}";
            }

            if (prmAsama == "EXITKIOSK")
            {
                adres = "http://" + ip + "/ExitKioskMode";
                parametre = "{" + ct + "TransactionHandle" + ct + ": {" + ct + "SerialNumber" + ct + ": " + ct + CihazSeriNo + ct + "," + ct + "Fingerprint" + ct + ": " + ct + prmFingerPrint + ct + "," + ct + "TransactionSequence" + ct + ": " + prmTranNo.ToString() + "," + ct + "TransactionDate" + ct + ": " + ct + prmTranDate + ct + "}}";
            }

            if (prmAsama == "TURMOB")
            {
                string talan = "Vkn";
                if (prmvkn.Length == 11) talan = "Tckn";

                adres = "http://" + ip + "/GetTurmobInfo";
                parametre = "{" + ct + "TransactionHandle" + ct + ": {" + ct + "SerialNumber" + ct + ": " + ct + CihazSeriNo + ct + "," + ct + "Fingerprint" + ct + ": " + ct + prmFingerPrint + ct + "," + ct + "TransactionSequence" + ct + ": " + prmTranNo.ToString() + "," + ct + "TransactionDate" + ct + ": " + ct + prmTranDate + ct + "}," + ct + "TurmobInput" + ct + ": {" + ct + talan + ct + ": " + ct + prmvkn + ct + "}}";
            }

            if (prmAsama == "CANCEL")
            {
                adres = "http://" + ip + "/CancelSale";
                parametre = "{" + ct + "TransactionHandle" + ct + ": {" + ct + "SerialNumber" + ct + ": " + ct + CihazSeriNo + ct + "," + ct + "Fingerprint" + ct + ": " + ct + prmFingerPrint + ct + "," + ct + "TransactionSequence" + ct + ": " + prmTranNo.ToString() + "," + ct + "TransactionDate" + ct + ": " + ct + prmTranDate + ct + "}," + ct + "Sale" + ct + ": {" + ct + "InvoiceNo" + ct + ": " + ct + iptalFaturano + ct + "}}";
            }

            if (prmAsama == "ADVSALE")
            {
                adres = "http://" + ip + "/AdvanceSale";
                parametre = "{" + ct + "TransactionHandle" + ct + ": {" + ct + "SerialNumber" + ct + ": " + ct + CihazSeriNo + ct + "," + ct + "Fingerprint" + ct + ": " + ct + prmFingerPrint + ct + "," + ct + "TransactionSequence" + ct + ": " + prmTranNo.ToString() + "," + ct + "TransactionDate" + ct + ": " + ct + prmTranDate + ct + "}," + ct + "Sale" + ct + ": {" + ct + "RefererApp" + ct + ": " + ct + "TeknoPass Pay" + ct + "," + ct + "RefererAppVersion" + ct + ": " + ct + "1.0.0" + ct + "," + ct + "TotalAmount" + ct + ": " + tutar.ToString() + "," + ct + "SaleReason" + ct + ":" + ct + "Avans Aciklama" + ct  + "," + ct + "SendPhoneNotification" + ct + ": false," + ct + "SendEMailNotification" + ct + ": false," + ct + "ShowCreditCardMenu" + ct + ": false," + ct + "AskCustomer" + ct + ": false," + ct + "SendResponseBeforePrint" + ct + ": true }}";
            }

            if (prmAsama == "GUNSONUTOPLU")
            {
                adres = "http://" + ip + "/QueryReport";
                parametre = "{" + ct + "TransactionHandle" + ct + ": {" + ct + "SerialNumber" + ct + ": " + ct + CihazSeriNo + ct + "," + ct + "Fingerprint" + ct + ": " + ct + prmFingerPrint + ct + "," + ct + "TransactionSequence" + ct + ": " + prmTranNo.ToString() + "," + ct + "TransactionDate" + ct + ": " + ct + prmTranDate + ct + "}," + ct + "ReportData" + ct + ": {" + ct + "ReportType" + ct + ":" + ct + "XReport" + ct + "," + ct + "PrepareImage" + ct + ": false," + ct + "ReceiptWidth" + ct + ":" + ct + "58mm" + ct + "}}";
            }

            if (prmAsama == "ASKIDASATIS")
            {
                adres = "http://" + ip + "/ListPendingSaleWithDetail";
                parametre = "{" + ct + "TransactionHandle" + ct + ": {" + ct + "SerialNumber" + ct + ": " + ct + CihazSeriNo + ct + "," + ct + "Fingerprint" + ct + ": " + ct + prmFingerPrint + ct + "," + ct + "TransactionSequence" + ct + ": " + prmTranNo.ToString() + "," + ct + "TransactionDate" + ct + ": " + ct + prmTranDate + ct + "}}";
            }

            if (prmAsama == "FISYAZDIR")
            {
                adres = "http://" + ip + "/PrintOut";
                string printOutStr = "";

                for (int i = 0; i < fisyazitems.Count; i++)
                {
                    string satir = TrSil(fisyazitems[i]);
                    if (i > 0) printOutStr += ",";
                    printOutStr += satir;
                    //printOutStr += "{" + ct + "type" + ct + ": " + ct + satir.Split('|')[0] + ct + "," + ct + "allignment" + ct + ": " + ct + satir.Split('|')[1] + ct + "," + ct + "fontSize" + ct + ": " + ct + satir.Split('|')[2] + ct + "," + ct + "text" + ct + ": " + ct + satir.Split('|')[3] + ct + "," + ct + "fontWeight" + ct + ": " + ct + satir.Split('|')[4] + ct + "}";
                }                


                parametre = "{" + ct + "TransactionHandle" + ct + ": {" + ct + "SerialNumber" + ct + ": " + ct + CihazSeriNo + ct + "," + ct + "Fingerprint" + ct + ": " + ct + prmFingerPrint + ct + "," + ct + "TransactionSequence" + ct + ": " + prmTranNo.ToString() + "," + ct + "TransactionDate" + ct + ": " + ct + prmTranDate + ct + "}," + ct  + "Print" + ct + ":{" + ct + "PrintableItems" + ct + ":[" + printOutStr + "]}}" ;
            }

            

            prmGidenJson = parametre;

            callback("GIDEN;"+prmGidenJson);

            PavoUrl(adres, parametre, callback);
        }

        public async void PavoUrl(string adres, string parametre, WorkCompletedCallBack callback)
        {
            IletisimHatasi = false;
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, adres);
            var content = new StringContent(parametre, null, "application/json");
            request.Content = content;

            try
            {
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                CevapYaz(await response.Content.ReadAsStringAsync(), callback);
            }
            catch
            {
                IletisimHatasi = true;
                string ct = "\"";
                string gon = "{" + ct + "HasError" + ct + ":true," + ct + "ErrorCode" + ct + ":9001," + ct + "Message" + ct + ":" + ct + "Adres Erişim Hatası" + ct + "," + ct + "TransactionHandle" + ct + ":{ " + ct + "SerialNumber" + ct + ":" + ct + prmCihazSeriNo + ct + "," + ct + "Fingerprint" + ct + ":" + ct + prmFingerPrint + ct + "," + ct + "TransactionSequence" + ct + ":1," + ct + "TransactionDate" + ct + ":" + ct + "2020 - 01 - 01T01:01:01.262988" + ct + "}," + ct + "Errors" + ct + ":[" + ct + "Adres erişim hatası" + ct + "]}";           
                CevapYaz(gon, callback);
            }            
        }

        public string TrSil(string gelen)
        {
            string sonuc = gelen;

            sonuc = sonuc.Replace("Ğ", "G");
            sonuc = sonuc.Replace("Ü", "U");
            sonuc = sonuc.Replace("Ş", "S");
            sonuc = sonuc.Replace("İ", "I");
            sonuc = sonuc.Replace("Ö", "O");
            sonuc = sonuc.Replace("Ç", "C");

            sonuc = sonuc.Replace("ğ", "g");
            sonuc = sonuc.Replace("ü", "u");
            sonuc = sonuc.Replace("ş", "s");
            sonuc = sonuc.Replace("ı", "i");
            sonuc = sonuc.Replace("ö", "o");
            sonuc = sonuc.Replace("ç", "c");

            return sonuc;
        }

        public double StrToFloatDef(string gelen, double def)
        {
            double sonuc = def;

            try
            {
                sonuc = Convert.ToDouble(gelen);
            }
            catch
            {
                sonuc = def;
            }

            return sonuc;
        }

        public int StrToIntDef(string gelen, int def)
        {
            int sonuc = def;

            try
            {
                sonuc = Convert.ToInt32(gelen);
            }
            catch
            {
                sonuc = def;
            }

            return def;
        }

        public class PavoSonuc
        {            
            public bool HasError { get; set; }

            public int ErrorCode { get; set; }

            public string Message { get; set; }

            public PavoSonucAlt TransactionHandle { get; set; }

        }

        public class PavoSonucOdm
        {
            public bool HasError { get; set; }

            public int ErrorCode { get; set; }

            public string Message { get; set; }

            public PavoSonucAlt TransactionHandle { get; set; }
            public PavoSonucData Data { get; set; }
        }

        public class PavoSonucAlt
        {
            public string SerialNumber { get; set; }

            public string FingerPrint { get; set; }

            public int TransactionSequence { get; set; }

            public string TransactionDate { get; set; }            
        }

        public class PavoSonucData
        {
            public int Id { get; set; }
            public string SaleNumber { get; set; }
            public bool IsOffline { get; set; }
            public string OrderNo { get; set; }
            public int TypeId { get; set; }
            public int StatusId { get; set; }
            public List<PavoSonucAP> AddedPayments { get; set; }
            public List<PavoSonucFinDoc> FinancialDocuments { get; set; }
            public List<string> Errors { get; set; }
            public string CustomerReceiptImage { get; set; }
        }
        public class PavoSonucAP
        {
            public int Id { get; set; }
            public int SaleId { get; set; }
            public int StatusId { get; set; }
            public int MerchantId { get; set; }            
            public bool IsExternal { get; set; }
            public PavoSonucPayment OnlinePayment { get; set; }
            public PavoSonucPayment CashPayment { get; set; }               
        }

        public class PavoSonucPayment
        {
            public int Id { get; set; }
            public int PaymentId { get; set; }
            public int SaleId { get; set; }
            public string ResponseCode { get; set; }
            public string ResponseDescription { get; set; }
            public double GivenAmount { get; set; }
        }
        public class PavoSonucFinDoc
        {
            public int Id { get; set; }
            public string InvoiceNo { get; set; }
            public string DocumentNo { get; set; }
            public string SaleUid { get; set; }
            public bool CancelRequested { get; set; }            
        }

        public class SecilenCari
        {
            public int KNO = 0;
            public int MUSTERITURU = 0;
            public string CARIADI = "";
            public string ADRES = "";
            public string TEL1 = "";
            public string TEL2 = "";
            public string ULKE = "";
            public string IL = "";
            public string ILCE = "";
            public string BOLGE = "";
            public string EMAIL = "";
            public string VERGIDAIRESI = "";
            public string VERGINO = "";
            public bool EFATURA = false;
            public string ADI = "";
            public string SOYADI = "";
        }
    }
}
