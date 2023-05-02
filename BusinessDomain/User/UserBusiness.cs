using BusinessModel.Common;
using BusinessModel.User;
using DataAccess.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.User
{
    public class UserBusiness : IUserBusiness
    {
        private readonly IUserDA _userRepository;

        public UserBusiness(IUserDA userRepositoryInstance)
        {
            _userRepository = userRepositoryInstance;
        }
        public StatusResponseModel CreateUserDetails(UserModel userModel)
        {
            return _userRepository.CreateUserDetails(userModel);
        }
        public List<UserModel> GetUserDetailsById(int id)
        {
            return _userRepository.GetUserDetailsById(id);
        }
        public List<RoleModel> GetRoleDetails()
        {
            return _userRepository.GetRoleDetails();
        }
        public StatusResponseModel UpdatePassword(PasswordUpdateModel passwordUpdateModel)
        {
            return _userRepository.UpdatePassword(passwordUpdateModel);
        }
        public StatusResponseModel Raisecomplaint(ComplaintModel complaintModel)
        {
            return _userRepository.CustomerComplaint(complaintModel);
        }
    }
}
