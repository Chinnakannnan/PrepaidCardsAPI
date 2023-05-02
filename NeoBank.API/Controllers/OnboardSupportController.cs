
using BusinessModel.Common;
using BusinessModel.OnBoarding.OnboardSupport;
using DataAccess.Onboarding.Onboarding_Support;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using Microsoft.Extensions.Configuration;
using BusinessDomain.PAN;
using BusinessModel.MobileOTP;
using BusinessDomain.MobileOTP;
using BusinessDomain.Aadhaar;
using BusinessModel.Aadhaar;
using BusinessModel.GST_Validation;
using BusinessDomain.Onboarding.OnboardSupport;
using NeoBank.API.Utilities;
using Microsoft.AspNetCore.Authorization;
using static NeoBank.API.Startup;

namespace NeoBank.API.Controllers
{
  //  [Authorize(Roles = Roles.Admin)]
    [Route("api/OnboardSupport")]
    [ApiController]
    public class OnboardSupportController : ControllerBase
    {
        public const string lstrFolderName = "OnBoarding";
    
        private readonly IMobileOTPBusiness _MobileOtp;
        private readonly IAadhaarBusiness _Aadhaar;
        private readonly IPanBusiness _PanAndGst;

        private readonly IConfiguration _configuration;

        private readonly IOnbSupportBusiness _onboardBusiness;
       


        //public static class MyServer
        //{
        //    public static string MapPath(string path)
        //    {
        //        try
        //        {
        //            return Path.Combine(
        //          (string)AppDomain.CurrentDomain.GetData("ContentRootPath"),
        //          path);
        //        }
        //        catch (Exception ex)
        //        {
        //            return "no";
        //        }
             
        //    }
        //}
   



        public OnboardSupportController(IOnbSupportBusiness onboardBusinessInstance,IOnboardSupportDA onboardSupportInstance, IMobileOTPBusiness MobileotpInstance,
            IAadhaarBusiness AadhaarInstance, IPanBusiness PanInstance, IConfiguration Configuration)
        {
//_onboardSupport = onboardSupportInstance;
            _MobileOtp = MobileotpInstance;
            _Aadhaar = AadhaarInstance;
            _PanAndGst = PanInstance;
            _configuration = Configuration;
            _onboardBusiness = onboardBusinessInstance;
        }

