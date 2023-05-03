using BusinessDomain.Kit;
using BusinessModel.Common;
using BusinessModel.Kit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static NeoBank.API.Startup;

namespace NeoBank.API.Controllers
{
    //[Authorize(Roles = Roles.Admin)]
    [Route("api/Kit")]
    [ApiController]
    public class KitController : ControllerBase
    {
        private readonly IKitBusiness _kitBusiness;
        StatusResponseModel statusModal = new StatusResponseModel();
        string strResponse = string.Empty;
        public KitController(IKitBusiness kitBusinessInstance) => (_kitBusiness)=(kitBusinessInstance);
       

        [HttpPost]
        [Route("CreateKitDetails")]
        public IActionResult CreateKitDetails(KitModel kitModel)
        {
            try
            {
                if(kitModel == null)
                {
                    statusModal.statuscode = ResponseCode.Request_Empty;
                    statusModal.statusdesc = ResponseMsg.Request_Empty;
                    strResponse = JsonConvert.SerializeObject(statusModal);
                    return StatusCode((int)HttpStatusCode.NotFound, strResponse);
                }
                if (string.IsNullOrEmpty(kitModel.KitReferenceNumber) && string.IsNullOrEmpty(kitModel.CardNo))
                {
                    statusModal.statuscode = ResponseCode.InvalidKitOrEmpty;
                    statusModal.statusdesc = ResponseMsg.InvalidKitOrEmpty;
                    strResponse = JsonConvert.SerializeObject(statusModal);
                    return StatusCode((int)HttpStatusCode.NotFound, strResponse);
                }
                var result = _kitBusiness.CreateKitDetails(kitModel);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }


        [HttpPost]
        [Route("AddBulkKitDetails")]
        public IActionResult AddBulkKitDetails(Object dataset)
        {

            try
            {
                if (dataset == null)
                {
                    statusModal.statuscode = ResponseCode.Request_Empty;
                    statusModal.statusdesc = ResponseMsg.Request_Empty;
                    strResponse = JsonConvert.SerializeObject(statusModal);
                    return StatusCode((int)HttpStatusCode.NotFound, strResponse);
                }

                var result = _kitBusiness.AddBulkKitDetails(dataset);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        [HttpPost]
        [Route("UpdateKitDetails")]
        public IActionResult UpdateKitDetails(KitModel kitModel)
        {
            try
            {
                var result = _kitBusiness.UpdateKitDetails(kitModel);

                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetKitDetailsById")]
        public IActionResult GetKitDetailsById(int id)
        {
            try
            {
                var result = _kitBusiness.GetKitDetailsById(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetKitDetails")]
        public IActionResult GetKitDetails()
        {
            try
            {
                var result = _kitBusiness.GetKitDetails();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
        }
    }
}
