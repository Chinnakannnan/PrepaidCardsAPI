using BusinessModel.Common;
using BusinessModel.Login;
using DataAccess.Auth;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.Auth
{
    public class AuthBusiness : IAuthBusiness
    {
        private readonly IAuthDA _authRepository;

        public AuthBusiness(IAuthDA authRepositoryInstance)
        {
            _authRepository = authRepositoryInstance;
        }
        public Tokens Authenticate(AuthenticateModel authenticateModel, string clientId = null, string clientSecret = null)
        {
            var userName = Crypto.AES_ENCRYPT(authenticateModel.Username, COMMON.EMAILKEY);
            var password = Crypto.AES_ENCRYPT(authenticateModel.Password, COMMON.EMAILKEY);
            StatusResponseModel iStatus = _authRepository.Authenticate(userName, password, clientId, clientSecret);
            try
            {
                if (iStatus.statuscode ==CommonStatusCode.InvalidCode)
                {
                    //return status = -1;//"Please check your admin password and try again.";
                    return null;
                }

                //return status = 1;
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes("NeoBankAPI2022!a"); // This will comes from Configuration
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                  {
             new Claim(ClaimTypes.Name, authenticateModel.Username)
                  }),
                    Expires = DateTime.UtcNow.AddMinutes(10),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var JWTToken = tokenHandler.WriteToken(token);
                _authRepository.updateJWTToken(JWTToken, userName);

                return new Tokens { Token = tokenHandler.WriteToken(token) };
            }
            catch (Exception oEx)
            {
                //return status = -1;
                return null;
            }
        }

        public Tokens AuthenticatebyLogin(AuthenticateModel authenticateModel, string clientId = null, string clientSecret = null)
        {
            //var userName = Crypto.AES_ENCRYPT(authenticateModel.Username, COMMON.EMAILKEY);
            //var password = Crypto.AES_ENCRYPT(authenticateModel.Password, COMMON.EMAILKEY);
            StatusResponseModel iStatus = _authRepository.Authenticate(authenticateModel.Username, authenticateModel.Password, clientId, clientSecret);
            try
            {
                if (iStatus.statuscode == CommonStatusCode.InvalidCode)
                {
                    //return status = -1;//"Please check your admin password and try again.";
                    return null;
                }

                //return status = 1;
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.UTF8.GetBytes("NeoBankAPI2022!a"); // This will comes from Configuration
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                  {
             new Claim(ClaimTypes.Name, authenticateModel.Username)
                  }),
                    Expires = DateTime.UtcNow.AddMinutes(10),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var JWTToken = tokenHandler.WriteToken(token);
                _authRepository.updateJWTToken(JWTToken, authenticateModel.Username);

                return new Tokens { Token = tokenHandler.WriteToken(token) };
            }
            catch (Exception oEx)
            {
                //return status = -1;
                return null;
            }
        }

        public List<GetUserModel> GetLoginUserData()
        {
            var result = _authRepository.GetLoginUserData();
            var lst = new List<GetUserModel>();
            foreach (var item in result)
            {
                var usrModel = new GetUserModel();
                usrModel.UserName = Crypto.AES_DECRYPT(item.UserName, COMMON.EMAILKEY);
                usrModel.Password = Crypto.AES_DECRYPT(item.Password, COMMON.EMAILKEY);
                usrModel.firstName = item.firstName;

                lst.Add(usrModel);
            }

            return lst;


        }
    }
}
