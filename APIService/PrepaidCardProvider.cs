using BusinessModel.Common;
using BusinessModel.Prepaid;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Configuration;
using static System.Net.WebRequestMethods;
using BusinessModel.GST_Validation;

namespace APIService
{
    public class PrepaidCardProvider
    {
        public const string lstrFolderName = "Prepaid Cards";
        Log objLog = new Log();
        M2BEncrypt enc = new M2BEncrypt();
        public PrepaidCardProvider()
        {

        }
        public PrepaidResponse SendMobOTPByPlainJSON(PrepaidSendOTPRequest prepaidSendOTP)
        {
            PrepaidResponse response = new PrepaidResponse();

            try
            {
                string jsonReq = JsonConvert.SerializeObject(prepaidSendOTP);
                objLog.WriteAppLog("Send OTP to Register Customer - Request  :" + jsonReq, lstrFolderName);
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(PrepaidConstants.PrepaidBaseAddress);
                http.DefaultRequestHeaders.Accept.Clear();

                http.DefaultRequestHeaders.Add(CommonConstants.Tenant, CommonConstants.RefundCustomerTenantValue);
                http.DefaultRequestHeaders.Add(PrepaidConstants.PartnerId, PrepaidConstants.TenantValue);
                http.DefaultRequestHeaders.Add(PrepaidConstants.PartnerToken, PrepaidConstants.PartnerTokenValue);
                http.DefaultRequestHeaders.TryAddWithoutValidation(PrepaidConstants.ContentType, PrepaidConstants.ApplicationJson);

                prepaidSendOTP.mobileNumber = "+91" + prepaidSendOTP.mobileNumber;
                prepaidSendOTP.entityId = "nxtign" + prepaidSendOTP.mobileNumber.Substring(10 - 4);
                var stringContent = new StringContent(JsonConvert.SerializeObject(prepaidSendOTP), Encoding.UTF8, PrepaidConstants.ApplicationJson);
                objLog.WriteAppLog("Send OTP to Register Customer - Request to Transcorp  :" + stringContent, lstrFolderName);
                HttpResponseMessage responseMessage = http.PostAsync(PrepaidConstants.OTPRequestURL, stringContent).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultValue = responseMessage.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<PrepaidResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                    objLog.WriteAppLog("Send OTP to Register Customer - Response From Transcorp  :" + resultValue, lstrFolderName);
                    //response = ConvertToOTPResponseModel(resultValue);
                }
                else
                {
                    objLog.WriteAppLog("Send OTP to Register Customer - Response From Transcorp  : null", lstrFolderName);
                }

                return response;
            }
            catch (System.Exception ex)
            {
                objLog.WriteErrorLog("Send OTP to Register Customer - Response From Transcorp Error :" + ex.ToString(), lstrFolderName);
                throw ex;
            }
        }

