using BusinessDomain.Auth;
using BusinessModel.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NeoBank.API.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthBusiness _authBusiness;
        public AuthController(IAuthBusiness authBusinesssInstance) => (_authBusiness) = (authBusinesssInstance);
    

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate(AuthenticateModel authenticateModel)
        {
            //Dictionary<string, string> requestHeaders = new Dictionary<string, string>();

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

            ////foreach (var header in Request.Headers)
            ////{
            ////    requestHeaders.Add(header.Key, header.Value);
            ////}

            var token = _authBusiness.Authenticate(authenticateModel, clientID, clientSecret);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }

    }
}