        [HttpPost]
        [Route("VerifyUserdata")]
        public IActionResult VerifyUserdata(verifyUserData data)
        {
            try
            {
                string serial = System.Text.Json.JsonSerializer.Serialize(data);
                if (string.IsNullOrEmpty(serial))
                {
                    var hello = "Data is null";
                    return Ok(hello);
                }
               var result = _onboardBusiness.VerifyUserdata(data);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        [Route("CreateShop")]
        public IActionResult CreateShop(OnboardSupportModel data)
        {
            try
            {
                var result = _onboardBusiness.CreateShop(data);


                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("GotoRef")]
        public IActionResult GotoRef(detailsOnb data)
        {
            string lstrRequest = string.Empty;
            try
            {

                   //lstrRequest = System.Text.Json.JsonSerializer.Serialize(data);
                if (data== null)
                {
                    var hello = "Data is null";
                    return Ok(hello);
                }
                 var result = _onboardBusiness.GotoRef(data);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        } 
        
        [HttpPost]
        [Route("MobileOtp")]
        public IActionResult MobileOtp(MobileOTPModel data)
        {
            try
            {
                if (data == null)
                {
                    var hello = "Data is null";
                    return Ok(hello);
                }
                var result = _MobileOtp.SendOTPtoMobile(data);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("AadhaarOTP")]
        public IActionResult AadhaarOTP(AadhaarOTPRequest data)
        {
            try
            {
                if (data == null)
                {
                    var hello = "Data is null";
                    return Ok(hello);
                }
                var result = _Aadhaar.GenerateOTP(data);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("AadharDetails")]
        public IActionResult AadharDetails(AadhaarKYCRequest data)
        {
            try
            {
                if (data == null)
                {
                    var hello = "Data is null";
                    return Ok(hello);
                }

                var result = _Aadhaar.KYCWithOTP(data);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }
        
        [HttpPost]
        [Route("PanValidation")]
        public IActionResult PanValidation(PanRequest data)
        {
            try
            {
                if (data == null)
                {
                    var hello = "Data is null";
                    return Ok(hello);
                }
                var result = _PanAndGst.getPanData(data);
                //if result !=""
                detailsOnb Response = new detailsOnb();
                //here you have to encrypt the details of the fetched real data
                Response.panDetails = result;
                Response.pancardNo = data.panNo;
                Response.refId = data.refId;
                _onboardBusiness.SavePANDetails(Response);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }
   
        [HttpPost]
        [Route("GstValidation")]
        public IActionResult GstValidation(PanRequest data)
        {
            try
            {
                if (data == null)
                {
                    var hello = "Data is null";
                    return Ok(hello);
                }
                var result = _PanAndGst.getGSTData(data);
                detailsOnb Response = new detailsOnb();
                //here you have to encrypt the details of the fetched real data
                Response.gstDetails = result;
                Response.gstNo = data.panNo;
                Response.refId = data.refId;
                _onboardBusiness.SaveGSTDetails(Response);
             
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("SendingMail")]
        public IActionResult SendingMail(string MailID, string OTP)
        {
            try
            {
                if (MailID == null || OTP == null)
                {
                    var hello = "Data is null";
                    return Ok(hello);
                }

                var path ="Template/MailOTP.html";
                string imgUrl = _configuration.GetSection("appSettings")["IMAGEURLPATH"];
                string fromAddr = _configuration.GetSection("appSettings")["FROMMAILADDRESS"];
                string smtpAddr = _configuration.GetSection("appSettings")["SMTPADDRESS"];
                string smtpPort = _configuration.GetSection("appSettings")["SMTPPORT"];
                string mailSecret = _configuration.GetSection("appSettings")["FROMMAILSECRET"];
                //encrypt emailid and otp here..
            
              string EmailID = Crypto.AES_ENCRYPT(MailID, COMMON.EMAILKEY);
              string OTP1 = Crypto.AES_ENCRYPT(OTP, COMMON.EMAILKEY);

                MailServices.SendOTP(EmailID, OTP1, imgUrl, fromAddr, smtpAddr, smtpPort, mailSecret, path);

                var result = "";
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("GetUserDetails")]
        public IActionResult GetUserDetails(getDetailRequest data)
        {
            try
            {
                if (data == null)
                {
                    var hello = "Data is null";
                    return Ok(hello);
                }
                var result = _onboardBusiness.GetUserDetails(data);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        [Route("InsertUserPlan")]
        public IActionResult InsertUserPlan(mapuserplan data)
        {
            try
            {
                if (data == null)
                {
                    var hello = "Data is null";
                    return Ok(hello);
                }
                var result = _onboardBusiness.InsertUserPlan(data);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        [Route("Selectuserplan")]
        public IActionResult Selectuserplan(createusermap data)
        {
            try
            {
                if (data == null)
                {
                    var hello = "Data is null";
                    return Ok(hello);
                }
                var result = _onboardBusiness.Selectuserplan(data);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        [Route("CreateUserMapping")]
        public IActionResult CreateUserMapping(createusermap data)
        {
            try
            {
                if (data == null)
                {
                    var hello = "Data is null";
                    return Ok(hello);
                }
                var result = _onboardBusiness.CreateUserMapping(data);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        [Route("ListDist")]
        public IActionResult ListDist(createusermap data)
        {
            try
            {
                if (data == null)
                {
                    var hello = "Data is null";
                    return Ok(hello);
                }
                var result = _onboardBusiness.ListDist(data);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        [Route("SuperDisgrp")]
        public IActionResult SuperDisgrp(createusermap data)
        {
            try
            {
                if (data == null)
                {
                    var hello = "Data is null";
                    return Ok(hello);
                }
                var result = _onboardBusiness.SuperDisgrp(data);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }
     
        [HttpPost]
        [Route("GET_GROUP1")]
        public IActionResult GET_GROUP1(plan data)
        {
            try
            {
                if (data == null)
                {
                    var hello = "Data is null";
                    return Ok(hello);
                }
                var result = _onboardBusiness.GET_GROUP1(data);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("adminstatus")]
        public IActionResult adminstatus(OnboardSupportModel data)
        {
            try
            {
                if (data == null)
                {
                    var hello = "Data is null";
                    return Ok(hello);
                }
                string imgUrl = _configuration.GetSection("appSettings")["IMAGEURLPATH"];
                string fromAddr = _configuration.GetSection("appSettings")["FROMMAILADDRESS"];
                string smtpAddr = _configuration.GetSection("appSettings")["SMTPADDRESS"];
                string smtpPort = _configuration.GetSection("appSettings")["SMTPPORT"];
                string mailSecret = _configuration.GetSection("appSettings")["FROMMAILSECRET"];
                data.vaccountid = _configuration.GetSection("appSettings")["VIRTUALACCOUNTCODE"] + data.mobileNo;
                data.vifsccode = _configuration.GetSection("appSettings")["VIRTUALACCOUNTIFSCCODE"];
                data.vbankname = _configuration.GetSection("appSettings")["VIRTUALACCOUNTBANK"];
                var result = _onboardBusiness.adminstatus(data);
                
                string lstremail = Crypto.AES_ENCRYPT(data.emailId, COMMON.EMAILKEY);
                MailServices.AdminApproval(lstremail, data.password, data.tpin, data.vaccountid, data.vifsccode, data.vbankname, imgUrl, fromAddr, smtpAddr, smtpPort, mailSecret);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        [Route("OnBoardget")]
        public IActionResult OnBoardget(OnboardSupportModel data)
        {
            try
            {
                if (data == null)
                {
                    var hello = "Data is null";
                    return Ok(hello);
                }


                var result = _onboardBusiness.OnBoardget(data);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        [Route("UpdUserRoleMap")]
        public IActionResult UpdUserRoleMap(UserRoleRes data)
        {
            try
            {
                if (data == null)
                {
                    var hello = "Data is null";
                    return Ok(hello);
                }
                var result = _onboardBusiness.UpdUserRoleMap(data);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        [Route("UpdRoleMap")]
        public IActionResult  UpdRoleMap(UserRoleRes data)
        {
            try
            {
                if (data == null)
                {
                    var hello = "Data is null";
                    return Ok(hello);
                }
                var result = _onboardBusiness.UpdRoleMap(data);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        [Route("UserRoleMap")]
        public IActionResult UserRoleMap(UserRoleRes data)
        {
            try
            {
                if (data == null)
                {
                    var hello = "Data is null";
                    return Ok(hello);
                }
                var result = _onboardBusiness.UserRoleMap(data);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }
        //     [HttpPost]
        //[Route("")]
        //public IActionResult(data)
        //{
        //    try
        //    {
        //        var result = _onboardBusiness.(data);

        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
        //    }
        //}












        //[HttpPost]
        //[Route("MailServices")]
        //public IActionResult MailServices(verifyUserData data)
        //{
        //    try
        //    {
        //        var result = _onboardSupport.VerifyUserdata(data);

        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
        //    }
        //}




    }

}



////lstrData = JsonSerializer.Serialize(requestBody);
////      objResponse =JsonSerializer.Deserialize<JsonRes>(lstrReturn);

















