using BusinessModel.Common;
using BusinessModel.Prepaid;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using BusinessModel.Card;
using BusinessModel.Common;
using System.IO;
using System.Net;
using System.Configuration;


namespace APIService
{
    public class PPICard
    {

        public const string lstrFolderName = "PPICard";
        Log objLog = new Log();
        M2BEncrypt enc = new M2BEncrypt();
        public PPICard()
        {

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
                HttpResponseMessage responseMessage = http.PostAsync(CommonConstants.GetCardDetailsRequestURL, stringContent).Result;

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

    }
}
