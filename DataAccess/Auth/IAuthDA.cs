using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessModel.Common;

namespace DataAccess.Auth
{
    public interface IAuthDA
    {
        StatusResponseModel Authenticate(string username, string password, string clientId = null, string clientSecret = null);

        int updateJWTToken(string JWTToken, string username);

        List<GetUserModel> GetLoginUserData();
    }
}