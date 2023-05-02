using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessModel.Login;
using BusinessDomain.Queries.Login;
using BusinessDomain.Login;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using BusinessModel.Common;
using BusinessDomain.MobileOTP;
using BusinessModel.MobileOTP;
using BusinessDomain.Auth;
using Newtonsoft.Json.Linq;
using NeoBank.API.Utilities;
using Microsoft.Extensions.Configuration;
using DocumentFormat.OpenXml.Wordprocessing;
using BusinessModel.User;

namespace NeoBank.API.Controllers
{
    [Authorize]
    [Route("api/v1")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly ILoginBusiness _loginBusiness;
        private readonly IConfiguration _configuration;
        StatusResponseModel statusModal = new StatusResponseModel();
        string strResponse = string.Empty;
        private readonly IMobileOTPBusiness _MobileOtp;

        public LoginController(ILoginBusiness loginBusinessInstance, IMobileOTPBusiness MobileotpInstance, IConfiguration configuration) =>
         (_loginBusiness, _MobileOtp, _configuration) = (loginBusinessInstance, MobileotpInstance, configuration);




        [AllowAnonymous]
        [HttpPost]
        [Route("LoginValidate")]
        public IActionResult Loginvalidate(LoginModel loginRequest)
        {
            try
            {
                string clientID = string.Empty;
                string clientSecret = string.Empty;
                if (Request.Headers.TryGetValue("clientId", out var clientid))
                {
                    clientID = clientid;
                }
                if (Request.Headers.TryGetValue("clientSecret", out var clientsecret))
                {
                    clientSecret = clientsecret;
                }
                if (string.IsNullOrEmpty(clientSecret) && string.IsNullOrEmpty(clientID))
                {
                    statusModal.statuscode = ResponseCode.Invalid_ClinetID_Secrect;
                    statusModal.statusdesc = ResponseMsg.Invalid_ClinetID_Secrect;
                    strResponse = JsonConvert.SerializeObject(statusModal);
                    return StatusCode((int)HttpStatusCode.BadRequest, strResponse);
                }
                if (loginRequest == null)
                {
                    statusModal.statuscode = ResponseCode.Request_Empty;
                    statusModal.statusdesc = ResponseMsg.Request_Empty;
                    strResponse = JsonConvert.SerializeObject(statusModal);
                    return StatusCode((int)HttpStatusCode.BadRequest, strResponse);
                }
                if (string.IsNullOrEmpty(loginRequest.UserName) && string.IsNullOrEmpty(loginRequest.Password))
                {
                    statusModal.statuscode = ResponseCode.Request_Empty;
                    statusModal.statusdesc = ResponseMsg.Request_Empty;
                    strResponse = JsonConvert.SerializeObject(statusModal);
                    return StatusCode((int)HttpStatusCode.BadRequest, strResponse);
                }
                LoginValidateModel result = _loginBusiness.GetLoginData(loginRequest, clientID, clientSecret);
               
                if (result == null)
                {
                    statusModal.statuscode = ResponseCode.Plese_Try_Again_TableNull;
                    statusModal.statusdesc = ResponseMsg.Plese_Try_Again_TableNull;
                    strResponse = JsonConvert.SerializeObject(statusModal);
                    return StatusCode((int)HttpStatusCode.BadRequest, strResponse);
                }
                if (string.IsNullOrEmpty(result.Token))
                {
                    return Unauthorized();
                }
                if (result.StatusCode == "000")
                {
                   
                    return Ok(result);
                }
                else
                {
                    statusModal.statuscode = result.StatusCode;
                    statusModal.statusdesc = result.StatusDesc;
                    strResponse = JsonConvert.SerializeObject(statusModal);
                    return StatusCode((int)HttpStatusCode.OK, strResponse);
                }
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginModel loginRequest)
        {
            try
            {
                string clientID = string.Empty;
                string clientSecret = string.Empty;
                if (Request.Headers.TryGetValue("clientId", out var clientid))
                {
                    clientID = clientid;
                }
                if (Request.Headers.TryGetValue("clientSecret", out var clientsecret))
                {
                    clientSecret = clientsecret;
                }
                if (string.IsNullOrEmpty(clientSecret) && string.IsNullOrEmpty(clientID))
                {
                    statusModal.statuscode = ResponseCode.Invalid_ClinetID_Secrect;
                    statusModal.statusdesc = ResponseMsg.Invalid_ClinetID_Secrect;
                    strResponse = JsonConvert.SerializeObject(statusModal);
                    return StatusCode((int)HttpStatusCode.BadRequest, strResponse);
                }
                if (loginRequest == null)
                {
                    statusModal.statuscode = ResponseCode.Request_Empty;
                    statusModal.statusdesc = ResponseMsg.Request_Empty;
                    strResponse = JsonConvert.SerializeObject(statusModal);
                    return StatusCode((int)HttpStatusCode.BadRequest, strResponse);
                }
                if (string.IsNullOrEmpty(loginRequest.UserName) && string.IsNullOrEmpty(loginRequest.Password))
                {
                    statusModal.statuscode = ResponseCode.Request_Empty;
                    statusModal.statusdesc = ResponseMsg.Request_Empty;
                    strResponse = JsonConvert.SerializeObject(statusModal);
                    return StatusCode((int)HttpStatusCode.BadRequest, strResponse);
                }
                LoginValidateModel result = _loginBusiness.GetLoginData(loginRequest, clientID, clientSecret);

                if (result == null)
                {
                    statusModal.statuscode = ResponseCode.Plese_Try_Again_TableNull;
                    statusModal.statusdesc = ResponseMsg.Plese_Try_Again_TableNull;
                    strResponse = JsonConvert.SerializeObject(statusModal);
                    return StatusCode((int)HttpStatusCode.BadRequest, strResponse);
                }
               /* if (string.IsNullOrEmpty(result.Token))
                {
                    return Unauthorized();
                }*/
                if (result.StatusCode == "000")
                {
                    return Ok(result);
                }
                else
                {
                    statusModal.statuscode = result.StatusCode;
                    statusModal.statusdesc = result.StatusDesc;
                    strResponse = JsonConvert.SerializeObject(statusModal);
                    return StatusCode((int)HttpStatusCode.OK, strResponse);
                }
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("BlockUnauthorized")]
        public IActionResult BlockUnauthorized(LoginModel loginRequest)
        {
            try
            {
                StatusResponseModel result = _loginBusiness.BlockUnauthorized(loginRequest);
                return Ok(result);                
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        [Authorize]
        [HttpPost]
        [Route("GetUserInfo")]
        public IActionResult GetUserInfo(LoginModel loginRequest)
        {
            try
            {
                if (loginRequest == null)
                {
                    statusModal.statuscode = ResponseCode.Request_Empty;
                    statusModal.statusdesc = ResponseMsg.Request_Empty;
                    strResponse = JsonConvert.SerializeObject(statusModal);
                    return StatusCode((int)HttpStatusCode.BadRequest, strResponse);
                }
                if (string.IsNullOrEmpty(loginRequest.UserName))
                {
                    statusModal.statuscode = ResponseCode.Request_Empty;
                    statusModal.statusdesc = ResponseMsg.Request_Empty;
                    strResponse = JsonConvert.SerializeObject(statusModal);
                    return StatusCode((int)HttpStatusCode.BadRequest, strResponse);
                }
                LoginResponseModel result = _loginBusiness.GetUserInfo(loginRequest);

                if (result == null)
                {
                    statusModal.statuscode = ResponseCode.Plese_Try_Again_TableNull;
                    statusModal.statusdesc = ResponseMsg.Plese_Try_Again_TableNull;
                    strResponse = JsonConvert.SerializeObject(statusModal);
                    return StatusCode((int)HttpStatusCode.BadRequest, strResponse);
                }
                
                if (result.StatusCode == "000")
                {
                    result.emailAddress = Crypto.AES_DECRYPT(result.emailAddress, COMMON.EMAILKEY);
                    return Ok(result);
                }
                else
                {
                    statusModal.statuscode = result.StatusCode;
                    statusModal.statusdesc = result.StatusDesc;
                    strResponse = JsonConvert.SerializeObject(statusModal);
                    return StatusCode((int)HttpStatusCode.OK, strResponse);
                }
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }


        [AllowAnonymous]
        [Route("LoginOtp")]
        [HttpPost]
        public IActionResult LoginOtp(MobileOTPModel data)
        {
            var fromAddr = _configuration.GetValue<string>("ApplicationSettings:FROMMAILADDRESS");
            var smtpAddr = _configuration.GetValue<string>("ApplicationSettings:SMTPADDRESS");
            var smtpPort = _configuration.GetValue<string>("ApplicationSettings:SMTPPORT");
            var mailSecret = _configuration.GetValue<string>("ApplicationSettings:FROMMAILSECRET");         
            var lstrImageURL = _configuration.GetValue<string>("ApplicationSettings:IMAGEURLPATH");
            try
            {
                if (data == null)
                {
                    var hello = "Data is null";
                    return Ok(hello);
                }
                var result = _MobileOtp.SendOTPtoMobile(data);
                MailServices.LoginOTP(data.mobileOTP, data.emailAddress, fromAddr, smtpAddr, smtpPort, mailSecret, lstrImageURL);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }
        [Route("getuser")]
        [HttpPost]
        public IActionResult getuser([FromBody] LoginModel login)
        {
            var tt = login;
            var context = HttpContext;
            string userId = context.Request.Headers["UserID"];
            return View();
        }

        [Route("user")]
        [HttpPost]
        public IActionResult user([FromBody] LoginModel login)
        {
            var tt = login;
            var context = HttpContext;
            string userId = context.Request.Headers["UserID"];
            return View();
        }

        //[Route("GetAllUsersList")]
        //[HttpGet]
        //public async Task<IActionResult> GetAllUsersList()
        //{
        //    var usersList = await _mediator.Send(new GetAllUsersByQuery());
        //    string res = JsonConvert.SerializeObject(usersList);
        //    return Ok(res);
        //}

        [Route("GetAllUsers")]
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var result = _loginBusiness.GetAllUsers();
            return Ok(result);

        }
/*
        [AllowAnonymous]
        [Route("GetAllUsers")]
        [HttpGet]
        public IActionResult UnauthorizedBlock()
        {
            var result = _loginBusiness.GetAllUsers();
            return Ok(result);

        }*/

    }
}
