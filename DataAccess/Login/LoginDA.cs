using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessModel.Common;
using BusinessModel.Login;

namespace DataAccess.Login
{
    public class LoginDA : DapperRepository<object>, ILogInDA

    {
        public LoginDA(IDatabaseConfig databaseConfig) : base(databaseConfig)
        {

        }
        public LoginValidateModel GetLoginData(LoginModel loginModel)
        {
            var dParam = new Dapper.DynamicParameters();
            dParam.Add("@userid", loginModel.UserName);
            dParam.Add("@password", loginModel.Password);
            LoginValidateModel _loginList = QuerySP<LoginValidateModel>("sp_get_login_validate", dParam).FirstOrDefault();
            return _loginList;
           
        }
        
        public StatusResponseModel BlockUnauthorized(LoginModel loginModel)
        {
            var dParam = new Dapper.DynamicParameters();
            dParam.Add("@userid", loginModel.UserName);
            StatusResponseModel _loginList = QuerySP<StatusResponseModel>("sp_block_login", dParam).FirstOrDefault();
            return _loginList;

        }
        public LoginResponseModel GetUserInfo(LoginModel loginModel)
        {
            var dParam = new Dapper.DynamicParameters();
            dParam.Add("@userid", loginModel.UserName);
            LoginResponseModel _loginList = QuerySP<LoginResponseModel>("sp_get_login", dParam).FirstOrDefault();
            return _loginList;

        }
        
        public List<LoginModel> GetAllUsers()
        {
            List<LoginModel> _loginList = QuerySP<LoginModel>("Usp_GetAllUsers").ToList();
            return _loginList;
        }
    }
}
