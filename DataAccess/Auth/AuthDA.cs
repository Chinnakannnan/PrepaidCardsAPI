using BusinessModel.Common;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Auth
{
    public class AuthDA : DapperRepository<object>, IAuthDA
    {
        public AuthDA(IDatabaseConfig databaseConfig) : base(databaseConfig)
        {

        }
        public StatusResponseModel Authenticate(string username, string password, string clientId = null, string clientSecret = null)
        {
            var dParam = new DynamicParameters();
            dParam.Add("@Username", username);
            dParam.Add("@Password", password);
            dParam.Add("@ClientId", clientId);
            dParam.Add("@ClientSecret", clientSecret);

            var result = QuerySP<StatusResponseModel>("sp_ValidateUserLogin", dParam).FirstOrDefault();
            return result;
        }

        public int updateJWTToken(string JWTToken, string username)
        {
            var dParam = new DynamicParameters();
            dParam.Add("@Jwttoken", JWTToken);
            dParam.Add("@UserName", username);

            var result = QuerySP<int>("SP_UpdateJWTToken", dParam).FirstOrDefault();
            return result;
        }



        public List<GetUserModel> GetLoginUserData()
        {
            var result = QuerySP<GetUserModel>("SP_userTestData").ToList();
            return result;
        }


    }

    public class GetUserModel
    {
        public string firstName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
