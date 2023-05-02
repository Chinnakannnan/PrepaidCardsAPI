using BusinessModel.Common;
using BusinessModel.OnBoarding.OnboardSupport;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace DataAccess.Onboarding.Onboarding_Support
{
    class OnboardSupportDA : DapperRepository<object>, IOnboardSupportDA
    {
        public OnboardSupportDA(IDatabaseConfig databaseConfig) : base(databaseConfig)
        {
        

        }

     

        public StatusModel VerifyUserdata(verifyUserData data)
        {
            var dParam = new DynamicParameters();
     
            dParam.Add("@MailId", data.emailId);
            dParam.Add("@ReferenceId", data.refId);
            //dParam.Add("@mailOTP", data.mailOTP);
            dParam.Add("@AadhaarNo", data.aadhaarNo);
            //dParam.Add("@Flag", "0");
            //dParam.Add("@onboardType", data.onboardType);


            var result = QuerySP<StatusModel>("sp_insertvalue", dParam).FirstOrDefault();
            return result;
        }


        public List<OnboardSupportModel> GotoRef(detailsOnb data)
        {
            var dParam = new DynamicParameters();

            dParam.Add("@RefId", data.refId);
            List<OnboardSupportModel> _userList = QuerySP<OnboardSupportModel>("sp_getby_refid", dParam).ToList();
            return _userList;
        }

        public StatusModel CreateShop(OnboardSupportModel data)
        {
            var dParam = new DynamicParameters();
         
            dParam.Add("@Name", data.name);
            dParam.Add("@ShopNumber", data.shopNumber);
            dParam.Add("@ShopName", data.shopName);
            dParam.Add("@AddressLine1", data.addressLine1);
            dParam.Add("@AddressLine2", data.addressLine2);
            dParam.Add("@City", data.city);
            dParam.Add("@State", data.state);
            dParam.Add("@Zipcode", data.pincode);
            dParam.Add("@Country", data.country);
            dParam.Add("@GSTNo", data.gstNo);
            dParam.Add("@GSTSoftcopy", data.gstSoftcopy);
            dParam.Add("@GSTImg", data.gst_imgName);
            dParam.Add("@Imgpath", data.imgPath);
            dParam.Add("@PanCardNo", data.pancardNo);
            dParam.Add("@PanSoftcopy", data.panSoftcopy);
            dParam.Add("@PanImg", data.pan_imgName);
            dParam.Add("@AadhaarSoftcopy", data.aadhaarfrontSoftcopy);
            dParam.Add("@AdFImg", data.aadharfront_imgName);
            dParam.Add("@AadharBack", data.aadharbackSoftcopy);
            dParam.Add("@AdBImg", data.aadhaarback_imgName);
            dParam.Add("@MobileNo", data.mobileNo);
            dParam.Add("@Mobile2", data.mobile2);
            dParam.Add("@UserType", data.usertype);
            dParam.Add("@mobOTP", data.mobOTP);
            dParam.Add("@ReferId", data.refId);
            dParam.Add("@ipAddress", data.ipAddress);
            dParam.Add("@Flag","1" );
            dParam.Add("@onboardType", data.onboardType);
           
            var result = QuerySP<StatusModel>("sp_insprocessupd", dParam).FirstOrDefault();
            return result;
        }
     
   
        // public StatusModel (OnboardSupportModel data)
        //{
        //    var dParam = new DynamicParameters();
        //    dParam.Add("@", data.);

        //    var result = QuerySP<StatusModel>("", dParam).FirstOrDefault();
                //    return result;
        //}

        //public StatusModel GotoRef(OnboardSupportModel data)
        //{
        //    var dParam = new DynamicParameters();
        //    dParam.Add("@RefId", data.refId);

        //    var result = QuerySP<StatusModel>("sp_getby_refid", dParam).FirstOrDefault();
        //    return result;
        //}

        public StatusModel SaveGSTDetails(detailsOnb data)
        {
            var dParam = new DynamicParameters();
            dParam.Add("@GST_Details", data.gstDetails);
            dParam.Add("@GstNo", data.gstNo);
            dParam.Add("@ReferId", data.refId);
            dParam.Add("@Type", "1");
            dParam.Add("@Aadhaar_Details", "");
            dParam.Add("@AadhaarNo", "");
            dParam.Add("@PanNo", "");
            dParam.Add("@PAN_Details", "");
    

            var result = QuerySP<StatusModel>("sp_gstdetails", dParam).FirstOrDefault();
            return result;
        }


        public StatusModel SavePANDetails(detailsOnb data)
        {
            var dParam = new DynamicParameters();
            dParam.Add("@PAN_Details", data.panDetails);
            dParam.Add("@PanNo", data.pancardNo);
            dParam.Add("@ReferId", data.refId);
            dParam.Add("@Type", "2");
            dParam.Add("@Aadhaar_Details", "");
            dParam.Add("@AadhaarNo", "");
            dParam.Add("@GST_Details", "");
            dParam.Add("@GstNo", "");

            var result = QuerySP<StatusModel>("sp_gstdetails", dParam).FirstOrDefault();
            return result;
        }


        //nirmal's tab starts here...
        public StatusModel GetUserDetails(getDetailRequest data)
        {
            var dParam = new DynamicParameters();
            dParam.Add("@id", data.merchantid);
            dParam.Add("@flag", data.flag);
            var result = QuerySP<StatusModel>("sp_get_user", dParam).FirstOrDefault();
            return result;
        }
        public StatusModel InsertUserPlan(mapuserplan data)
        {
            var dParam = new DynamicParameters();
            dParam.Add("@Users", data.Users);
            dParam.Add("@UserType", data.usertype);
            dParam.Add("@GroupName", data.GroupName);
            dParam.Add("@FeatureName", data.FeatureName);
            var result = QuerySP<StatusModel>("SP_InsUserPlan", dParam).FirstOrDefault();
            return result;
        }
        public StatusModel Selectuserplan(createusermap data)
        {
            var dParam = new DynamicParameters();
            dParam.Add("@Users", data.Users);


            var result = QuerySP<StatusModel>("Sp_getgroupfromuser", dParam).FirstOrDefault();
            return result;
        }
        public StatusModel CreateUserMapping(createusermap data)
        {
            var dParam = new DynamicParameters();

            dParam.Add("@mapusercode", data.mapusercode);
            dParam.Add("@mapusertype", data.mapusertype);
            dParam.Add("@customerID", data.customerID);

            var result = QuerySP<StatusModel>("sp_onboardmap", dParam).FirstOrDefault();
            return result;
        }
        public StatusModel ListDist(createusermap data)
        {
            var dParam = new DynamicParameters();
            dParam.Add("@mapusercode", data.mapusercode);
            dParam.Add("@usertype", data.usertype);
            var result = QuerySP<StatusModel>("sp_getdist", dParam).FirstOrDefault();
            return result;
        }
        public StatusModel SuperDisgrp(createusermap data)
        {
            var dParam = new DynamicParameters();
            //dParam.Add("@", data.);
            var result = QuerySP<StatusModel>("SP_Get_SuperDisGroup", dParam).FirstOrDefault();
            return result;
        }
        public StatusModel GET_GROUP1(plan data)
        {
            var dParam = new DynamicParameters();
            dParam.Add("@customerId", data.customerId);
            dParam.Add("@mapusertype", data.mapusertype);
            var result = QuerySP<StatusModel>("Sp_getgroupfromuser", dParam).FirstOrDefault();
            return result;
        }
        public StatusModel adminstatus(OnboardSupportModel data)
        {



            string lstremail = Crypto.AES_ENCRYPT(data.emailId, COMMON.EMAILKEY);
            var dParam = new DynamicParameters();
            dParam.Add("@CustomerId", data.customerId);
            dParam.Add("@ShopName", data.shopName);
            dParam.Add("@MobileNo", data.mobileNo);
            dParam.Add("@AddressLine1", data.addressLine1);
            dParam.Add("@AddressLine2", data.addressLine2);
            dParam.Add("@City", data.city);
            dParam.Add("@State", data.state);
            dParam.Add("@Country", data.country);
            dParam.Add("@pincode", data.pincode);
            dParam.Add("@gst", data.gstNo);
            dParam.Add("@pan", data.pancardNo);
            dParam.Add("@usertype", data.usertype);
            dParam.Add("@status", data.status);
            dParam.Add("@onBoardingStatus", data.status);
            dParam.Add("@vAccountId", data.vaccountid);
            dParam.Add("@vIFSCCode",data.vifsccode );
            dParam.Add("@vBankName", data.vbankname);
            dParam.Add("@EmailId", lstremail);
            dParam.Add("@mapusertype",data.mapusertype );
            dParam.Add("@mapUserCode",data.mapUserCode );
            dParam.Add("@idProofNumber",data.aadhaarNo );
            dParam.Add("@idProofType","Aadhaar" );
      
            data.password = Crypto.AES_ENCRYPT(Utility.GetStan(), COMMON.EMAILKEY);
            Thread.Sleep(100);
            data.tpin = Crypto.AES_ENCRYPT(Utility.GetStan(), COMMON.EMAILKEY);

            dParam.Add("@password", data.password);
            dParam.Add("@tpin", data.tpin);
            data.aesKey = Utility.GetAESKEY();
            Thread.Sleep(100);
            data.aesKey += Utility.GetAESKEY();
            Thread.Sleep(100);
            dParam.Add("@aesKey", data.aesKey);
            data.consumerkey = Utility.GetStan() + Utility.GetAlphaChar();
            Thread.Sleep(100);
            dParam.Add("@consumerkey", data.consumerkey );
            data.consumersecret = Utility.GetConsumerSecret();
            dParam.Add("@consumersecret", data.consumersecret);
            var result = QuerySP<StatusModel>("SP_Admin_upd", dParam).FirstOrDefault();
            return result;

        }













        public StatusModel UpdUserRoleMap(UserRoleRes data)
        {
            var dParam = new DynamicParameters();
            dParam.Add("@featurename", data.featurename);
            dParam.Add("@usertype", data.UserType);
            dParam.Add("@customerid", data.CustomerId);
            dParam.Add("@status", data.status);
            var result = QuerySP<StatusModel>("sp_upd_Userrolemap", dParam).FirstOrDefault();
            return result;
        }
        public StatusModel OnBoardget(OnboardSupportModel data)
        {

            string CustomerId = string.Empty;
            string UserType = string.Empty;
            var dParam = new DynamicParameters();
            dParam.Add("@UserType", data.usertype);
            dParam.Add("@CustomerId", data.customerId);
            List<OnboardSupportModel> _userList = QuerySP<OnboardSupportModel>("sp_get_processdata1", dParam).ToList();
            foreach (var item in _userList)
            {
                CustomerId = item.customerId;
                UserType = item.usertype;
            }
            dParam.Add("@CustomerId", CustomerId);
            dParam.Add("@UserType", UserType);

            var result = QuerySP<StatusModel>("sp_onboard_instst", dParam).FirstOrDefault();
            return result;
        }


        public StatusModel UpdRoleMap(UserRoleRes data)
        {
            var dParam = new DynamicParameters();
            dParam.Add("@featurename", data.featurename);
            dParam.Add("@UserType", data.UserType);
            dParam.Add("@CustomerId", data.CustomerId);
            dParam.Add("@status", data.status);
            var result = QuerySP<StatusModel>("sp_upd_Userrolemap", dParam).FirstOrDefault();
            return result;
        }
        public StatusModel UserRoleMap(UserRoleRes data)
        {
            var dParam = new DynamicParameters();
            dParam.Add("@CustomerId", data.CustomerId);
            dParam.Add("@UserType", data.UserType);
            var result = QuerySP<StatusModel>("sp_get_processdata1", dParam).FirstOrDefault();
            return result;
        }












    }
}
