using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.Login
{
    public class LoginModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class LoginValidateModel
    {
        public string StatusCode { get; set; }
        public string StatusDesc { get; set; }
        public string MobileNo { get; set; }
        public string Token { get; set; }

    }
    public class LoginResponseModel
    {
        public string StatusCode { get; set; }
        public string StatusDesc { get; set; }
        public string MobileNo { get; set; }
        public string emailAddress { get; set; }
        public string CustomerId { get; set; }
        public string UserType { get; set; }
        public string LoginName { get; set; }
        public string AesKey { get; set; }
        public string vaccountid { get; set; }
        public string vifsccode { get; set; }
        public string EntityId { get; set; }
        public string Token { get; set; }

    }
}
