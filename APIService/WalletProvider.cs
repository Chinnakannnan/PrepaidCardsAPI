using BusinessModel.Card;
using BusinessModel.Common;
using BusinessModel.Prepaid;
using BusinessModel.Wallet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace APIService
{
    public class WalletProvider
    {
        public const string lstrFolderName = "Wallet";
        Log objLog = new Log();
        M2BEncrypt enc = new M2BEncrypt();
        public WalletProvider()
        {

        }

        public LoadWalletResponse LoadCustomerWalletByEncry(LoadWalletRequest loadWalletRequest)
        {
            LoadWalletResponse response = new LoadWalletResponse();
            YappayEncryptedResponse responseEncry = new YappayEncryptedResponse();
            try
            {
                string jsonReq = JsonConvert.SerializeObject(loadWalletRequest);
                objLog.WriteAppLog("Customer Wallet - Request from client app  :" + jsonReq, lstrFolderName);

                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(PrepaidConstants.PrepaidBaseAddressByEncry);
                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Add(CommonConstants.Tenant, CommonConstants.M2PTenant);
                http.DefaultRequestHeaders.Add(PrepaidConstants.Authorization, PrepaidConstants.PartnerTokenValue);
                http.DefaultRequestHeaders.TryAddWithoutValidation(PrepaidConstants.ContentType, PrepaidConstants.ApplicationJson);
                // encryption

                //objLog.WriteAppLog("Send OTP to Register Customer - Request to Transcorp plain data  :" + requestData, lstrFolderName);
                string uniqRanNumber = Utility.GetTransID().Substring(18 - 16);
                string requestEncryptedData = enc.encodeRequest(jsonReq, uniqRanNumber, CommonConstants.CustomerTenantValue);
                objLog.WriteAppLog("Customer Wallet - Request to Transcorp Request Encrypted Data  :" + requestEncryptedData, lstrFolderName);
                var stringContent = new StringContent(requestEncryptedData, Encoding.UTF8, PrepaidConstants.ApplicationJson);

                //var stringContent = new StringContent(JsonConvert.SerializeObject(loadWalletRequest), Encoding.UTF8, CommonConstants.ApplicationJson);
                HttpResponseMessage responseMessage = http.PostAsync(CommonConstants.LoadWalletRequestURL, stringContent).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultValue = responseMessage.Content.ReadAsStringAsync().Result;
                    responseEncry = JsonConvert.DeserializeObject<YappayEncryptedResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                    string requestDecryptrdData = enc.decodeResponse(responseEncry);
                    objLog.WriteAppLog("Customer Wallet - Response From Transcorp  :" + requestDecryptrdData, lstrFolderName);
                    response = JsonConvert.DeserializeObject<LoadWalletResponse>(requestDecryptrdData);
                }
                else
                {
                    string respMesg = responseMessage.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<LoadWalletResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                    objLog.WriteAppLog("Send OTP to Register Customer - Response From Transcorp  :" + respMesg, lstrFolderName);
                }

                return response;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
        public LoadWalletResponse LoadCustomerWallet(LoadWalletRequest loadWalletRequest)
        {
            LoadWalletResponse response = new LoadWalletResponse();

            try
            {
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(CommonConstants.YappayBaseAddress);
                http.DefaultRequestHeaders.Accept.Clear();

                http.DefaultRequestHeaders.Add(CommonConstants.Tenant, CommonConstants.TenantValue);
                http.DefaultRequestHeaders.TryAddWithoutValidation(CommonConstants.ContentType, CommonConstants.ContentTypeValue);

                var stringContent = new StringContent(JsonConvert.SerializeObject(loadWalletRequest), Encoding.UTF8, CommonConstants.ApplicationJson);
                HttpResponseMessage responseMessage = http.PostAsync(CommonConstants.LoadWalletRequestURL, stringContent).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultValue = responseMessage.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<LoadWalletResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                }

                return response;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        public LoadWalletResponse RefundCustomerWallet(RefundCustomerWalletRequest refundCustomerWalletRequest)
        {
            LoadWalletResponse response = new LoadWalletResponse();
            YappayEncryptedResponse responseEncry = new YappayEncryptedResponse();
            try
            {
                string jsonReq = JsonConvert.SerializeObject(refundCustomerWalletRequest);
                objLog.WriteAppLog("Refund Customer  Wallet - Request from client app  :" + jsonReq, lstrFolderName);

                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(PrepaidConstants.PrepaidBaseAddressByEncry);
                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Add(CommonConstants.Tenant, CommonConstants.RefundCustomerTenantValue);
                http.DefaultRequestHeaders.Add(PrepaidConstants.Authorization, PrepaidConstants.PartnerTokenValue);
                http.DefaultRequestHeaders.TryAddWithoutValidation(PrepaidConstants.ContentType, PrepaidConstants.ApplicationJson);
                // encryption

                //objLog.WriteAppLog("Send OTP to Register Customer - Request to Transcorp plain data  :" + requestData, lstrFolderName);
                string uniqRanNumber = Utility.GetTransID().Substring(18 - 16);
                string requestEncryptedData = enc.encodeRequest(jsonReq, uniqRanNumber, CommonConstants.CustomerTenantValue);
                objLog.WriteAppLog("RefundC ustomer  Wallet - Request to Transcorp Request Encrypted Data  :" + requestEncryptedData, lstrFolderName);
                var stringContent = new StringContent(requestEncryptedData, Encoding.UTF8, PrepaidConstants.ApplicationJson);

                HttpResponseMessage responseMessage = http.PostAsync(CommonConstants.RefundCustomerWalletUrl, stringContent).Result;
                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultValue = responseMessage.Content.ReadAsStringAsync().Result;
                    responseEncry = JsonConvert.DeserializeObject<YappayEncryptedResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                    string requestDecryptrdData = enc.decodeResponse(responseEncry);
                    objLog.WriteAppLog("Refund Customer  Wallet - Response From Transcorp  :" + requestDecryptrdData, lstrFolderName);
                    response = JsonConvert.DeserializeObject<LoadWalletResponse>(requestDecryptrdData);
                }
                else
                {
                    string respMesg = responseMessage.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<LoadWalletResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                    objLog.WriteAppLog("Refund Customer  Wallet - Response From Transcorp  :" + respMesg, lstrFolderName);
                }

                return response;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        public FetchBalanceResponse FetchCustomerBalance(string entityId)
        {
            FetchBalanceResponse response = new FetchBalanceResponse();

            try
            {
                objLog.WriteAppLog("Customer Wallet - Fetch Balance Request -  :" + entityId, lstrFolderName);

                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(CommonConstants.YappayBaseAddress);
                http.DefaultRequestHeaders.Accept.Clear();

                http.DefaultRequestHeaders.Add(CommonConstants.Tenant, CommonConstants.RefundCustomerTenantValue);
                http.DefaultRequestHeaders.Add(PrepaidConstants.Authorization, PrepaidConstants.PartnerTokenValue);
                http.DefaultRequestHeaders.TryAddWithoutValidation(CommonConstants.ContentType, CommonConstants.ContentTypeValue);

                var stringContent = new StringContent(entityId, Encoding.UTF8, CommonConstants.ApplicationJson);
                HttpResponseMessage responseMessage = http.GetAsync(CommonConstants.FetchCustomerBalanceUrl + "/" + entityId).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultValue = responseMessage.Content.ReadAsStringAsync().Result;
                    objLog.WriteAppLog("Customer Wallet - Fetch Balance response -  :" + resultValue, lstrFolderName);

                    response = JsonConvert.DeserializeObject<FetchBalanceResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                }
                else
                {
                    var resultValue = responseMessage.Content.ReadAsStringAsync().Result;
                    objLog.WriteAppLog("Customer Wallet - Fetch Balance response -  :" + resultValue, lstrFolderName);
                    response = JsonConvert.DeserializeObject<FetchBalanceResponse>(responseMessage.Content.ReadAsStringAsync().Result);

                }

                return response;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        public TransactionStatusResponse FetchTransactionStatus(string extTrxId)
        {
            TransactionStatusResponse response = new TransactionStatusResponse();

            try
            {
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(CommonConstants.YappayBaseAddress);
                http.DefaultRequestHeaders.Accept.Clear();

                http.DefaultRequestHeaders.Add(CommonConstants.Tenant, CommonConstants.RefundCustomerTenantValue);
                http.DefaultRequestHeaders.TryAddWithoutValidation(CommonConstants.ContentType, CommonConstants.ContentTypeValue);

                var stringContent = new StringContent(extTrxId, Encoding.UTF8, CommonConstants.ApplicationJson);
                HttpResponseMessage responseMessage = http.GetAsync(CommonConstants.FetchTransactionStatusUrl + "/" + extTrxId).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultValue = responseMessage.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<TransactionStatusResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                }

                return response;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }
        public BlockCardResponse BlockCard(BlockCardRequest blockCardRequest)
        {
            BlockCardResponse response = new BlockCardResponse();
            YappayEncryptedResponse responseEncry = new YappayEncryptedResponse();
            try
            {
                HttpClient http = new HttpClient();;
                http.DefaultRequestHeaders.Accept.Clear();
                string jsonReq = JsonConvert.SerializeObject(blockCardRequest);
                objLog.WriteAppLog("Customer Wallet - Request from client app  :" + jsonReq, lstrFolderName);

                http.BaseAddress = new Uri(PrepaidConstants.PrepaidBaseAddressByEncry);

                http.DefaultRequestHeaders.Add(CommonConstants.Tenant, CommonConstants.RefundCustomerTenantValue);
                http.DefaultRequestHeaders.Add(PrepaidConstants.Authorization, PrepaidConstants.PartnerTokenValue);
                http.DefaultRequestHeaders.TryAddWithoutValidation(PrepaidConstants.ContentType, PrepaidConstants.ApplicationJson);
                // encryption
                string uniqRanNumber = Utility.GetTransID().Substring(18 - 16);
                string requestEncryptedData = enc.encodeRequest(jsonReq, uniqRanNumber, CommonConstants.CustomerTenantValue);
                objLog.WriteAppLog("Customer Wallet - Request to Transcorp Request Encrypted Data  :" + requestEncryptedData, lstrFolderName);
                var stringContent = new StringContent(requestEncryptedData, Encoding.UTF8, PrepaidConstants.ApplicationJson);
             
                HttpResponseMessage responseMessage = http.PostAsync(CommonConstants.BlockCardUrl, stringContent).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultValue = responseMessage.Content.ReadAsStringAsync().Result;
                    responseEncry = JsonConvert.DeserializeObject<YappayEncryptedResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                    string requestDecryptrdData = enc.decodeResponse(responseEncry);
                    objLog.WriteAppLog("Customer Wallet - Response From Transcorp  :" + requestDecryptrdData, lstrFolderName);
                    response = JsonConvert.DeserializeObject<BlockCardResponse>(requestDecryptrdData);
                }
                else
                {
                    string respMesg = responseMessage.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<BlockCardResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                    objLog.WriteAppLog("Send OTP to Register Customer - Response From Transcorp  :" + respMesg, lstrFolderName);
                }
                return response;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public CardReplacementResponse CardReplacement(CardReplacementRequest cardReplacementRequest)
        {
            CardReplacementResponse response = new CardReplacementResponse();
            YappayEncryptedResponse responseEncry = new YappayEncryptedResponse();
            try
            {
                HttpClient http = new HttpClient(); ;
                http.DefaultRequestHeaders.Accept.Clear();
                string jsonReq = JsonConvert.SerializeObject(cardReplacementRequest);
                objLog.WriteAppLog("Card Replacement Request - Request from client app  :" + jsonReq, lstrFolderName);

                http.BaseAddress = new Uri(PrepaidConstants.PrepaidBaseAddressByEncry);

                http.DefaultRequestHeaders.Add(CommonConstants.Tenant, CommonConstants.RefundCustomerTenantValue);
                http.DefaultRequestHeaders.Add(PrepaidConstants.Authorization, PrepaidConstants.PartnerTokenValue);
                http.DefaultRequestHeaders.TryAddWithoutValidation(PrepaidConstants.ContentType, PrepaidConstants.ApplicationJson);
                // encryption
                string uniqRanNumber = Utility.GetTransID().Substring(18 - 16);
                string requestEncryptedData = enc.encodeRequest(jsonReq, uniqRanNumber, CommonConstants.CustomerTenantValue);
                objLog.WriteAppLog("Customer Wallet - Request to Transcorp Request Encrypted Data  :" + requestEncryptedData, lstrFolderName);
                var stringContent = new StringContent(requestEncryptedData, Encoding.UTF8, PrepaidConstants.ApplicationJson);

                HttpResponseMessage responseMessage = http.PostAsync(CommonConstants.CardReplacementUrl, stringContent).Result;


                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultValue = responseMessage.Content.ReadAsStringAsync().Result;
                    responseEncry = JsonConvert.DeserializeObject<YappayEncryptedResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                    string requestDecryptrdData = enc.decodeResponse(responseEncry);
                    objLog.WriteAppLog("Card Replacement - request From Transcorp  :" + requestDecryptrdData, lstrFolderName);
                    response = JsonConvert.DeserializeObject<CardReplacementResponse>(requestDecryptrdData);
                }
                else
                {
                    string respMesg = responseMessage.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<CardReplacementResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                    objLog.WriteAppLog("Card Replacement - request From Transcorp  :" + respMesg, lstrFolderName);
                }

                return response;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public FetchTransactionsResponse FetchTransactions(string extTrxId)
        {
            FetchTransactionsResponse response = new FetchTransactionsResponse();
            YappayEncryptedResponse responseEncry = new YappayEncryptedResponse();

            try
            {
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(CommonConstants.YappayBaseAddress);
                http.DefaultRequestHeaders.Accept.Clear();

                http.DefaultRequestHeaders.Add(CommonConstants.Tenant, CommonConstants.RefundCustomerTenantValue);
                http.DefaultRequestHeaders.TryAddWithoutValidation(CommonConstants.ContentType, CommonConstants.ContentTypeValue);

                objLog.WriteAppLog("Fetch Transactions - Plain Request to Transcorp Encrypted data  :" + extTrxId, lstrFolderName);
                string uniqRanNumber = Utility.GetTransID().Substring(18 - 16);
                string requestEncryptedData = enc.encodeRequest(extTrxId, uniqRanNumber, CommonConstants.CustomerTenantValue);
                objLog.WriteAppLog("Fetch Transactions - Request to Transcorp Request Encrypted Data  :" + requestEncryptedData, lstrFolderName);

                HttpResponseMessage responseMessage = http.GetAsync(CommonConstants.FetchTransactionsUrl + "/" + requestEncryptedData).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultValue = responseMessage.Content.ReadAsStringAsync().Result;
                    responseEncry = JsonConvert.DeserializeObject<YappayEncryptedResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                    string requestEncryptedData1 = enc.decodeResponse(responseEncry);

                    objLog.WriteAppLog("Fetch Transactions - Response From Transcorp  :" + requestEncryptedData1, lstrFolderName);
                    response = JsonConvert.DeserializeObject<FetchTransactionsResponse>(requestEncryptedData1);
                }
                else
                {
                    string respMesg = responseMessage.Content.ReadAsStringAsync().Result;
                    
                    objLog.WriteAppLog("Fetch Transactions - Response From Transcorp  :" + respMesg, lstrFolderName);
                }

                return response;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        public FetchTransactionsResponse FetchTransactionsByDates(FetchTransactionsbyDatesResult trnResult)
        {
            FetchTransactionsResponse response = new FetchTransactionsResponse();
            YappayEncryptedResponse responseEncry = new YappayEncryptedResponse();

            try
            {
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(CommonConstants.YappayBaseAddress);
                http.DefaultRequestHeaders.Accept.Clear();
                //DateTime temp = DateTime.ParseExact(trnResult.fromDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                string fromDate = trnResult.fromDate.ToString("yyyy-MM-dd");
                string toDate = trnResult.toDate.ToString("yyyy-MM-dd");

                string jsonReq = JsonConvert.SerializeObject(trnResult.entityId+ "?fromDate=" + fromDate+ "&toDate="+ toDate+ "&pageNumber =" + trnResult.pageNumber+ "&pageSize="+ trnResult.pageSize);

                http.DefaultRequestHeaders.Add(CommonConstants.Tenant, CommonConstants.RefundCustomerTenantValue);
                http.DefaultRequestHeaders.Add(PrepaidConstants.Authorization, PrepaidConstants.PartnerTokenValue);
                http.DefaultRequestHeaders.TryAddWithoutValidation(CommonConstants.ContentType, CommonConstants.ContentTypeValue);

                objLog.WriteAppLog("Fetch Transactions - Plain Request to Transcorp Encrypted data  :" + jsonReq, lstrFolderName);
                //string uniqRanNumber = Utility.GetTransID().Substring(18 - 16);
                //string requestEncryptedData = enc.encodeRequest(jsonReq, uniqRanNumber, CommonConstants.CustomerTenantValue);
                //objLog.WriteAppLog("Fetch Transactions - Request to Transcorp Request Encrypted Data  :" + requestEncryptedData, lstrFolderName);

                HttpResponseMessage responseMessage = http.GetAsync(CommonConstants.FetchTransactionsByDatesUrl + "/" + trnResult.entityId + "?fromDate=" + fromDate + "&toDate=" + toDate + "&pageNumber =" + trnResult.pageNumber + "&pageSize=" + trnResult.pageSize).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultValue = responseMessage.Content.ReadAsStringAsync().Result;
                    //responseEncry = JsonConvert.DeserializeObject<YappayEncryptedResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                    //string requestEncryptedData1 = enc.decodeResponse(responseEncry);

                    objLog.WriteAppLog("Fetch Transactions - Response From Transcorp  :" + resultValue, lstrFolderName);
                    response = JsonConvert.DeserializeObject<FetchTransactionsResponse>(resultValue);
                }
                else
                {
                    var resultValue = responseMessage.Content.ReadAsStringAsync().Result;
                    //responseEncry = JsonConvert.DeserializeObject<YappayEncryptedResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                    //string requestEncryptedData1 = enc.decodeResponse(responseEncry);

                    objLog.WriteAppLog("Fetch Transactions - Response From Transcorp  :" + resultValue, lstrFolderName);
                    response = JsonConvert.DeserializeObject<FetchTransactionsResponse>(resultValue);

                    //objLog.WriteAppLog("Fetch Transactions - Response From Transcorp  :" + respMesg, lstrFolderName);
                }

                return response;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        public MerchantPaymentResponse PaymentForMerchant(MerchantPaymentRequest merchantPaymentRequest)
        {
            MerchantPaymentResponse response = new MerchantPaymentResponse();
            YappayEncryptedResponse responseEncry = new YappayEncryptedResponse();

            try
            {
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(CommonConstants.YappayBaseAddress);
                http.DefaultRequestHeaders.Accept.Clear();

                http.DefaultRequestHeaders.Add(CommonConstants.Tenant, CommonConstants.TenantValue);
                http.DefaultRequestHeaders.TryAddWithoutValidation(CommonConstants.ContentType, CommonConstants.ContentTypeValue);

                string requestData = JsonConvert.SerializeObject(merchantPaymentRequest);

                objLog.WriteAppLog("Payment For Merchant - Plain Request to Transcorp Encrypted data  :" + requestData, lstrFolderName);
                string uniqRanNumber = Utility.GetTransID().Substring(18 - 16);
                string requestEncryptedData = enc.encodeRequest(requestData, uniqRanNumber, CommonConstants.CustomerTenantValue);
                objLog.WriteAppLog("Payment For Merchant - Request to Transcorp Request Encrypted Data  :" + requestEncryptedData, lstrFolderName);

                var stringContent = new StringContent(requestEncryptedData, Encoding.UTF8, CommonConstants.ApplicationJson);
                HttpResponseMessage responseMessage = http.PostAsync(CommonConstants.PaymentForMerchant, stringContent).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultValue = responseMessage.Content.ReadAsStringAsync().Result;

                    responseEncry = JsonConvert.DeserializeObject<YappayEncryptedResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                    string requestEncryptedData1 = enc.decodeResponse(responseEncry);

                    objLog.WriteAppLog("Payment For Merchant - Response From Transcorp  :" + requestEncryptedData1, lstrFolderName);

                    response = JsonConvert.DeserializeObject<MerchantPaymentResponse>(requestEncryptedData1);
                }
                else
                {
                    string respMesg = responseMessage.Content.ReadAsStringAsync().Result;
                    
                    objLog.WriteAppLog("Payment For Merchant - Response From Transcorp  :" + respMesg, lstrFolderName);
                }

                return response;
            }
            catch (System.Exception ex)   
            {
                throw ex;
            }
        }
        public CardModelResponse GetCardModelByEncry(CardModelRequest cardModel)
        {
            CardModelResponse response = new CardModelResponse();
            YappayEncryptedResponse responseEncry = new YappayEncryptedResponse();
            try
            {
                string jsonReq = JsonConvert.SerializeObject(cardModel);
                objLog.WriteAppLog("Customer Wallet - Request from client app  :" + jsonReq, lstrFolderName);

                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(PrepaidConstants.PrepaidBaseAddressByEncry);
                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Add(CommonConstants.Tenant, CommonConstants.RefundCustomerTenantValue);
                http.DefaultRequestHeaders.Add(PrepaidConstants.Authorization, PrepaidConstants.PartnerTokenValue);
                http.DefaultRequestHeaders.TryAddWithoutValidation(PrepaidConstants.ContentType, PrepaidConstants.ApplicationJson);
                // encryption

                //objLog.WriteAppLog("Send OTP to Register Customer - Request to Transcorp plain data  :" + requestData, lstrFolderName);
                string uniqRanNumber = Utility.GetTransID().Substring(18 - 16);
                string requestEncryptedData = enc.encodeRequest(jsonReq, uniqRanNumber, CommonConstants.CustomerTenantValue);
                objLog.WriteAppLog("Customer Wallet - Request to Transcorp Request Encrypted Data  :" + requestEncryptedData, lstrFolderName);
                var stringContent = new StringContent(requestEncryptedData, Encoding.UTF8, PrepaidConstants.ApplicationJson);

                //var stringContent = new StringContent(JsonConvert.SerializeObject(loadWalletRequest), Encoding.UTF8, CommonConstants.ApplicationJson);
                HttpResponseMessage responseMessage = http.PostAsync(CommonConstants.GetCardDetailsV3RequestURL, stringContent).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultValue = responseMessage.Content.ReadAsStringAsync().Result;
                    responseEncry = JsonConvert.DeserializeObject<YappayEncryptedResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                    string requestDecryptrdData = enc.decodeResponse(responseEncry);
                    objLog.WriteAppLog("Customer Wallet - Response From Transcorp  :" + requestDecryptrdData, lstrFolderName);
                    response = JsonConvert.DeserializeObject<CardModelResponse>(requestDecryptrdData);
                }
                else
                {
                    string respMesg = responseMessage.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<CardModelResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                    objLog.WriteAppLog("Send OTP to Register Customer - Response From Transcorp  :" + respMesg, lstrFolderName);
                }

                return response;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        public CardCVVModelResponse GetCardCVVModelByEncry(GetCVVCardModelRequest cardModel)
        {
            CardCVVModelResponse response = new CardCVVModelResponse();
            YappayEncryptedResponse responseEncry = new YappayEncryptedResponse();
            try
            {
                string jsonReq = JsonConvert.SerializeObject(cardModel);
                objLog.WriteAppLog("Customer Wallet - Request from client app  :" + jsonReq, lstrFolderName);

                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(PrepaidConstants.PrepaidBaseAddressByEncry);
                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Add(CommonConstants.Tenant, CommonConstants.RefundCustomerTenantValue);
                http.DefaultRequestHeaders.Add(PrepaidConstants.Authorization, PrepaidConstants.PartnerTokenValue);
                http.DefaultRequestHeaders.TryAddWithoutValidation(PrepaidConstants.ContentType, PrepaidConstants.ApplicationJson);
                // encryption

                //objLog.WriteAppLog("Send OTP to Register Customer - Request to Transcorp plain data  :" + requestData, lstrFolderName);
                string uniqRanNumber = Utility.GetTransID().Substring(18 - 16);
                string requestEncryptedData = enc.encodeRequest(jsonReq, uniqRanNumber, CommonConstants.CustomerTenantValue);
                objLog.WriteAppLog("Customer Wallet - Request to Transcorp Request Encrypted Data  :" + requestEncryptedData, lstrFolderName);
                var stringContent = new StringContent(requestEncryptedData, Encoding.UTF8, PrepaidConstants.ApplicationJson);

                //var stringContent = new StringContent(JsonConvert.SerializeObject(loadWalletRequest), Encoding.UTF8, CommonConstants.ApplicationJson);
                HttpResponseMessage responseMessage = http.PostAsync(CommonConstants.GetCVVRequestURL, stringContent).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultValue = responseMessage.Content.ReadAsStringAsync().Result;
                    responseEncry = JsonConvert.DeserializeObject<YappayEncryptedResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                    string requestDecryptrdData = enc.decodeResponse(responseEncry);
                    objLog.WriteAppLog("Customer Wallet - Response From Transcorp  :" + requestDecryptrdData, lstrFolderName);
                    response = JsonConvert.DeserializeObject<CardCVVModelResponse>(requestDecryptrdData);
                }
                else
                {
                    string respMesg = responseMessage.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<CardCVVModelResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                    objLog.WriteAppLog("Send OTP to Register Customer - Response From Transcorp  :" + respMesg, lstrFolderName);
                }

                return response;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

    }
}
