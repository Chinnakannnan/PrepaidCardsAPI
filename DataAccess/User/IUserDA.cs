using BusinessModel.Common;
using BusinessModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.User
{
    public interface IUserDA
    {
        StatusResponseModel CreateUserDetails(UserModel userModel);
        List<UserModel> GetUserDetailsById(int id);
        List<RoleModel> GetRoleDetails();
        StatusResponseModel UpdatePassword(PasswordUpdateModel passwordUpdateModel);
        public StatusResponseModel CustomerComplaint(ComplaintModel complaintModel);
    }
}
