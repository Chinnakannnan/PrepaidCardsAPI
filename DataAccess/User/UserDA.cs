using BusinessModel.Common;
using BusinessModel.User;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataAccess.User
{
   public class UserDA : DapperRepository<object>, IUserDA
    {
        public UserDA(IDatabaseConfig databaseConfig) : base(databaseConfig)
        {

        }

        public StatusResponseModel CreateUserDetails(UserModel userModel)
        {
            var dParam = new DynamicParameters();
            
            //dParam.Add("@uniquevalue", userModel.uniquevalue);
            //dParam.Add("@customerId", userModel.customerId);
            dParam.Add("@shopname", userModel.shopname);
            dParam.Add("@title", userModel.title);
            dParam.Add("@firstName", userModel.firstName);
            dParam.Add("@middleName", userModel.middleName);
            dParam.Add("@lastName", userModel.lastName);
            dParam.Add("@gender", userModel.gender);
            dParam.Add("@mobileNo", userModel.mobileNo);
            dParam.Add("@emailAddress", userModel.emailAddress);
            dParam.Add("@address1", userModel.address1);
            dParam.Add("@address2", userModel.address2);
            dParam.Add("@city", userModel.city);
            dParam.Add("@state", userModel.state);
            dParam.Add("@country", userModel.country);
            dParam.Add("@pincode", userModel.pincode);
            dParam.Add("@gst", userModel.gst);
            dParam.Add("@pan", userModel.pan);
            dParam.Add("@userPhotoURL", userModel.userPhotoURL);
            dParam.Add("@idProofType", userModel.idProofType);
            dParam.Add("@idProofNumber", userModel.idProofNumber);
            dParam.Add("@idProofURL", userModel.idProofURL);
            dParam.Add("@addressProofType", userModel.addressProofType);
            dParam.Add("@addressProofNumber", userModel.addressProofNumber);
            dParam.Add("@addressProofURL", userModel.addressProofURL);
            dParam.Add("@password", userModel.password);
            dParam.Add("@tpin", userModel.tpin);
            dParam.Add("@usertype", userModel.usertype);
            dParam.Add("@commissiontype", userModel.commissiontype);
            dParam.Add("@agenttype", userModel.agenttype);
            dParam.Add("@status", userModel.status);
            dParam.Add("@tpinstatus", userModel.tpinstatus);
            dParam.Add("@kycstatus", userModel.kycstatus);
            //dParam.Add("@passwordcount", userModel.passwordcount);
            //dParam.Add("@passwordexpiry", userModel.passwordexpiry);
            dParam.Add("@modifiedby", userModel.modifiedby);
            dParam.Add("@remarks", userModel.remarks);
            dParam.Add("@aesKey", userModel.aesKey);
            dParam.Add("@consumerkey", userModel.consumerkey);
            dParam.Add("@consumersecret", userModel.consumersecret);
            dParam.Add("@vAccountId", userModel.vaccountid);
            dParam.Add("@vIFSCCode", userModel.vifsccode);
            dParam.Add("@vBankName", userModel.vbankname);
            dParam.Add("@mapusertype", userModel.mapusertype);
            dParam.Add("@mapUserCode", userModel.mapUserCode);

            var result = QuerySP<StatusResponseModel>("sp_ins_user", dParam).FirstOrDefault();
            return result;
        }
        public StatusResponseModel CustomerComplaint(ComplaintModel complaintModel)
        {            
            var dParam = new DynamicParameters(); 
            dParam.Add("@customerId", complaintModel.CustomerID);
            dParam.Add("@subject", complaintModel.Subject);
            dParam.Add("@comment", complaintModel.Comment);  

            var result = QuerySP<StatusResponseModel>("sp_customercomplaints", dParam).FirstOrDefault();
            return result;
        }
        public List<UserModel> GetUserDetailsById(int id)
        {
            var dParam = new DynamicParameters();
            dParam.Add("@Id", id);

            List<UserModel> result = QuerySP<UserModel>("sp_GetUserDetailsbyId", dParam).ToList();

            return result;
        }

        public List<RoleModel> GetRoleDetails()
        {            
            List<RoleModel> result = QuerySP<RoleModel>("sp_GetRoleDetails").ToList();

            return result;
        }
        public StatusResponseModel UpdatePassword(PasswordUpdateModel passwordUpdateModel)
        {
            StatusResponseModel values = new StatusResponseModel();
            if (passwordUpdateModel == null) { values.statuscode = "001"; values.statusdesc = "Empty Values, Please fill all values"; return values; }
            var dParam = new DynamicParameters();
            dParam.Add("@CustomerId", passwordUpdateModel.CustomerId);
            GetInfo objParameter = QuerySP<GetInfo>("sp_get_userinfo", dParam).FirstOrDefault();
            if (objParameter == null) { values.statuscode = "001"; values.statusdesc = "Failed!.. Please Check with Admin"; return values; }
            var pswd = Crypto.AES_DECRYPT(objParameter.Password, COMMON.EMAILKEY);
            if (pswd == passwordUpdateModel.NewPassword) { values.statuscode = "001"; values.statusdesc = "Old Password and new password are same. Please use Diffeent password"; return values; }
            if (pswd == passwordUpdateModel.CurrentPassword)
            {
                var dParams = new DynamicParameters();
                dParams.Add("@CustomerId", passwordUpdateModel.CustomerId);
                dParams.Add("@password", Crypto.AES_ENCRYPT(passwordUpdateModel.ConfirmPassword, COMMON.EMAILKEY));
                var result = QuerySP<StatusResponseModel>("sp_ins_password", dParams).FirstOrDefault();
                return result;
            }
            else { values.statuscode = "001"; values.statusdesc = "Failed!.. Please Enter correct current password"; return values; }

        }
    }
}
