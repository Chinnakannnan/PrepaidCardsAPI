using BusinessModel.Common;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using BusinessModel.Wallet;
using System.Net;

namespace NeoBank.API.Controllers
{
    [Route("yap")]
    [ApiController]
    public class WebhookController : Controller
    {
        public const string lstrFolderName = "callbackWebhook";
        Log objLog = new Log();
        [HttpPost]
        [Route("callback")]
        public IActionResult callback([FromBody] object Request)
        {

            try
            {
                string lstrRequest = string.Empty;
                lstrRequest = Request.ToString();
                objLog.WriteAppLog("webhook : data :" + lstrRequest, lstrFolderName);
                return Ok("success");
            }
            catch (Exception ex)
            {
                objLog.WriteErrorLog("callback error" +ex.Message,lstrFolderName);
                return StatusCode((int)HttpStatusCode.InternalServerError, HttpStatusCode.InternalServerError);
            }
           
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
