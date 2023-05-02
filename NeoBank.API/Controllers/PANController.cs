using BusinessDomain.PAN;
using BusinessModel.GST_Validation;

using BusinessModel.OnBoarding.OnboardSupport;
using DataAccess.Onboarding.Onboarding_Support;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NeoBank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PANController : ControllerBase
    {
        private readonly IPanBusiness _PanBusiness;
       // private readonly IOnboardSupportDA _onboardSupport;
        public PANController(IPanBusiness panBusinessInstance)
        {
            _PanBusiness = panBusinessInstance;
            //  _onboardSupport = aadhaarInstance;, IOnboardSupportDA aadhaarInstance
        }

        [HttpPost]
        [Route("getPanData")]
        public IActionResult getPanData(PanRequest aadhaarOTPRequest)
        {
            try
            {
                var result = _PanBusiness.getPanData(aadhaarOTPRequest);
                //var result = _onboardSupport.SaveGSTDetails(panData);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        //[HttpPost]
        //[Route("SavePANData")]
        //public IActionResult SavePANData(detailsOnb aadhaarOTPRequest)
        //{
        //    try
        //    {
        //        var result = _onboardSupport.SavePANDetails(aadhaarOTPRequest);
        //        // var result = _onboardSupport.SavePANDetails(gstData);
        //        // var result = _PanBusiness.getGSTData(aadhaarOTPRequest);

        //        return Ok(result);
        //    }
        //    catch (Exception)
        //    {
        //        return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
        //    }
        //}
        [HttpPost]
        [Route("getGSTData")]
        public IActionResult getGSTData(PanRequest aadhaarOTPRequest)
        {
            try
            {
                var result = _PanBusiness.getGSTData(aadhaarOTPRequest);
               // var result = _onboardSupport.SavePANDetails(gstData);
                // var result = _PanBusiness.getGSTData(aadhaarOTPRequest);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        //[HttpPost]
        //[Route("saveGSTData")]
        //public IActionResult saveGSTData(string aadhaarOTPRequest)
        //{
        //    try
        //    {
               
        //        var result = _onboardSupport.SaveGSTDetails(aadhaarOTPRequest);
        //        // var result = _PanBusiness.getGSTData(aadhaarOTPRequest);

        //        return Ok(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
        //    }
        //}
    }
}
