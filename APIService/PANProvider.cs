using BusinessModel.GST_Validation;
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
    public class PANProvider
    {
        public PANProvider()
        {

        }
      

        public PanResponse VerifyLogin()
        {
           
            PanResponse res = new PanResponse();
     
             try
            {
             
                HttpClient http = new HttpClient();
             
                http.BaseAddress = new Uri(GstPanConstants.BaseAddress);
                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(GstPanConstants.ApplicationJson));


                var panrequest = new PanRequest()
                {
                    username = "accupaydtech_test",
                    password="hgw7c7M2TKH6hxqEUw2d"
                };
                var stringContent = new StringContent(JsonConvert.SerializeObject(panrequest), Encoding.UTF8, GstPanConstants.ApplicationJson);
                HttpResponseMessage responseMessage = http.PostAsync(GstPanConstants.LoginBaseAddress, stringContent).Result;

                if (responseMessage.IsSuccessStatusCode)
                {
                    var resultValue = responseMessage.Content.ReadAsStringAsync().Result;
                    res = JsonConvert.DeserializeObject<PanResponse>(responseMessage.Content.ReadAsStringAsync().Result);
                }
                return res;
            }
            catch (Exception ex)
            { 
                throw ex;
            }
         }

        public panRequestModel getpandata (PanRequest panRequestmodel)
        {
            panRequestModel panrequest = new panRequestModel()
            {
                essentials = new panReqData(),
                task="fetch"
            };
            if (panRequestmodel != null)
            {
                panrequest.essentials.number = panRequestmodel.panNo;
            }
            return panrequest;
        }

  
       public string getPanData(PanRequest panRequestmodel)
        {
            string resultValue = "";
            PanResponse res = new PanResponse();
            //PanResponse resfinal = new PanResponse();
           try
            {
                PanResponse newpan = new PanResponse();
                HttpClient http = new HttpClient();

               res= VerifyLogin();

                http.BaseAddress = new Uri(GstPanConstants.BaseAddress);
                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(GstPanConstants.ApplicationJson));
                http.DefaultRequestHeaders.Add("Authorization", res.id);

                http.DefaultRequestHeaders.TryAddWithoutValidation(GstPanConstants.ContentType, GstPanConstants.ContentTypeValue);
                http.DefaultRequestHeaders.TryAddWithoutValidation(GstPanConstants.Accept, GstPanConstants.AcceptValue);

                var aadhaarKYCRequestModel = getpandata(panRequestmodel);



                var stringContent1 = new StringContent(JsonConvert.SerializeObject(aadhaarKYCRequestModel), Encoding.UTF8, GstPanConstants.ApplicationJson);
                HttpResponseMessage responseMessage1 = http.PostAsync(res.userId + GstPanConstants.PANBaseAddress, stringContent1).Result;

                if (responseMessage1.IsSuccessStatusCode)//pause hereeeeeeeeee
                {
                   resultValue = responseMessage1.Content.ReadAsStringAsync().Result;
                   // resfinal = JsonConvert.DeserializeObject<PanResponse>(responseMessage1.Content.ReadAsStringAsync().Result);


                }

                return resultValue;


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public string getGSTData(PanRequest panRequestmodel)
        {

            PanResponse res = new PanResponse();
            PanResponse resfinal = new PanResponse();
            string resultValue = "";
            try
            {
                PanResponse newpan = new PanResponse();
                HttpClient http = new HttpClient();

                res = VerifyLogin();

                http.BaseAddress = new Uri(GstPanConstants.BaseAddress);
                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(GstPanConstants.ApplicationJson));
                http.DefaultRequestHeaders.Add("Authorization", res.id);

                http.DefaultRequestHeaders.TryAddWithoutValidation(GstPanConstants.ContentType, GstPanConstants.ContentTypeValue);
                http.DefaultRequestHeaders.TryAddWithoutValidation(GstPanConstants.Accept, GstPanConstants.AcceptValue);

                var aadhaarKYCRequestModel = getGSTdata(panRequestmodel);



                var stringContent1 = new StringContent(JsonConvert.SerializeObject(aadhaarKYCRequestModel), Encoding.UTF8, GstPanConstants.ApplicationJson);
                HttpResponseMessage responseMessage1 = http.PostAsync(res.userId + GstPanConstants.GSTBaseAddress, stringContent1).Result;

                if (responseMessage1.IsSuccessStatusCode)
                {
                   resultValue = responseMessage1.Content.ReadAsStringAsync().Result;
                   // resfinal = JsonConvert.DeserializeObject<PanResponse>(responseMessage1.Content.ReadAsStringAsync().Result);
                }

                return resultValue;
            }
            catch (Exception ex)
            {
                throw ex;
            }
          

        }
        public panRequestModel getGSTdata(PanRequest panRequestmodel)
        {
            panRequestModel panrequest = new panRequestModel()
            {
                essentials = new panReqData(),
                task = "gstinSearch"
            };



            if (panRequestmodel != null)
            {
                panrequest.essentials.gstin = panRequestmodel.gstNo;

            }
            return panrequest;
        }
    }
}
