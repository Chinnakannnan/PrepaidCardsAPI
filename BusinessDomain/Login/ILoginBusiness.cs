
namespace BusinessDomain.Login
{
    using BusinessModel.Common;
    using BusinessModel.Login;
    using BusinessModel.MobileOTP;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface ILoginBusiness
    {
        List<LoginModel> GetAllUsers();
        LoginValidateModel GetLoginData(LoginModel loginModel, string clientId = null, string clientSecret = null);
        LoginResponseModel GetUserInfo(LoginModel loginModel);
        StatusResponseModel BlockUnauthorized(LoginModel loginModel);
        string sendOTPMail(MobileOTPModel data);
    }
}
