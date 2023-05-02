using BusinessDomain.KitMapping;
using BusinessModel.KitMapping;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static NeoBank.API.Startup;

namespace NeoBank.API.Controllers
{
   // [Authorize(Roles = Roles.Admin)]
    [Route("api/KitMapping")]
    [ApiController]
    public class KitMappingController : ControllerBase
    {
        private readonly IKitMappingBusiness _kitMappingBusiness;
        public KitMappingController(IKitMappingBusiness kitMappingBusinessInstance)
        {
            _kitMappingBusiness = kitMappingBusinessInstance;
        }

        [HttpPost]
        [Route("SaveKitMappingDetails")]
        public IActionResult SaveKitMappingDetails(KitMappingModel kitMappingModel)
        {
            try
            {
                var result = _kitMappingBusiness.SaveKitMappingDetails(kitMappingModel);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("UpdateKitDetails")]
        public IActionResult UpdateKitDetails(KitMappingModel kitMappingModel)
        {
            try
            {
                var result = _kitMappingBusiness.UpdateKitDetails(kitMappingModel);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }
        [HttpPost]
        [Route("GetKitMappingDetailsById")]
        public IActionResult GetKitMappingDetailsById(int id)
        {
            try
            {
                var result = _kitMappingBusiness.GetKitMappingDetailsById(id);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("GetAssignedKitMappingDetailsByCustomer")]
        public IActionResult GetAssignedKitMappingDetailsByCustomer(KITMap kitmap)
        {
            try
            {
                var result = _kitMappingBusiness.GetAssignedKitMappingDetailsByCustomer(kitmap.customerId);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

    }
}
