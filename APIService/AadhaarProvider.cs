using BusinessModel.Aadhaar;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace APIService
{
    public class AadhaarProvider
    {
        public AadhaarProvider()
        {

        }
        public AadhaarOTPResponse GenerateOTP(AadhaarOTPRequest aadhaarOTPRequest)
        {
            AadhaarOTPResponse response = new AadhaarOTPResponse();
            try
            {
                HttpClient http = new HttpClient();
                //string url = "in/identity/okyc/otp/request";
                http.BaseAddress = new Uri(AadhaarConstants.AadhaarBaseAddress);
                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AadhaarConstants.ApplicationJson));
                http.DefaultRequestHeaders.Add(AadhaarConstants.APIKey, AadhaarConstants.APIKeyValue);
                http.DefaultRequestHeaders.Add(AadhaarConstants.AppId, AadhaarConstants.AppIdValue);
                http.DefaultRequestHeaders.TryAddWithoutValidation(AadhaarConstants.ContentType, AadhaarConstants.ContentTypeValue);
                http.DefaultRequestHeaders.TryAddWithoutValidation(AadhaarConstants.Accept, AadhaarConstants.AcceptValue);

                var aadhaarOTPRequestModel = ConvertToOTPRequestModel(aadhaarOTPRequest);

                var stringContent = new StringContent(JsonConvert.SerializeObject(aadhaarOTPRequestModel), Encoding.UTF8, AadhaarConstants.ApplicationJson);
                HttpResponseMessage responseMessage = http.PostAsync(AadhaarConstants.OTPRequestURL, stringContent).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultValue = responseMessage.Content.ReadAsStringAsync().Result;
                    //response = JsonConvert.DeserializeObject<AadhaarOTPResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                    response = ConvertToOTPResponseModel(resultValue);
                }

                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public AadhaarOTPResponse ConvertToOTPResponseModel(string resultValue)
        {
            AadhaarOTPResponse aadhaarOTPResponse = new AadhaarOTPResponse();
            if (!string.IsNullOrEmpty(resultValue))
            {
                var convertedResult = JsonConvert.DeserializeObject<AadhaarOTPResponseModel>(resultValue);
                if (convertedResult.success == true) //&& string.IsNullOrEmpty(convertedResult.errdesc)
                {
                    aadhaarOTPResponse.RequestId = !string.IsNullOrEmpty(convertedResult.request_id) ? convertedResult.request_id : string.Empty;
                    aadhaarOTPResponse.Success = convertedResult.success;
                    //aadhaarOTPResponse.ResponseMessage = convertedResult.response_message;
                }
                else
                {
                    aadhaarOTPResponse.RequestId = !string.IsNullOrEmpty(convertedResult.request_id) ? convertedResult.request_id : string.Empty;
                    aadhaarOTPResponse.Success = convertedResult.success;
                    aadhaarOTPResponse.ResponseMessage = convertedResult.response_message;
                }

            }
            return aadhaarOTPResponse;
        }

        public AadhaarOTPRequestModel ConvertToOTPRequestModel(AadhaarOTPRequest aadhaarOTPRequest)
        {
            AadhaarOTPRequestModel aadhaarOTPRequestModel = new AadhaarOTPRequestModel()
            {
                data = new AadhaarOTPData()
            };
            if (aadhaarOTPRequest != null)
            {
                aadhaarOTPRequestModel.data.customer_aadhaar_number = aadhaarOTPRequest.AadhaarNo;
                aadhaarOTPRequestModel.data.consent = AadhaarConstants.ConsentValue;
                aadhaarOTPRequestModel.data.consent_text = AadhaarConstants.ConsentTextValue;
            }
            return aadhaarOTPRequestModel;
        }

        public AadhaarKYCResponse KYCWithOTP(AadhaarKYCRequest aadhaarKYCRequest)
        {
            AadhaarKYCResponse response = new AadhaarKYCResponse();
            try
            {
                HttpClient http = new HttpClient();
                //string url = "in/identity/okyc/otp/verify";
                http.BaseAddress = new Uri(AadhaarConstants.AadhaarBaseAddress);
                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(AadhaarConstants.ApplicationJson));
                http.DefaultRequestHeaders.Add(AadhaarConstants.APIKey, AadhaarConstants.APIKeyValue);
                http.DefaultRequestHeaders.Add(AadhaarConstants.AppId, AadhaarConstants.AppIdValue);
                http.DefaultRequestHeaders.TryAddWithoutValidation(AadhaarConstants.ContentType, AadhaarConstants.ContentTypeValue);
                http.DefaultRequestHeaders.TryAddWithoutValidation(AadhaarConstants.Accept, AadhaarConstants.AcceptValue);

                var aadhaarKYCRequestModel = ConvertToKYCRequestModel(aadhaarKYCRequest);


                var stringContent = new StringContent(JsonConvert.SerializeObject(aadhaarKYCRequestModel), Encoding.UTF8, AadhaarConstants.ApplicationJson);
                HttpResponseMessage responseMessage = http.PostAsync(AadhaarConstants.KYCRequestURL, stringContent).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultValue = responseMessage.Content.ReadAsStringAsync().Result;
                    //response = JsonConvert.DeserializeObject<AadhaarKYCResponseModel>(responseMessage.Content.ReadAsStringAsync().Result);
                    response = ConvertToKYCResponseModel(resultValue);

                }

                return response;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public AadhaarKYCRequestModel ConvertToKYCRequestModel(AadhaarKYCRequest aadhaarKYCRequest)
        {
            AadhaarKYCRequestModel aadhaarKYCRequestModel = new AadhaarKYCRequestModel()
            {
                data = new AadhaarKYCData()
            }; 
            if (aadhaarKYCRequest != null)
            {
                aadhaarKYCRequestModel.data.request_id = aadhaarKYCRequest.RequestId;
                aadhaarKYCRequestModel.data.otp = aadhaarKYCRequest.OTP;
                aadhaarKYCRequestModel.data.consent = AadhaarConstants.ConsentValue;
                aadhaarKYCRequestModel.data.consent_text = AadhaarConstants.ConsentTextValue;
            }
            return aadhaarKYCRequestModel;
        }

        public AadhaarKYCResponse ConvertToKYCResponseModel(string resultValue)
        {
            AadhaarKYCResponse aadhaarKYCResponse = new AadhaarKYCResponse();
            if (!string.IsNullOrEmpty(resultValue))
            {
                var convertedResult = JsonConvert.DeserializeObject<AadhaarKYCResponseModel>(resultValue);
                if ((convertedResult.success == true) && (convertedResult.result != null))
                {

                    aadhaarKYCResponse.Name = !string.IsNullOrEmpty(convertedResult.result.user_full_name) ? convertedResult.result.user_full_name : string.Empty;
                    aadhaarKYCResponse.Success = convertedResult.success;
                    string[] addressElements =
               {
                        convertedResult.result.user_address.house,
                        convertedResult.result.user_address.street,
                        convertedResult.result.user_address.subdist,
                        convertedResult.result.user_address.vtc,
                        convertedResult.result.user_address.loc,
                        convertedResult.result.user_address.po,
                        convertedResult.result.user_address.state,
                        convertedResult.result.user_address.dist,
                        convertedResult.result.user_address.country
                    };
                    string primaryAddress = string.Join(",", addressElements.Where(s => !string.IsNullOrEmpty(s)));
                    aadhaarKYCResponse.PermanentAddress = primaryAddress;
                    //aadhaarOTPResponse.ResponseMessage = convertedResult.response_message;
                }
                else
                {
                    //aadhaarKYCResponse.RequestId = !string.IsNullOrEmpty(convertedResult.request_id) ? convertedResult.request_id : string.Empty;
                    aadhaarKYCResponse.Success = convertedResult.success;
                    aadhaarKYCResponse.ResponseMessage = convertedResult.response_message;
                    aadhaarKYCResponse.ReasonMessage = convertedResult.metadata.reason_message;
                }

            }
            return aadhaarKYCResponse;
        }
    }
}