        public PrepaidResponse SendMobileOTPByEncry(PrepaidSendOTPRequest prepaidSendOTP)
        {
            PrepaidResponse response = new PrepaidResponse();
            YappayEncryptedResponse responseEncry = new YappayEncryptedResponse();
            try
            {
                PrepaidSendOTPRequestWithID ppRequest = new PrepaidSendOTPRequestWithID();
                string jsonReq = JsonConvert.SerializeObject(prepaidSendOTP);
                objLog.WriteAppLog("Send OTP to Register Customer - Request from client app  :" + jsonReq, lstrFolderName);
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(PrepaidConstants.PrepaidBaseAddressByEncry);
                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Add(CommonConstants.Tenant, CommonConstants.RefundCustomerTenantValue);
                http.DefaultRequestHeaders.Add(PrepaidConstants.Authorization, PrepaidConstants.PartnerTokenValue);
                http.DefaultRequestHeaders.TryAddWithoutValidation(PrepaidConstants.ContentType, PrepaidConstants.ApplicationJson);

                prepaidSendOTP.entityId = "nxtign" + prepaidSendOTP.mobileNumber;
                prepaidSendOTP.mobileNumber = "+91" + prepaidSendOTP.mobileNumber;
                //prepaidSendOTP.entityId = "nxtign" + prepaidSendOTP.mobileNumber.Substring(10 - 4);

                ppRequest.mobileNumber = prepaidSendOTP.mobileNumber;
                ppRequest.entityId = prepaidSendOTP.entityId;
                string requestData = JsonConvert.SerializeObject(ppRequest);

                objLog.WriteAppLog("Send OTP to Register Customer - Request to Transcorp plain data  :" + requestData, lstrFolderName);
                string uniqRanNumber = Utility.GetTransID().Substring(18 - 16);
                string requestEncryptedData = enc.encodeRequest(requestData, uniqRanNumber, CommonConstants.CustomerTenantValue);
                objLog.WriteAppLog("Send OTP to Register Customer - Request to Transcorp Request Encrypted Data  :" + requestEncryptedData, lstrFolderName);
                //var stringContent = new StringContent(JsonConvert.SerializeObject(requestEncryptedData), Encoding.UTF8, PrepaidConstants.ApplicationJson);
                var stringContent = new StringContent(requestEncryptedData, Encoding.UTF8, PrepaidConstants.ApplicationJson);

                HttpResponseMessage responseMessage = http.PostAsync(PrepaidConstants.OTPRequestURL, stringContent).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultValue = responseMessage.Content.ReadAsStringAsync().Result;
                    responseEncry = JsonConvert.DeserializeObject<YappayEncryptedResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                    string requestEncryptedData1 = enc.decodeResponse(responseEncry);
                    objLog.WriteAppLog("Send OTP to Register Customer - Response From Transcorp  :" + requestEncryptedData1, lstrFolderName);
                    response = JsonConvert.DeserializeObject<PrepaidResponse>(requestEncryptedData1);
                }
                else
                {
                    string respMesg = responseMessage.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<PrepaidResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                    objLog.WriteAppLog("Send OTP to Register Customer - Response From Transcorp  :" + respMesg, lstrFolderName);
                }

                return response;
            }
            catch (System.Exception ex)
            {
                objLog.WriteErrorLog("Send OTP to Register Customer - Response From Transcorp Error :" + ex.ToString(), lstrFolderName);
                throw ex;
            }
        }

        public PrepaidResponse RegisterUserByEncry(PrepaidModel prepaidModel)
        {
            PrepaidResponse response = new PrepaidResponse();
            YappayEncryptedResponse responseEncry = new YappayEncryptedResponse();
            try
            {
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(PrepaidConstants.PrepaidBaseAddressByEncry);
                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Add(CommonConstants.Tenant, CommonConstants.RefundCustomerTenantValue);
                http.DefaultRequestHeaders.Add(PrepaidConstants.Authorization, PrepaidConstants.PartnerTokenValue);
                http.DefaultRequestHeaders.TryAddWithoutValidation(PrepaidConstants.ContentType, PrepaidConstants.ApplicationJson);
                //var stringContent = new StringContent(JsonConvert.SerializeObject(prepaidModel), Encoding.UTF8, PrepaidConstants.ApplicationJson);

                string requestData = JsonConvert.SerializeObject(prepaidModel);

                objLog.WriteAppLog("Register Customer - Plain Request to Transcorp Encrypted data  :" + requestData, lstrFolderName);
                string uniqRanNumber = Utility.GetTransID().Substring(18 - 16);
                string requestEncryptedData = enc.encodeRequest(requestData, uniqRanNumber, CommonConstants.CustomerTenantValue);
                objLog.WriteAppLog("Register Customer - Request to Transcorp Request Encrypted Data  :" + requestEncryptedData, lstrFolderName);
                var stringContent = new StringContent(requestEncryptedData, Encoding.UTF8, PrepaidConstants.ApplicationJson);

                HttpResponseMessage responseMessage = http.PostAsync(PrepaidConstants.UserRegisterURL, stringContent).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultValue = responseMessage.Content.ReadAsStringAsync().Result;
                    objLog.WriteAppLog("Register Customer - Decrypted Response From Transcorp  :" + resultValue, lstrFolderName);
                    responseEncry = JsonConvert.DeserializeObject<YappayEncryptedResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                    string requestEncryptedData1 = enc.decodeResponse(responseEncry);

                    objLog.WriteAppLog("Register Customer - Plain Response From Transcorp  :" + requestEncryptedData1, lstrFolderName);
                    response = JsonConvert.DeserializeObject<PrepaidResponse>(requestEncryptedData1);
                }
                else
                {
                    string respMesg = responseMessage.Content.ReadAsStringAsync().Result;
                    responseEncry = JsonConvert.DeserializeObject<YappayEncryptedResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                    //string requestEncryptedData1 = enc.decodeResponse(responseEncry);
                    //response = JsonConvert.DeserializeObject<PrepaidResponse>(requestEncryptedData1);
                    objLog.WriteAppLog("Register Customer - Response From Transcorp  :" + respMesg, lstrFolderName);
                }
                return response;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        public PrepaidResponse RegisterUserPlainJSON(PrepaidModel prepaidModel)
        {
            PrepaidResponse response = new PrepaidResponse();
            try
            {
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(PrepaidConstants.PrepaidBaseAddress);
                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Add(CommonConstants.Tenant, CommonConstants.RefundCustomerTenantValue);
                http.DefaultRequestHeaders.Add(PrepaidConstants.PartnerId, PrepaidConstants.TenantValue);
                http.DefaultRequestHeaders.Add(PrepaidConstants.PartnerToken, PrepaidConstants.PartnerTokenValue);
                http.DefaultRequestHeaders.TryAddWithoutValidation(PrepaidConstants.ContentType, PrepaidConstants.ApplicationJson);


                var stringContent = new StringContent(JsonConvert.SerializeObject(prepaidModel), Encoding.UTF8, PrepaidConstants.ApplicationJson);
                HttpResponseMessage responseMessage = http.PostAsync(PrepaidConstants.UserRegisterURL, stringContent).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultValue = responseMessage.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<PrepaidResponse>(responseMessage.Content.ReadAsStringAsync().Result);

                }
                else
                {
                    var msg = responseMessage.RequestMessage.ToString();
                    response = JsonConvert.DeserializeObject<PrepaidResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                }

                return response;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
        }

        public UpdateCustomerResponse UpdateCustomer(UpdateCustomerRequest updateCustomerRequest)
        {
            UpdateCustomerResponse response = new UpdateCustomerResponse();
            YappayEncryptedResponse responseEncry = new YappayEncryptedResponse();
            try
            {
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(PrepaidConstants.PrepaidBaseAddressByEncry);
                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Add(CommonConstants.Tenant, CommonConstants.RefundCustomerTenantValue);
                http.DefaultRequestHeaders.Add(PrepaidConstants.Authorization, PrepaidConstants.PartnerTokenValue);
                http.DefaultRequestHeaders.TryAddWithoutValidation(PrepaidConstants.ContentType, PrepaidConstants.ApplicationJson);
                string requestData = JsonConvert.SerializeObject(updateCustomerRequest);

                objLog.WriteAppLog("Customer Preferences External - Plain Request to Transcorp Encrypted data  :" + requestData, lstrFolderName);
                string uniqRanNumber = Utility.GetTransID().Substring(18 - 16);
                string requestEncryptedData = enc.encodeRequest(requestData, uniqRanNumber, CommonConstants.CustomerTenantValue);
                objLog.WriteAppLog("Customer Preferences External - Request to Transcorp Request Encrypted Data  :" + requestEncryptedData, lstrFolderName);

                var stringContent = new StringContent(requestEncryptedData, Encoding.UTF8, CommonConstants.ApplicationJson);
                HttpResponseMessage responseMessage = http.PostAsync(CommonConstants.UpdateCustomerUrl, stringContent).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultValue = responseMessage.Content.ReadAsStringAsync().Result;
                    responseEncry = JsonConvert.DeserializeObject<YappayEncryptedResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                    string requestEncryptedData1 = enc.decodeResponse(responseEncry);
                    objLog.WriteAppLog("Customer Preferences External - Response From Transcorp  :" + requestEncryptedData1, lstrFolderName);
                    response = JsonConvert.DeserializeObject<UpdateCustomerResponse>(requestEncryptedData1);
                }
                else
                {
                    string respMesg = responseMessage.Content.ReadAsStringAsync().Result;
                    responseEncry = JsonConvert.DeserializeObject<YappayEncryptedResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                    string requestEncryptedData1 = enc.decodeResponse(responseEncry);
                    objLog.WriteAppLog("Customer Preferences External - Response From Transcorp  :" + requestEncryptedData1, lstrFolderName);

                    response = JsonConvert.DeserializeObject<UpdateCustomerResponse>(requestEncryptedData1);

                }

                return response;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public CustomerTransactionLimitResponse CustomerTransactionLimit(CustomerDailyTransactionLimitRequest customerTransactionLimitRequest)
        {
            CustomerTransactionLimitResponse response = new CustomerTransactionLimitResponse();
            YappayEncryptedResponse responseEncry = new YappayEncryptedResponse();
            try
            {
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(PrepaidConstants.PrepaidBaseAddressByEncry);
                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Add(CommonConstants.Tenant, CommonConstants.RefundCustomerTenantValue);
                http.DefaultRequestHeaders.Add(PrepaidConstants.Authorization, PrepaidConstants.PartnerTokenValue);
                http.DefaultRequestHeaders.TryAddWithoutValidation(PrepaidConstants.ContentType, PrepaidConstants.ApplicationJson);
                string requestData = JsonConvert.SerializeObject(customerTransactionLimitRequest);

                objLog.WriteAppLog("Customer Preferences External - Plain Request to Transcorp Encrypted data  :" + requestData, lstrFolderName);
                string uniqRanNumber = Utility.GetTransID().Substring(18 - 16);
                string requestEncryptedData = enc.encodeRequest(requestData, uniqRanNumber, CommonConstants.CustomerTenantValue);
                objLog.WriteAppLog("Customer Preferences External - Request to Transcorp Request Encrypted Data  :" + requestEncryptedData, lstrFolderName);

                var stringContent = new StringContent(requestEncryptedData, Encoding.UTF8, CommonConstants.ApplicationJson);
                HttpResponseMessage responseMessage = http.PostAsync(CommonConstants.CustomerTransactionLimitUrl, stringContent).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultValue = responseMessage.Content.ReadAsStringAsync().Result;

                    responseEncry = JsonConvert.DeserializeObject<YappayEncryptedResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                    string requestEncryptedData1 = enc.decodeResponse(responseEncry);

                    objLog.WriteAppLog("Customer Preferences External - Response From Transcorp  :" + requestEncryptedData1, lstrFolderName);

                    response = JsonConvert.DeserializeObject<CustomerTransactionLimitResponse>(requestEncryptedData1);
                }
                else
                {
                    string respMesg = responseMessage.Content.ReadAsStringAsync().Result;
                    responseEncry = JsonConvert.DeserializeObject<YappayEncryptedResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                    string requestEncryptedData1 = enc.decodeResponse(responseEncry);
                    objLog.WriteAppLog("Customer Preferences External - Response From Transcorp  :" + requestEncryptedData1, lstrFolderName);

                    response = JsonConvert.DeserializeObject<CustomerTransactionLimitResponse>(requestEncryptedData1);
      
                }

                return response;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public AddCardResponse AddCard(AddCardRequest addCardRequest)
        {
            AddCardResponse response = new AddCardResponse();

            try
            {
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(CommonConstants.YappayBaseAddress);
                http.DefaultRequestHeaders.Accept.Clear();

                http.DefaultRequestHeaders.Add(CommonConstants.Tenant, CommonConstants.TenantValue);
                http.DefaultRequestHeaders.TryAddWithoutValidation(CommonConstants.ContentType, CommonConstants.ContentTypeValue);

                var stringContent = new StringContent(JsonConvert.SerializeObject(addCardRequest), Encoding.UTF8, CommonConstants.ApplicationJson);
                HttpResponseMessage responseMessage = http.PostAsync(CommonConstants.AddCardUrl, stringContent).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultValue = responseMessage.Content.ReadAsStringAsync().Result;
                    response = JsonConvert.DeserializeObject<AddCardResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                }

                return response;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public string POSTDataICICI(string URL, string body, string ContentType, string lstrHeaderAPIKEY, ref string lstrError)
        {
            string str = string.Empty;
            Log log = new Log();
            try
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
                httpWebRequest.ContentType = ContentType;
                httpWebRequest.Method = "POST";
                byte[] bytes = Encoding.UTF8.GetBytes(body);
                httpWebRequest.Headers.Add("apikey", lstrHeaderAPIKEY);
                log.WriteAppLog(URL + ":" + body + ":" + ContentType + ":" + lstrHeaderAPIKEY, lstrFolderName);
                using (Stream requestStream = httpWebRequest.GetRequestStream())
                {
                    requestStream.Write(bytes, 0, bytes.Length);
                    requestStream.Close();
                }
                using (StreamReader streamReader = new StreamReader(httpWebRequest.GetResponse().GetResponseStream(), Encoding.UTF8))
                    str = streamReader.ReadToEnd();
                return str;
            }
            catch (WebException ex)
            {
                log.WriteErrorLog("POSTDataICICI :" + ex.ToString(), lstrFolderName);
                lstrError = ex.ToString();
                return "";
            }
            catch (Exception ex)
            {
                log.WriteErrorLog("POSTDataICICI :" + ex.ToString(), lstrFolderName);
                lstrError = ex.ToString();
                return "";
            }

        }

        private string POSTData(string lstrHTTPMethod, string URL, string AccessToken, string body, string ContentType)
        {
            Log objLog = new Log();
            string lstrOut = string.Empty;
            try
            {
                //ServicePointManager.Expect100Continue = true;
                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(URL);
                httpWebRequest.Timeout = 40 * 1000;
                httpWebRequest.ContentType = ContentType;// "application/json";
                httpWebRequest.Method = lstrHTTPMethod;
                byte[] bytes = Encoding.UTF8.GetBytes(body);
                httpWebRequest.Headers.Add("Authorization", PrepaidConstants.PartnerTokenValue);
                httpWebRequest.Headers.Add(CommonConstants.Tenant, CommonConstants.RefundCustomerTenantValue);

                httpWebRequest.ProtocolVersion = HttpVersion.Version10;


                using (System.IO.Stream sendStream = httpWebRequest.GetRequestStream())
                {
                    sendStream.Write(bytes, 0, bytes.Length);
                    sendStream.Close();
                }

                HttpWebResponse myHttpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                if (myHttpWebResponse.StatusCode == HttpStatusCode.OK)
                {
                    //lstrOut = myHttpWebResponse.StatusDescription;
                }

                //System.Net.WebResponse res = httpWebRequest.GetResponse();
                System.IO.Stream ReceiveStream = myHttpWebResponse.GetResponseStream();
                using (System.IO.StreamReader sr = new System.IO.StreamReader(ReceiveStream, Encoding.UTF8))
                {
                    lstrOut = sr.ReadToEnd();
                }
                return lstrOut;
            }
            catch (WebException ex)
            {
                using (WebResponse response = ex.Response)
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)response;
                    using (Stream data = response.GetResponseStream())
                    using (var reader = new StreamReader(data))
                    {
                        lstrOut = reader.ReadToEnd();
                        objLog.WriteErrorLog("POSTData exception :" + lstrOut, lstrFolderName);
                    }
                }

                objLog.WriteErrorLog("POSTData :" + ex.ToString(), lstrFolderName);
                lstrOut = "";
                return lstrOut;
            }
            catch (Exception ex)
            {
                objLog.WriteErrorLog("POSTData :" + ex.ToString(), lstrFolderName);
                lstrOut = "";
                return lstrOut;
            }
        }

        public CustomerPreferencesExternalResponse CustomerPreferencesExternal(CustomerPreferencesExternalRequest customerPreferencesExternalRequest)
        {
            CustomerPreferencesExternalResponse response = new CustomerPreferencesExternalResponse();
            YappayEncryptedResponse responseEncry = new YappayEncryptedResponse();

            try
            {

                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(PrepaidConstants.PrepaidBaseAddressByEncry);
                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Add(CommonConstants.Tenant, CommonConstants.RefundCustomerTenantValue);
                http.DefaultRequestHeaders.Add(PrepaidConstants.Authorization, PrepaidConstants.PartnerTokenValue);
                http.DefaultRequestHeaders.TryAddWithoutValidation(PrepaidConstants.ContentType, PrepaidConstants.ApplicationJson);

                string requestData = JsonConvert.SerializeObject(customerPreferencesExternalRequest);

                objLog.WriteAppLog("Customer Preferences External - Plain Request to Transcorp Encrypted data  :" + requestData, lstrFolderName);
                string uniqRanNumber = Utility.GetTransID().Substring(18 - 16);
                string requestEncryptedData = enc.encodeRequest(requestData, uniqRanNumber, CommonConstants.CustomerTenantValue);
                objLog.WriteAppLog("Customer Preferences External - Request to Transcorp Request Encrypted Data  :" + requestEncryptedData, lstrFolderName);

                var stringContent = new StringContent(requestEncryptedData, Encoding.UTF8, CommonConstants.ApplicationJson);
                HttpResponseMessage responseMessage = http.PostAsync(CommonConstants.CustomerPreferencesNewExternalUrl, stringContent).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultValue = responseMessage.Content.ReadAsStringAsync().Result;

                    responseEncry = JsonConvert.DeserializeObject<YappayEncryptedResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                    string requestEncryptedData1 = enc.decodeResponse(responseEncry);

                    objLog.WriteAppLog("Customer Preferences External - Response From Transcorp  :" + requestEncryptedData1, lstrFolderName);

                    response = JsonConvert.DeserializeObject<CustomerPreferencesExternalResponse>(requestEncryptedData1);
                }
                else
                {
                    string respMesg = responseMessage.Content.ReadAsStringAsync().Result;
                    responseEncry = JsonConvert.DeserializeObject<YappayEncryptedResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                    //string requestEncryptedData1 = enc.decodeResponse(responseEncry);
                    //response = JsonConvert.DeserializeObject<PrepaidResponse>(requestEncryptedData1);
                    objLog.WriteAppLog("Customer Preferences External - Response From Transcorp  :" + respMesg, lstrFolderName);
                }

                return response;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }


        public FetchCustomerPreferencesResponse FetchCustomerPreferencesExternal(FetchCustomerPreferenceslRequest FetchcustomerPreferencesExternalRequest)
        {
            FetchCustomerPreferencesResponse response = new FetchCustomerPreferencesResponse();
            YappayEncryptedResponse responseEncry = new YappayEncryptedResponse();

            try
            {

                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(PrepaidConstants.PrepaidBaseAddressByEncry);
                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Add(CommonConstants.Tenant, CommonConstants.RefundCustomerTenantValue);
                http.DefaultRequestHeaders.Add(PrepaidConstants.Authorization, PrepaidConstants.PartnerTokenValue);
                http.DefaultRequestHeaders.TryAddWithoutValidation(PrepaidConstants.ContentType, PrepaidConstants.ApplicationJson);

                string requestData = JsonConvert.SerializeObject(FetchcustomerPreferencesExternalRequest);

                objLog.WriteAppLog("Customer Preferences External - Plain Request to Transcorp Encrypted data  :" + requestData, lstrFolderName);
                string uniqRanNumber = Utility.GetTransID().Substring(18 - 16);
                string requestEncryptedData = enc.encodeRequest(requestData, uniqRanNumber, CommonConstants.CustomerTenantValue);
                objLog.WriteAppLog("Customer Preferences External - Request to Transcorp Request Encrypted Data  :" + requestEncryptedData, lstrFolderName);

                var stringContent = new StringContent(requestEncryptedData, Encoding.UTF8, CommonConstants.ApplicationJson);
                HttpResponseMessage responseMessage = http.PostAsync(CommonConstants.FetchCustomerPreferencesNewUrl, stringContent).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultValue = responseMessage.Content.ReadAsStringAsync().Result;

                    responseEncry = JsonConvert.DeserializeObject<YappayEncryptedResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                    string requestEncryptedData1 = enc.decodeResponse(responseEncry);

                    objLog.WriteAppLog("Customer Preferences External - Response From Transcorp  :" + requestEncryptedData1, lstrFolderName);

                    response = JsonConvert.DeserializeObject<FetchCustomerPreferencesResponse>(requestEncryptedData1);
                }
                else
                {
                    string respMesg = responseMessage.Content.ReadAsStringAsync().Result;
                    responseEncry = JsonConvert.DeserializeObject<YappayEncryptedResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                    //string requestEncryptedData1 = enc.decodeResponse(responseEncry);
                    //response = JsonConvert.DeserializeObject<PrepaidResponse>(requestEncryptedData1);
                    objLog.WriteAppLog("Customer Preferences External - Response From Transcorp  :" + respMesg, lstrFolderName);
                }

                return response;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }


        public UpdateKYCStatusResponse UpdateKYCStatus(UpdateKYCStatusRequest updateKYCStatusRequest)
        {
            UpdateKYCStatusResponse response = new UpdateKYCStatusResponse();
            YappayEncryptedResponse responseEncry = new YappayEncryptedResponse();

            try
            {
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(CommonConstants.YappayBaseAddress);
                http.DefaultRequestHeaders.Accept.Clear();

                http.DefaultRequestHeaders.Add(CommonConstants.Tenant, CommonConstants.TenantValue);
                http.DefaultRequestHeaders.TryAddWithoutValidation(CommonConstants.ContentType, CommonConstants.ContentTypeValue);

                string requestData = JsonConvert.SerializeObject(updateKYCStatusRequest);

                objLog.WriteAppLog("Update KYC Status - Plain Request to Transcorp Encrypted data  :" + requestData, lstrFolderName);
                string uniqRanNumber = Utility.GetTransID().Substring(18 - 16);
                string requestEncryptedData = enc.encodeRequest(requestData, uniqRanNumber, CommonConstants.CustomerTenantValue);
                objLog.WriteAppLog("Update KYC Status - Request to Transcorp Request Encrypted Data  :" + requestEncryptedData, lstrFolderName);

                var stringContent = new StringContent(requestEncryptedData, Encoding.UTF8, CommonConstants.ApplicationJson);
                HttpResponseMessage responseMessage = http.PostAsync(CommonConstants.UpdateKYCStatusUrl, stringContent).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultValue = responseMessage.Content.ReadAsStringAsync().Result;

                    responseEncry = JsonConvert.DeserializeObject<YappayEncryptedResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                    string requestEncryptedData1 = enc.decodeResponse(responseEncry);

                    objLog.WriteAppLog("Update KYC Status - Response From Transcorp  :" + requestEncryptedData1, lstrFolderName);

                    response = JsonConvert.DeserializeObject<UpdateKYCStatusResponse>(requestEncryptedData1);
                }
                else
                {
                    string respMesg = responseMessage.Content.ReadAsStringAsync().Result;
                    responseEncry = JsonConvert.DeserializeObject<YappayEncryptedResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                    //string requestEncryptedData1 = enc.decodeResponse(responseEncry);
                    //response = JsonConvert.DeserializeObject<PrepaidResponse>(requestEncryptedData1);
                    objLog.WriteAppLog("Update KYC Status - Response From Transcorp  :" + respMesg, lstrFolderName);
                }

                return response;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public SetPinResponse SetPin(SetPinRequest setPinRequest)
        {
            SetPinResponse response = new SetPinResponse();
            YappayEncryptedResponse responseEncry = new YappayEncryptedResponse();

            try
            {
                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(CommonConstants.YappayBaseAddress);
                http.DefaultRequestHeaders.Accept.Clear();

                http.DefaultRequestHeaders.Add(CommonConstants.Tenant, CommonConstants.TenantValue);
                http.DefaultRequestHeaders.TryAddWithoutValidation(CommonConstants.ContentType, CommonConstants.ContentTypeValue);

                string requestData = JsonConvert.SerializeObject(setPinRequest);

                objLog.WriteAppLog("SetPin - Plain Request to Transcorp Encrypted data  :" + requestData, lstrFolderName);
                string uniqRanNumber = Utility.GetTransID().Substring(18 - 16);
                string requestEncryptedData = enc.encodeRequest(requestData, uniqRanNumber, CommonConstants.CustomerTenantValue);
                objLog.WriteAppLog("SetPin - Request to Transcorp Request Encrypted Data  :" + requestEncryptedData, lstrFolderName);

                var stringContent = new StringContent(requestEncryptedData, Encoding.UTF8, CommonConstants.ApplicationJson);
                HttpResponseMessage responseMessage = http.PostAsync(CommonConstants.SetPinUrl, stringContent).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultValue = responseMessage.Content.ReadAsStringAsync().Result;

                    responseEncry = JsonConvert.DeserializeObject<YappayEncryptedResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                    string requestEncryptedData1 = enc.decodeResponse(responseEncry);

                    objLog.WriteAppLog("SetPin - Response From Transcorp  :" + requestEncryptedData1, lstrFolderName);

                    response = JsonConvert.DeserializeObject<SetPinResponse>(requestEncryptedData1);
                }
                else
                {
                    string respMesg = responseMessage.Content.ReadAsStringAsync().Result;
                    responseEncry = JsonConvert.DeserializeObject<YappayEncryptedResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                    //string requestEncryptedData1 = enc.decodeResponse(responseEncry);
                    //response = JsonConvert.DeserializeObject<PrepaidResponse>(requestEncryptedData1);
                    objLog.WriteAppLog("SetPin - Response From Transcorp  :" + respMesg, lstrFolderName);
                }

                return response;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }


        public CardWidgetDetailsResonse FetchCardWidget(CardWidgetsRequest cardWidgetRequest)
        {
            CardWidgetDetailsResonse response = new CardWidgetDetailsResonse();
            YappayEncryptedResponse responseEncry = new YappayEncryptedResponse();
            CardWidgetDetailsRequest cardWidgetDetailsRequest=new CardWidgetDetailsRequest();
            try
            {
                string encodedStr = Convert.ToBase64String(Encoding.UTF8.GetBytes(cardWidgetRequest.kitNo + cardWidgetRequest.entityId + cardWidgetRequest.dob));
                //string inputStr = Encoding.UTF8.GetString(Convert.FromBase64String(encodedStr));
                cardWidgetDetailsRequest.token= encodedStr;
                cardWidgetDetailsRequest.kitNo = cardWidgetRequest.kitNo;
                cardWidgetDetailsRequest.entityId = cardWidgetRequest.entityId;
                cardWidgetDetailsRequest.appGuid = CommonConstants.AppGuid;
                cardWidgetDetailsRequest.business = CommonConstants.Business;
                cardWidgetDetailsRequest.callbackUrl = CommonConstants.CallBackUrl;
                cardWidgetDetailsRequest.dob = cardWidgetRequest.dob;

                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(PrepaidConstants.PrepaidBaseAddressByEncry);
                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Add(CommonConstants.Tenant, CommonConstants.RefundCustomerTenantValue);
                http.DefaultRequestHeaders.Add(PrepaidConstants.Authorization, PrepaidConstants.PartnerTokenValue);
                http.DefaultRequestHeaders.TryAddWithoutValidation(PrepaidConstants.ContentType, PrepaidConstants.ApplicationJson);
                string requestData = JsonConvert.SerializeObject(cardWidgetDetailsRequest);

                objLog.WriteAppLog("FetchCardWidget - Plain Request to Transcorp Encrypted data  :" + requestData, lstrFolderName);
                string uniqRanNumber = Utility.GetTransID().Substring(18 - 16);
                string requestEncryptedData = enc.encodeRequest(requestData, uniqRanNumber, CommonConstants.CustomerTenantValue);
                objLog.WriteAppLog("FetchCardWidget - Request to Transcorp Request Encrypted Data  :" + requestEncryptedData, lstrFolderName);

                var stringContent = new StringContent(requestEncryptedData, Encoding.UTF8, CommonConstants.ApplicationJson);

                HttpResponseMessage responseMessage = http.PostAsync(CommonConstants.FetchCardWidgetUrl, stringContent).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultValue = responseMessage.Content.ReadAsStringAsync().Result;
                    responseEncry = JsonConvert.DeserializeObject<YappayEncryptedResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                    string requestEncryptedData1 = enc.decodeResponse(responseEncry);
                    objLog.WriteAppLog("FetchCardWidget - Response From Transcorp  :" + requestEncryptedData1, lstrFolderName);
                    response = JsonConvert.DeserializeObject<CardWidgetDetailsResonse>(requestEncryptedData1);
                }
                else
                {
                    string respMesg = responseMessage.Content.ReadAsStringAsync().Result;
                    responseEncry = JsonConvert.DeserializeObject<YappayEncryptedResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                    string requestEncryptedData1 = enc.decodeResponse(responseEncry);
                    objLog.WriteAppLog("FetchCardWidget - Response From Transcorp  :" + requestEncryptedData1, lstrFolderName);

                    response = JsonConvert.DeserializeObject<CardWidgetDetailsResonse>(requestEncryptedData1);

                }

                return response;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public CardWidgetDetailsResonse SetPinWidget(CardWidgetsRequest cardWidgetRequest)
        {
            CardWidgetDetailsResonse response = new CardWidgetDetailsResonse();
            YappayEncryptedResponse responseEncry = new YappayEncryptedResponse();
            CardWidgetDetailsRequest cardWidgetDetailsRequest = new CardWidgetDetailsRequest();
            try
            {
                string encodedStr = Convert.ToBase64String(Encoding.UTF8.GetBytes(cardWidgetRequest.kitNo + cardWidgetRequest.entityId + cardWidgetRequest.dob));
                //string inputStr = Encoding.UTF8.GetString(Convert.FromBase64String(encodedStr));
                cardWidgetDetailsRequest.token = encodedStr;
                cardWidgetDetailsRequest.kitNo = cardWidgetRequest.kitNo;
                cardWidgetDetailsRequest.entityId = cardWidgetRequest.entityId;
                cardWidgetDetailsRequest.appGuid = CommonConstants.AppGuid;
                cardWidgetDetailsRequest.business = CommonConstants.Business;
                cardWidgetDetailsRequest.callbackUrl = CommonConstants.CallBackUrl;
                cardWidgetDetailsRequest.dob = cardWidgetRequest.dob;

                HttpClient http = new HttpClient();
                http.BaseAddress = new Uri(PrepaidConstants.PrepaidBaseAddressByEncry);
                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Add(CommonConstants.Tenant, CommonConstants.RefundCustomerTenantValue);
                http.DefaultRequestHeaders.Add(PrepaidConstants.Authorization, PrepaidConstants.PartnerTokenValue);
                http.DefaultRequestHeaders.TryAddWithoutValidation(PrepaidConstants.ContentType, PrepaidConstants.ApplicationJson);
                string requestData = JsonConvert.SerializeObject(cardWidgetDetailsRequest);

                objLog.WriteAppLog("SetPinWidget - Plain Request to Transcorp Encrypted data  :" + requestData, lstrFolderName);
                string uniqRanNumber = Utility.GetTransID().Substring(18 - 16);
                string requestEncryptedData = enc.encodeRequest(requestData, uniqRanNumber, CommonConstants.CustomerTenantValue);
                objLog.WriteAppLog("SetPinWidget - Request to Transcorp Request Encrypted Data  :" + requestEncryptedData, lstrFolderName);

                var stringContent = new StringContent(requestEncryptedData, Encoding.UTF8, CommonConstants.ApplicationJson);

                HttpResponseMessage responseMessage = http.PostAsync(CommonConstants.FetchSetPinWidgetUrl, stringContent).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultValue = responseMessage.Content.ReadAsStringAsync().Result;
                    responseEncry = JsonConvert.DeserializeObject<YappayEncryptedResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                    string requestEncryptedData1 = enc.decodeResponse(responseEncry);
                    objLog.WriteAppLog("SetPinWidget - Response From Transcorp  :" + requestEncryptedData1, lstrFolderName);
                    response = JsonConvert.DeserializeObject<CardWidgetDetailsResonse>(requestEncryptedData1);
                }
                else
                {
                    string respMesg = responseMessage.Content.ReadAsStringAsync().Result;
                    responseEncry = JsonConvert.DeserializeObject<YappayEncryptedResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                    string requestEncryptedData1 = enc.decodeResponse(responseEncry);
                    objLog.WriteAppLog("SetPinWidget - Response From Transcorp  :" + requestEncryptedData1, lstrFolderName);

                    response = JsonConvert.DeserializeObject<CardWidgetDetailsResonse>(requestEncryptedData1);

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
