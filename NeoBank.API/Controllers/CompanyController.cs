using BusinessDomain.Company;
using BusinessModel.Company;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NeoBank.API.Controllers
{
    //[Authorize]
    [Route("api/Company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyBusiness _companyBusiness;
        public CompanyController(ICompanyBusiness companyBusinessInstance)
        {
            _companyBusiness = companyBusinessInstance;
        }

        [HttpPost]
        [Route("CreateCompanyDetails")]
        public IActionResult CreateCompanyDetails(CompanyModel companyModel)
        {
            try
            {
                var result = _companyBusiness.CreateCompanyDetails(companyModel);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("UpdateCompanyDetails")]
        public IActionResult UpdateCompanyDetails(CompanyModel companyModel)
        {
            try
            {
                var result = _companyBusiness.UpdateCompanyDetails(companyModel);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetCompanyDetailsById")]
        public IActionResult GetCompanyDetailsById(int id)
        {
            try
            {
                var result = _companyBusiness.GetCompanyDetailsById(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("DeleteCompanyDetailsById")]
        public IActionResult DeleteCompanyDetailsById(int id)
        {
            try
            {
                var result = _companyBusiness.DeleteCompanyDetailsById(id);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }
    }
}
