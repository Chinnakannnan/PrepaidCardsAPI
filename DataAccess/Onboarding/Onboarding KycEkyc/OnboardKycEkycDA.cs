using BusinessModel.Common;
using BusinessModel.OnBoarding.OnboardKycEkyc;
using Dapper;
using NeoBank.API.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Onboarding.Onboarding_KycEkyc
{
    class OnboardKycEkycDA : DapperRepository<object>, IOnboardKycEkycDA
    {

        public OnboardKycEkycDA(IDatabaseConfig databaseConfig) : base(databaseConfig)
        {


        }


        public StatusResponseModel UserRegistration(RegistrationRequest data)
        {


            string EmailId = Crypto.AES_ENCRYPT(data.emailAddress, COMMON.EMAILKEY);

            var dParam = new DynamicParameters();
            // objRequest = JsonConvert.DeserializeObject<sendLink>(data);
            //string responsestring = JsonSerializer.Serialize(responseMessage);
            dParam.Add("@EmailId", EmailId);
            if (data.shopname != "") { dParam.Add("@shopname", data.shopname); }
            else { dParam.Add("@shopname", data.firstName); }
            dParam.Add("@title", data.title);
            dParam.Add("@Name", data.firstName);
            dParam.Add("@middleName", data.middleName);
            dParam.Add("@lastName", data.lastName);
            dParam.Add("@gender", data.gender.ToUpper());
            dParam.Add("@AddressLine1", data.address1);
            dParam.Add("@AddressLine2", data.address2);
            dParam.Add("@AddressLine3", data.address3);
            dParam.Add("@City", data.city.ToUpper());
            dParam.Add("@State", data.state);
            dParam.Add("@Zipcode", data.pincode);
            dParam.Add("@Country", data.country);
            dParam.Add("@PanCardNo", data.pan);
            dParam.Add("@GSTNo", data.gst);

            dParam.Add("@MobileNo", data.mobileNo);
            dParam.Add("@dob", data.dob);
            // dParam.Add("@AadhaarNo", data.addressProofNumber);

            if (data.addressProofType == "1")
            {
                dParam.Add("@addressProoftype", "Aadhaar Number");
                dParam.Add("@addressProofUrl", "1");
                dParam.Add("@addressProofNumber", data.aadhaarNo);
            }
            if (data.addressProofType == "2")
            {
                dParam.Add("@addressProoftype", "Driving License");
                dParam.Add("@addressProofUrl", "2");
                dParam.Add("@addressProofNumber", data.drivingLicense);
            }
            if (data.addressProofType == "3")
            {
                dParam.Add("@addressProoftype", "Voter ID");
                dParam.Add("@addressProofUrl", "3");
                dParam.Add("@addressProofNumber", data.voterId);

            }


            dParam.Add("@idProofType", "PAN");
            dParam.Add("@flag", "0");
            dParam.Add("@UserType", data.usertype);

            dParam.Add("@aadhaarNo", data.aadhaarNo);
            dParam.Add("@drivingLicense", data.drivingLicense);
            dParam.Add("@voterId", data.voterId);
            if (data.voterId != "" || data.drivingLicense != "" || data.aadhaarNo != "")
            { dParam.Add("@kycStatus", "1"); }
            else { dParam.Add("@kycStatus", "0"); }


            var result = QuerySP<StatusResponseModel>("SP_UserRegistration", dParam).FirstOrDefault();

            return result;
        }

        public StatusResponseModel UpdateStatus(RegistrationRequest data)
        {
            string EmailId = Crypto.AES_ENCRYPT(data.emailAddress, COMMON.EMAILKEY);

            var dParam = new DynamicParameters();
            // objRequest = JsonConvert.DeserializeObject<sendLink>(data);
            //string responsestring = JsonSerializer.Serialize(responseMessage);

            dParam.Add("@status", data.status);
            dParam.Add("@CustomerId", data.customerId);
            dParam.Add("@password", data.password);
            dParam.Add("@tpin", data.tpin);
            dParam.Add("@aesKey", data.aesKey);
            dParam.Add("@consumerkey", data.consumerkey);
            dParam.Add("@consumersecret", data.consumersecret);
            dParam.Add("@vAccountId", data.vAccountId);
            dParam.Add("@vBankName", data.vBankName);
            dParam.Add("@vIFSCCode", data.vIFSCCode);


            if (data.shopname != "") { dParam.Add("@shopname", data.shopname); }
            else { dParam.Add("@shopname", data.firstName); }
            dParam.Add("@title", data.title);
            dParam.Add("@Name", data.firstName);
            dParam.Add("@middleName", data.middleName);
            dParam.Add("@lastName", data.lastName);
            dParam.Add("@gender", data.gender);
            dParam.Add("@AddressLine1", data.address1);
            dParam.Add("@AddressLine2", data.address2);
            dParam.Add("@AddressLine3", data.address3);
            dParam.Add("@City", data.city);
            dParam.Add("@State", data.state);
            dParam.Add("@Zipcode", data.pincode);
            dParam.Add("@Country", data.country);
            dParam.Add("@PanCardNo", data.pan);
            dParam.Add("@GSTNo", data.gst);
            dParam.Add("@MobileNo", data.mobileNo);
            dParam.Add("@AadhaarNo", data.aadhaarNo);
            dParam.Add("@dob", data.dob);

            if (data.addressProofType == "1")
            { dParam.Add("@addressProoftype", "Aadhaar Number"); }
            if (data.addressProofType == "2")
            { dParam.Add("@addressProoftype", "Driving License"); }
            if (data.addressProofType == "3")
            { dParam.Add("@addressProoftype", "Voter ID"); }

            dParam.Add("@idProofType", "PAN");
            dParam.Add("@idProofNumber", data.pan);

            dParam.Add("@drivingLicense", data.drivingLicense);
            dParam.Add("@voterId", data.voterId);
            dParam.Add("@AadhaarNo", data.aadhaarNo);
            if (data.voterId != "" || data.drivingLicense != "" || data.aadhaarNo != "")
            { dParam.Add("@kycStatus", "1"); }
            else { dParam.Add("@kycStatus", "0"); }

            var result = QuerySP<StatusResponseModel>("SP_UpdateUser", dParam).FirstOrDefault();

            return result;
        }
        public List<RegistrationRequest> GetUser()
        {
            var dParam = new DynamicParameters();
            List<RegistrationRequest> _userList = QuerySP<RegistrationRequest>("Sp_GetUserData", dParam).ToList();
            // List<KitData> _KitList = QuerySP<KitData>("Sp_GetUserData", dParam).ToList();

            return _userList;
        }
        public StatusModel VerifyUserdataEkyc(CheckUserData data)
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
        public List<OnboardKycEkycModel> GotoRefEkyc(detailsOnboard data)
        {
            var dParam = new DynamicParameters();

            dParam.Add("@RefId", data.refId);
            List<OnboardKycEkycModel> _userList = QuerySP<OnboardKycEkycModel>("sp_getby_refid", dParam).ToList();
            return _userList;
        }

        public StatusModel CreateShopEkyc(OnboardKycEkycModel data)
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
            dParam.Add("@Flag", "1");
            dParam.Add("@onboardType", data.onboardType);

            var result = QuerySP<StatusModel>("sp_insprocessupd", dParam).FirstOrDefault();
            return result;
        }


        public StatusModel SaveAadhaarData(detailsOnboard data)
        {
            var dParam = new DynamicParameters();
            dParam.Add("@Aadhaar_Details", data.aadhaarDetails);
            dParam.Add("@ReferId", data.refId);
            dParam.Add("@Type", "3");
            dParam.Add("@GST_Details", "");
            dParam.Add("@PAN_Details", "");
            dParam.Add("@AadhaarNo", data.aadhaarNo);
            dParam.Add("@PanNo", "");
            dParam.Add("@GstNo", "");

            var result = QuerySP<StatusModel>("sp_gstdetails", dParam).FirstOrDefault();
            return result;
        }



        //public StatusModel GotoRef(OnboardSupportModel data)
        //{
        //    var dParam = new DynamicParameters();
        //    dParam.Add("@RefId", data.refId);

        //    var result = QuerySP<StatusModel>("sp_getby_refid", dParam).FirstOrDefault();
        //    return result;
        //}

        public StatusModel SaveGSTDetailsEkyc(detailsOnboard data)
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


        public StatusModel SavePANDetailsEkyc(detailsOnboard data)
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


        public StatusModel SendLink(sendLink data)
        {


            string uniqueId = Crypto.AES_ENCRYPT("Uni_" + Utility.GetStan(), COMMON.EMAILKEY);

            var dParam = new DynamicParameters();
            // objRequest = JsonConvert.DeserializeObject<sendLink>(data);
            //string responsestring = JsonSerializer.Serialize(responseMessage);
            dParam.Add("@firstName", data.firstName);
            dParam.Add("@lastName", data.lastName);
            dParam.Add("@mobileNumber", data.mobileNumber);
            dParam.Add("@emailId", data.emailId);
            dParam.Add("@uniqueId", uniqueId);
            dParam.Add("@mapUserCode", data.mapUsercode);
            dParam.Add("@supUsertype", data.supUsertype);


            var result = QuerySP<StatusModel>("sp_ins_sendlink", dParam).FirstOrDefault();
            return result;
        }



        public StatusModel CheckUniqueId(sendLink data)
        {


            //  string uniqueId = Crypto.AES_ENCRYPT("Uni_" + Utility.GetStan(), COMMON.EMAILKEY);

            var dParam = new DynamicParameters();
            dParam.Add("@uniqueId", data.uniqueId);

            var result = QuerySP<StatusModel>("sp_chk_uniqId", dParam).FirstOrDefault();


            return result;
        }



        public StatusModel RegisterKYCdata(OnboardKycEkycModel data)
        {


            string uniqueId = Crypto.AES_ENCRYPT("Uni_" + Utility.GetStan(), COMMON.EMAILKEY);

            var dParam = new DynamicParameters();
            dParam.Add("@uniqueId", uniqueId);
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
            dParam.Add("@Flag", "1");
            dParam.Add("@AadhaarNo", data.aadhaarNo);
            dParam.Add("@EmailId", data.emailId);
            dParam.Add("@mailOTP", data.mailOTP);
            dParam.Add("@ipAddress", data.ipAddress);
            dParam.Add("@onboardType", "KYC");
            dParam.Add("@mapUserCode", data.mapUserCode);
            dParam.Add("@mapusertype", data.mapusertype);


            var result = QuerySP<StatusModel>("sp_ins_processdata", dParam).FirstOrDefault();

            return result;
        }






        public StatusModel ApprovedData(sendLink data)
        {
            var dParam = new DynamicParameters();
            dParam.Add("@Id", data.mapUsercode);

            var result = QuerySP<StatusModel>("SP_ApprovedData", dParam).FirstOrDefault();
            return result;
        }
        public StatusModel RejectedData(sendLink data)
        {
            var dParam = new DynamicParameters();
            dParam.Add("@Id", data.mapUsercode);

            var result = QuerySP<StatusModel>("SP_ApprovedData", dParam).FirstOrDefault();
            return result;
        }
        public StatusModel WaitingForApproval(sendLink data)
        {
            var dParam = new DynamicParameters();
            dParam.Add("@Id", data.mapUsercode);

            var result = QuerySP<StatusModel>("SP_ApprovedData", dParam).FirstOrDefault();
            return result;
        }


        //string uniqueId = crypto.AES_ENCRYPT(objRequest.uniqueId, COMMON.EMAILKEY);


        //     



        //        ds = objDAL.ExecuteQueryWithParam(objCMD, "");

        //        if (ds != null)
        //        {
        //            if (ds.Tables[0].Rows.Count > 0)
        //            {
        //                if (ds.Tables[0].Rows[0][0].ToString() == "000")
        //                {
        //                    objResp.StatusCode = ds.Tables[0].Rows[0][0].ToString();
        //objResp.Status = ds.Tables[0].Rows[0][1].ToString();
        //System.Diagnostics.Debug.WriteLine("CustomerId", ds.Tables[0].Rows[0][2].ToString());
        //                    objResp.CustomerId = ds.Tables[0].Rows[0][2].ToString();
        //System.Diagnostics.Debug.WriteLine("UserType", ds.Tables[0].Rows[0][3].ToString());

        //                    objResp.UserType = ds.Tables[0].Rows[0][3].ToString();
        //lstrReturn = JsonConvert.SerializeObject(objResp);
        //                    Response.StatusCode = HttpStatusCode.OK;
        //                    Response.Content = new StringContent(lstrReturn, System.Text.Encoding.UTF8, "application/json");
        //                    return Response;


    }
}
