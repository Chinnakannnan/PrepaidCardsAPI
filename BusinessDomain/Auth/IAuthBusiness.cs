using BusinessModel.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.Auth
{
    public interface IAuthBusiness
    {
        Tokens AuthenticatebyLogin(AuthenticateModel authenticateModel, string clientId = null, string clientSecret = null);
        Tokens Authenticate(AuthenticateModel authenticateModel, string clientId = null, string clientSecret = null);
    }
}
