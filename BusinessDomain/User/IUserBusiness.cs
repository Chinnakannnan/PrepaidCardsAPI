using BusinessModel.Common;
using BusinessModel.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.User
{
    public interface IUserBusiness
    {
        StatusResponseModel CreateUserDetails(UserModel userModel);
        List<UserModel> GetUserDetailsById(int id);
        List<RoleModel> GetRoleDetails();
        StatusResponseModel UpdatePassword(PasswordUpdateModel passwordUpdateModel);
        StatusResponseModel Raisecomplaint(ComplaintModel complaintModel);
    }
}
