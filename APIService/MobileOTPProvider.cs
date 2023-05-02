using BusinessModel.MobileOTP;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static BusinessModel.MobileOTP.MobileOTPModel;

namespace APIService
{
    public class MobileOTPProvider
    {

        public MobileOTPProvider()
        {

        }

        public string SendOTPToMobile(MobileOTPModel mobileData)
        {
            string resultValue = "";
      
            try
            {
              
                HttpClient http = new HttpClient();

                

                http.BaseAddress = new Uri(MOBILEOTP.BaseURL);
                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MOBILEOTP.ApplicationJson));
                http.DefaultRequestHeaders.TryAddWithoutValidation(MOBILEOTP.ContentType, MOBILEOTP.ContentTypeValue);
                http.DefaultRequestHeaders.TryAddWithoutValidation(MOBILEOTP.Accept, MOBILEOTP.AcceptValue);

                // var aadhaarKYCRequestModel = getpandata(panRequestmodel);



                var stringContent1 = new StringContent(JsonConvert.SerializeObject(mobileData.mobileNo), Encoding.UTF8, MOBILEOTP.ApplicationJson);
                HttpResponseMessage responseMessage1 = http.PostAsync(MOBILEOTP.UserDetail + mobileData.mobileNo + MOBILEOTP.TEMPLATE1+MOBILEOTP.TEMPLATE2+" "+ mobileData.mobileOTP+ " " + MOBILEOTP.TEMPLATE3, stringContent1).Result;

                if (responseMessage1.IsSuccessStatusCode)//pause hereeeeeeeeee
                {
                    resultValue = responseMessage1.Content.ReadAsStringAsync().Result;
                }

                return resultValue;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




















    }
}
