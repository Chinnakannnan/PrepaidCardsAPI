using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System;
using BusinessModel.TransactionModel;
using BusinessDomain.Aadhaar;
using BusinessDomain.MobileOTP;
using BusinessDomain.Onboarding.OnboardKycEkyc;
using BusinessDomain.PAN;
using DataAccess.Onboarding.Onboarding_KycEkyc;
using Microsoft.Extensions.Configuration;
using DataAccess.Transactions;
using BusinessDomain.Transactions;

namespace NeoBank.API.Controllers
{
    [Route("api/Transaction")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
      
  
        private readonly IConfiguration _configuration;
        private readonly ITransactionBusiness _transactionBusiness;
        public TransactionController( ITransactionBusiness transactionBussinessInstance, IConfiguration Configuration)
        {
            _transactionBusiness = transactionBussinessInstance;
        
        }
         [HttpPost]
        [Route("GetTransactionDetails")]
        public IActionResult GetTransaction(TransactionModel data)
        {
            try
            {
                if (data == null)
                {
                    var hello = "Data is null";
                    return BadRequest(hello);
                }
                var result = _transactionBusiness.GetTransaction(data);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }
    }
}
