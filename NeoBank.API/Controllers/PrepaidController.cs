using BusinessDomain.Prepaid;
using BusinessModel.Prepaid;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BusinessModel.Common;
using Newtonsoft.Json;
using BusinessModel.Wallet;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace NeoBank.API.Controllers
{
    [Authorize]
    [Route("api/Prepaid")]
    [ApiController]
    public class PrepaidController : ControllerBase
    {
        public const string lstrFolderName = "Prepaid Cards";
        Log objLog = new Log();
        private readonly IPrepaidBusiness _prepaidBusiness;
        public PrepaidController(IPrepaidBusiness prepaidBusinessInstance)
        {
            _prepaidBusiness = prepaidBusinessInstance;
        }
        [HttpPost]
        [Route("ActivateCard")]
        public IActionResult ActivateCard(PrepaidSendOTPRequest prepaidSendOTP)
        {
            try
            {
                var result = _prepaidBusiness.RegisterCustomerWithOTP(prepaidSendOTP);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("ActivateCardOTP")]
        public IActionResult ActivateCardOTP(PrepaidSendOTPRequest prepaidSendOTP)
        {
            try
            {
                if (!string.IsNullOrEmpty(prepaidSendOTP.CustomerId))
                {
                    string jsonReq = JsonConvert.SerializeObject(prepaidSendOTP);
                    objLog.WriteAppLog("Send OTP to Register ActivateCardOTP - Request  :" + jsonReq, lstrFolderName);
                    var result = _prepaidBusiness.SendMobOTP(prepaidSendOTP);
                    return Ok(result);
                }
                else
                {
                    objLog.WriteAppLog("Send OTP to Register ActivateCardOTP - Request  : Customer Id is missing", lstrFolderName);
                    return StatusCode((int)HttpStatusCode.BadRequest, HttpStatusCode.BadRequest);
                }
            }
            catch (Exception ex)
            {
                objLog.WriteErrorLog("Send OTP to Register ActivateCardOTP - Request  :" + ex.ToString(), lstrFolderName);
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("RegisterUser")]
        public IActionResult RegisterUser(PrepaidModel prepaidModel)
        {
            try
            {
                var result = _prepaidBusiness.RegisterUser(prepaidModel);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("UpdateCustomer")]
        public IActionResult UpdateCustomer(UpdateCustomerRequest updateCustomerRequest)
        {
            try
            {
                var result = _prepaidBusiness.UpdateCustomer(updateCustomerRequest);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("CustomerTransactionLimit")]
        public IActionResult CustomerTransactionLimit(CustomerDailyTransactionLimitRequest customerTransactionLimitRequest)
        {
            try
            {
                var result = _prepaidBusiness.CustomerTransactionLimit(customerTransactionLimitRequest);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("AddCard")]
        public IActionResult AddCard(AddCardRequest addCardRequest)
        {
            try
            {
                var result = _prepaidBusiness.AddCard(addCardRequest);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("CustomerPreferencesExternal")]
        public IActionResult CustomerPreferencesExternal(CustomerPreferencesExternalRequest customerPreferencesExternalRequest)
        {
            try
            {
                var result = _prepaidBusiness.CustomerPreferencesExternal(customerPreferencesExternalRequest);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("FetchCustomerPreferences")]
        public IActionResult FetchCustomerPreferences(FetchCustomerPreferenceslRequest customerPreferencesExternalRequest)
        {
            try
            {
                var result = _prepaidBusiness.FetchCustomerPreferences(customerPreferencesExternalRequest);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("UpdateKYCStatus")]
        public IActionResult UpdateKYCStatus(UpdateKYCStatusRequest updateKYCStatusRequest)
        {
            try
            {
                var result = _prepaidBusiness.UpdateKYCStatus(updateKYCStatusRequest);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("SetPin")]
        public IActionResult SetPin(SetPinRequest setPinRequest)
        {
            try
            {
                var result = _prepaidBusiness.SetPin(setPinRequest);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("FetchCardWidget")]
        public IActionResult FetchCardWidget(CardWidgetsRequest widgetData)
        {
            try
            {
                var result = _prepaidBusiness.FetchCardWidget(widgetData);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("SetPinWidget")]
        public IActionResult SetPinWidget(CardWidgetsRequest widgetData)
        {
            try
            {
                var result = _prepaidBusiness.SetPinWidget(widgetData);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

    }
}
