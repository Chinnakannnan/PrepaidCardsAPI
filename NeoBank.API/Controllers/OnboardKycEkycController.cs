using BusinessDomain.Aadhaar;
using BusinessDomain.MobileOTP;
using BusinessDomain.Onboarding.OnboardKycEkyc;
using BusinessDomain.PAN;
using BusinessModel.Aadhaar;
using BusinessModel.Common;
using BusinessModel.GST_Validation;
using BusinessModel.MobileOTP;
using BusinessModel.OnBoarding.OnboardKycEkyc;
using DataAccess.Onboarding.Onboarding_KycEkyc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NeoBank.API.Utilities;
using System.Net;
using System;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using static NeoBank.API.Startup;

namespace NeoBank.API.Controllers
{
  //  [Authorize(Roles = Roles.Admin)]
    [Route("api/OnboardingKycEkyc")]
    [ApiController]
    public class OnboardingKycEkycController : ControllerBase
    {

        public const string lstrFolderName = "OnbKycEkyc";
        private readonly IOnboardKycEkycDA _onboardKycEkyc;
        private readonly IMobileOTPBusiness _MobileOtp;
        private readonly IAadhaarBusiness _Aadhaar;
        private readonly IPanBusiness _PanAndGst;
        private readonly IConfiguration _configuration;
        private readonly IOnbKycEkycBusiness _onboardKycEkycBusiness;
        public OnboardingKycEkycController(IOnbKycEkycBusiness onboardBusinessInstance, IOnboardKycEkycDA onboardKycEkycInstance, IConfiguration Configuration, IPanBusiness PanInstance, IAadhaarBusiness AadhaarInstance, IMobileOTPBusiness MobileotpInstance)
        {
            _onboardKycEkycBusiness = onboardBusinessInstance;
            _onboardKycEkyc = onboardKycEkycInstance;
            _MobileOtp = MobileotpInstance;
            _Aadhaar = AadhaarInstance;
            _PanAndGst = PanInstance;
            _configuration = Configuration;
        }
        [HttpPost]
        [Route("UserRegistration")]
        public IActionResult UserRegistration(RegistrationRequest data)
        {
            try
            {
                if (data == null)
                {
                    var hello = "Data is null";
                    return BadRequest(hello);
                }
                var result = _onboardKycEkycBusiness.UserRegistration(data);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }
        [HttpGet]
        [Route("GetUser")]
        public IActionResult GetUser()
        {
            try
            {

                var result = _onboardKycEkycBusiness.GetUser();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        [Route("UpdateStatus")]
        public IActionResult UpdateStatus(RegistrationRequest data)
        {
            try
            {

                var result = _onboardKycEkycBusiness.UpdateStatus(data);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("SendLink")]
        public IActionResult SendLink(sendLink data)
        {
            //sendLink objRequest = new sendLink();

            try
            {
                if (data == null)
                {
                    var hello = "Data is null";
                    return Ok(hello);
                }
                var result = _onboardKycEkycBusiness.SendLink(data);

                // objRequest = JsonSerializer.Deserialize<sendLink>(data);
                //  MailService.SendLink(lstremail, lstrLink, data.uniqueId);

                string imgUrl = _configuration.GetSection("appSettings")["IMAGEURLPATH"];
                string fromAddr = _configuration.GetSection("appSettings")["FROMMAILADDRESS"];
                string smtpAddr = _configuration.GetSection("appSettings")["SMTPADDRESS"];
                string smtpPort = _configuration.GetSection("appSettings")["SMTPPORT"];
                string mailSecret = _configuration.GetSection("appSettings")["FROMMAILSECRET"];
                //encrypt emailid and otp here..
                string EmailId = Crypto.AES_ENCRYPT(data.emailId, COMMON.EMAILKEY);
                string Link = Crypto.AES_ENCRYPT("http://localhost:51181/ONBkyc/KycEkyc", COMMON.EMAILKEY);

                // MailServices.SendLink(EmailId, Link, data.uniqueId, imgUrl, fromAddr, smtpAddr, smtpPort, mailSecret);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        [Route("VerifyUserdataEkyc")]
        public IActionResult VerifyUserdataEkyc(CheckUserData data)
        {
            try
            {
                if (data == null)
                {
                    var hello = "Data is null";
                    return BadRequest(hello);
                }
                var result = _onboardKycEkycBusiness.VerifyUserdataEkyc(data);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        [Route("CreateShop")]
        public IActionResult CreateShop(OnboardKycEkycModel data)
        {
            try
            {
                if (data == null)
                {
                    var hello = "Data is null";
                    return Ok(hello);
                }
                var result = _onboardKycEkycBusiness.CreateShopKycEkyc(data);


                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        [Route("GotoRef")]
        public IActionResult GotoRef(detailsOnboard data)
        {
            try
            {
                if (data == null)
                {
                    var hello = "Data is null";
                    return Ok(hello);
                }
                var result = _onboardKycEkycBusiness.GotoRefKycEkyc(data);

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
        public IActionResult AadharDetails(OnboardAadhaarKYCRequest data)
        {//OnboardAadhaarKYCRequest
            try
            {
                if (data == null)
                {
                    var hello = "Data is null";
                    return Ok(hello);
                }
                //var result = _Aadhaar.OnboardingKYCWithOTP(data);
                var result = "ghfghfghfghgfhg";
                //here we have to send mail to the user about the ref id;
                string refId = "Ref_" + Utility.GetStan();
                string lstremail = Crypto.AES_ENCRYPT(data.EmailId, COMMON.EMAILKEY);
                string lstrref = Crypto.AES_ENCRYPT(refId, COMMON.EMAILKEY);
                if (result != null)
                {
                    string imgUrl = _configuration.GetSection("appSettings")["IMAGEURLPATH"];
                    string fromAddr = _configuration.GetSection("appSettings")["FROMMAILADDRESS"];
                    string smtpAddr = _configuration.GetSection("appSettings")["SMTPADDRESS"];
                    string smtpPort = _configuration.GetSection("appSettings")["SMTPPORT"];
                    string mailSecret = _configuration.GetSection("appSettings")["FROMMAILSECRET"];
                    //encrypt emailid and otp here..
                    MailServices.SendRefId(lstremail, lstrref, imgUrl, fromAddr, smtpAddr, smtpPort, mailSecret);



                    detailsOnboard Response = new detailsOnboard();
                    //here you have to encrypt the details of the fetched real data
                    string aadharResp = JsonSerializer.Serialize(result);
                    string AadhaarEncrypted = Crypto.AES_ENCRYPT(aadharResp, COMMON.EMAILKEY);

                    Response.aadhaarDetails = AadhaarEncrypted;
                    Response.aadhaarNo = data.AadhaarNo;
                    Response.refId = refId;
                    // // Response.refId = refId;
                    var result1 = _onboardKycEkycBusiness.SaveAadhaarData(Response);



                }



                //SqlEnclaveSession["RefId"] = num;

                //MailService.SendRefId(lstremail, lstrref);

                //  var path = "Template/MailOTP.html";











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
                detailsOnboard Response = new detailsOnboard();
                //here you have to encrypt the details of the fetched real data
                Response.panDetails = result;
                Response.pancardNo = data.panNo;
                Response.refId = data.refId;
                _onboardKycEkycBusiness.SaveGSTDetailsKycEkyc(Response);

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
                detailsOnboard Response = new detailsOnboard();
                //here you have to encrypt the details of the fetched real data
                Response.gstDetails = result;
                Response.gstNo = data.panNo;
                Response.refId = data.refId;
                _onboardKycEkycBusiness.SaveGSTDetailsKycEkyc(Response);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        [Route("CheckUniqueId")]
        public IActionResult CheckUniqueId(sendLink data)
        {
            try
            {
                if (data == null)
                {
                    var hello = "Data is null";
                    return Ok(hello);
                }
                var result = _onboardKycEkycBusiness.CheckUniqueId(data);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        [Route("RegisterKYCdata")]

        public IActionResult RegisterKYCdata(OnboardKycEkycModel data)
        {
            try
            {
                if (data == null)
                {
                    var hello = "Data is null";
                    return Ok(hello);
                }
                var result = _onboardKycEkycBusiness.RegisterKYCdata(data);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        [Route("ApprovedData")]
        public IActionResult ApprovedData(sendLink data)
        {
            try
            {
                if (data == null)
                {
                    var hello = "Data is null";
                    return BadRequest(hello);
                }
                var result = _onboardKycEkycBusiness.ApprovedData(data);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        [Route("RejectedData")]
        public IActionResult RejectedData(sendLink data)
        {
            try
            {
                if (data == null)
                {
                    var hello = "Data is null";
                    return BadRequest(hello);
                }
                var result = _onboardKycEkycBusiness.RejectedData(data);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        [Route("WaitingForApproval")]
        public IActionResult WaitingForApproval(sendLink data)
        {
            try
            {
                if (data == null)
                {
                    var hello = "Data is null";
                    return BadRequest(hello);
                }
                var result = _onboardKycEkycBusiness.WaitingForApproval(data);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }
    }
}
