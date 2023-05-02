
namespace BusinessDomain.Login
{
    using BusinessModel.Login;
    using DataAccess.Login;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BusinessModel.Common;
    using NeoBank.API.Utilities;
    using Microsoft.Extensions.Configuration;
    using System.Configuration;
    using BusinessModel.MobileOTP;
    using BusinessDomain.Auth;
    using Newtonsoft.Json.Serialization;

    public class LoginBusiness : ILoginBusiness
    {
        private readonly ILogInDA _loginRepository;
        private readonly IConfiguration _configuration;
        private readonly IAuthBusiness _authBusiness;
        private readonly ILogInDA _loginDA;
        public LoginBusiness(ILogInDA loginRepositoryInstance, IConfiguration configuration, IAuthBusiness authBusiness, ILogInDA loginDA) => 
       (_loginRepository, _configuration, _authBusiness,_loginDA) = (loginRepositoryInstance,configuration, authBusiness, loginDA);


        public List<LoginModel> GetAllUsers()
        {
            return _loginRepository.GetAllUsers();
        }
        public LoginValidateModel GetLoginData(LoginModel loginModel, string clientId = null, string clientSecret = null)
        {
            LoginValidateModel results = new LoginValidateModel();
            if (loginModel.UserName.Contains("@"))
            {
                loginModel.UserName = Crypto.AES_ENCRYPT(loginModel.UserName, COMMON.EMAILKEY);
            }
            loginModel.Password = Crypto.AES_ENCRYPT(loginModel.Password, COMMON.EMAILKEY);
            //string u = Crypto.AES_DECRYPT("vSMQnX6UjTksWcI+JxoQI31qTph6nhx1Sp+0F01e2bs=", COMMON.EMAILKEY);
            //string u1 = Crypto.AES_DECRYPT("O6jdlmUeOYfAlMheUMWcCGUTTNi07X3Wb9/wUV1HJ3E=", COMMON.EMAILKEY);
            //string p1 = Crypto.AES_DECRYPT("CpEsurU6qzKRijI/TlF+gg==", COMMON.EMAILKEY);
            var result = _loginRepository.GetLoginData(loginModel);

            if(result.StatusCode == "000"){
                AuthenticateModel authenticateModel = new AuthenticateModel();
                authenticateModel.Username = loginModel.UserName;
                authenticateModel.Password = loginModel.Password;
                var token = _authBusiness.AuthenticatebyLogin(authenticateModel, clientId, clientSecret);
                if (token is null)
                {
                    return null;
                }
                result.Token = token.Token;
                return result;
            }
            else
            {
                results.StatusCode=result.StatusCode;
                results.StatusDesc = result.StatusDesc;
                return results;
            }

          
        }

        public StatusResponseModel BlockUnauthorized(LoginModel loginModel)
        {
            try
            {
                if (loginModel.UserName.Contains("@"))
                {
                    loginModel.UserName = Crypto.AES_ENCRYPT(loginModel.UserName, COMMON.EMAILKEY);
                }
                var result = _loginDA.BlockUnauthorized(loginModel);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }


        }

        public LoginResponseModel GetUserInfo(LoginModel loginModel)
        {
            try
            {
                if (loginModel.UserName.Contains("@"))
                {
                    loginModel.UserName = Crypto.AES_ENCRYPT(loginModel.UserName, COMMON.EMAILKEY);
                }
                var result = _loginDA.GetUserInfo(loginModel);
                return result;
            }
            catch (Exception ex) {
                return null; 
            }


        }

        

        public string sendOTPMail(MobileOTPModel data)
        {
            string imgUrl = _configuration.GetSection("appSettings")["IMAGEURLPATH"];
            string fromAddr = _configuration.GetSection("appSettings")["FROMMAILADDRESS"];
            string smtpAddr = _configuration.GetSection("appSettings")["SMTPADDRESS"];
            string smtpPort = _configuration.GetSection("appSettings")["SMTPPORT"];
            string mailSecret = _configuration.GetSection("appSettings")["FROMMAILSECRET"];
            //string EmailId = Crypto.AES_ENCRYPT(data.emailAddress, COMMON.EMAILKEY);
            string EmailId = Crypto.AES_ENCRYPT("cynthia@accupaydtech.com", COMMON.EMAILKEY);
            string OTP = Crypto.AES_ENCRYPT(data.mobileOTP, COMMON.EMAILKEY);
            MailServices.UserEntryOTP(EmailId,  OTP,  imgUrl,  fromAddr,  smtpAddr,  smtpPort,  mailSecret);
            var result = "Success";
            return result;
        }
    }
}
