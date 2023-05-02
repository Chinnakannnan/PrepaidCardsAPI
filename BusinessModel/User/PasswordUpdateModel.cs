using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.User
{
    public class PasswordUpdateModel
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
        public string CustomerId { get; set; }
    }
    public class GetInfo
    {
        public string Password { get; set; }
        public string AccessKey { get; set; }
    }
}
