using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessModel.Common;
using BusinessModel.Login;

namespace DataAccess.Login
{
    public interface ILogInDA
    {
        LoginValidateModel GetLoginData(LoginModel loginModel);
        LoginResponseModel GetUserInfo(LoginModel loginModel);
        StatusResponseModel BlockUnauthorized(LoginModel loginModel);
        List<LoginModel> GetAllUsers();
    }
}
