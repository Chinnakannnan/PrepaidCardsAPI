using BusinessDomain.Aadhaar;
using BusinessModel.Aadhaar;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NeoBank.API.Controllers
{
    [Route("api/Aadhaar")]
    [ApiController]
    public class AadhaarController : ControllerBase
    {
        private readonly IAadhaarBusiness _aadhaarBusiness;
        public AadhaarController(IAadhaarBusiness aadhaarBusinessInstance)
        {
            _aadhaarBusiness = aadhaarBusinessInstance;
        }

        [HttpPost]
        [Route("GenerateOTP")]
        public IActionResult GenerateOTP(AadhaarOTPRequest aadhaarOTPRequest)
        {
            try
            {
                var result = _aadhaarBusiness.GenerateOTP(aadhaarOTPRequest);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("KYCWithOTP")]
        public IActionResult KYCWithOTP(AadhaarKYCRequest aadhaarKYCRequest)
        {
            try
            {
                var result = _aadhaarBusiness.KYCWithOTP(aadhaarKYCRequest);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }
    }
}
