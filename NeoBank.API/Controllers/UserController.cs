using BusinessDomain.User;
using BusinessModel.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using NeoBank.API.Utilities;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Extensions.Configuration;

namespace NeoBank.API.Controllers
{
    //[Authorize]
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBusiness _userBusiness;
        private readonly IConfiguration _configuration;
       
        public UserController(IUserBusiness userBusinessInstance, IConfiguration configuration)
        {
            _userBusiness = userBusinessInstance;
            _configuration = configuration;
        }
       
        [HttpPost]
        [Route("CreateUserDetails")]
        public IActionResult CreateUserDetails(UserModel userModel)
        {
            try
            {
                var result = _userBusiness.CreateUserDetails(userModel);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetUserDetailsById")]
        public IActionResult GetUserDetailsById(int id)
        {
            try
            {
                var result = _userBusiness.GetUserDetailsById(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetRoleDetails")]
        public IActionResult GetRoleDetails()
        {
            try
            {
                var result = _userBusiness.GetRoleDetails();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("ResetPassword")]
        public IActionResult UpdatePassword(PasswordUpdateModel passwordUpdateModel)
        {

            try
            {
                var result = _userBusiness.UpdatePassword(passwordUpdateModel);
                if(result != null)
                {

                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("RaiseComplaint")]
        public IActionResult RaiseComplaint(ComplaintModel complaintModel)
        {
            var fromAddr = _configuration.GetValue<string>("ApplicationSettings:FROMMAILADDRESS");
            var smtpAddr = _configuration.GetValue<string>("ApplicationSettings:SMTPADDRESS");
            var smtpPort = _configuration.GetValue<string>("ApplicationSettings:SMTPPORT");
            var mailSecret = _configuration.GetValue<string>("ApplicationSettings:FROMMAILSECRET");
            var toaddress = _configuration.GetValue<string>("ApplicationSettings:SUPPORTTEAMMAIL");
            var lstrImageURL = _configuration.GetValue<string>("ApplicationSettings:IMAGEURLPATH");
            
           try
            {
                var result = _userBusiness.Raisecomplaint(complaintModel);
                if (result != null)
                {
                    MailServices.ComplaintMail(complaintModel.CustomerID.ToString(), toaddress, complaintModel.Subject, complaintModel.Comment, fromAddr, smtpAddr, smtpPort, mailSecret, lstrImageURL);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }
    }
}
