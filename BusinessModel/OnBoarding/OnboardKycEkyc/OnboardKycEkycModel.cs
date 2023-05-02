using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.OnBoarding.OnboardKycEkyc
{
    public static class MOBILEOTP
    {
        public static string URL = "https://api.itextos.com/genericapi/MQSRequestReceiver?username=ACCUPAYDTXN&password=rGUZcyZh";
        public static string TEMPLATE1 = "&from=ACUPYD&content=";
        public static string TEMPLATE2 = "Dear Customer,";
        //public static string TEMPLATE3 = "is the OTP. NEVER SHARE THE OTP WITH ANYONE. - ACCUPAYD TECH";
        public static string TEMPLATE3 = "is your ACCUPAYD TECH - Transcorp  Card OTP. Don't share the OTP with anyone. - Team ACCUPAYD TECH";

    }
    public class kycgetList
    {

        public string Sno { get; set; }
        public string Name { get; set; }
        public string Refid { get; set; }

        public string CustomerId { get; set; }

        public string ShopNumber { get; set; }
        public string ShopName { get; set; }
        public string Password { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string pincode { get; set; }
        public string Zipcode { get; set; }
        public string Country { get; set; }
        public string GSTNo { get; set; }
        public string GSTSoftcopy { get; set; }


        public string GSTImg { get; set; }
        public string Imgpath { get; set; }
        public string PanCardNo { get; set; }
        //public string PanSoftcopy { get; set; }
        //public string PanSoftcopy1 { get; set; }
        public string panSoftcopy { get; set; }
        public string AadhaarNo { get; set; }
        public string AadhaarSoftcopy { get; set; }
        public string AadharBack { get; set; }
        public string Status { get; set; }
        public string AdFImg { get; set; }
        public string onboardType { get; set; }
        public string mapusercode { get; set; }
        public string mapusertype { get; set; }

        public string AdBImg { get; set; }
        public string MobileNo { get; set; }
        public string Mobile2 { get; set; }
        public string EmailId { get; set; }
        public string UserType { get; set; }
        public string mobOTP { get; set; }
        public string mailOTP { get; set; }
        public string flag { get; set; }
    }
    public class OnboardKycEkycModel
    {
         public string sno { get; set; }
            public string status { get; set; }
            public string name { get; set; }
            public string middleName { get; set; }
            public string lastName { get; set; }
            public string title { get; set; }
            public string gender { get; set; }
            public string refId { get; set; }

            public string customerId { get; set; }

            public string shopNumber { get; set; }
            public string shopName { get; set; }
            public string password { get; set; }
            public string tpin { get; set; }

            public string addressLine1 { get; set; }
            public string addressLine2 { get; set; }
            public string addressLine3 { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string pincode { get; set; }
            public string country { get; set; }
            public string gstNo { get; set; }
            public string gstSoftcopy { get; set; }
            public string gst_imgString { get; set; }
            public string gst_imgName { get; set; }
            public string imgPath { get; set; }
            public string pancardNo { get; set; }
            public string panSoftcopy { get; set; }
            public string pan_imgString { get; set; }
            public string pan_imgName { get; set; }
            public string aadhaarNo { get; set; }
            public string aadhaarfrontSoftcopy { get; set; }
            public string aadhaarfront_imgString { get; set; }
            public string aadharfront_imgName { get; set; }
            public string aadharbackSoftcopy { get; set; }
            public string aadharback_imgString { get; set; }
            public string aadhaarback_imgName { get; set; }
            public string idProofType { get; set; }
            public string mobileNo { get; set; }
            public string mobile2 { get; set; }
            public string emailId { get; set; }
            public string usertype { get; set; }
            public string mobOTP { get; set; }
            public string mailOTP { get; set; }
            public string ipAddress { get; set; }
            public string onboardType { get; set; }
            public string aesKey { get; set; }
            public string consumerkey { get; set; }
            public string consumersecret { get; set; }
            public string mapusertype { get; set; }
            public string mapUserCode { get; set; }
            public string uniqueId { get; set; }
            public string flag { get; set; }

        }
    public class detailsOnboard
    {
        public string aadhaarNo { get; set; }
        public string aadhaarDetails { get; set; }
        public string pancardNo { get; set; }
        public string refId { get; set; }
        public string panDetails { get; set; }
        public string gstNo { get; set; }
        public string gstDetails { get; set; }
    }
    public class CheckUserData
        {
            public string emailId { get; set; }
            public string refId { get; set; }
            //  public string mailOTP { get; set; }

            // public string onboardType { get; set; }
            public string aadhaarNo { get; set; }

        }



        //public static class COMMON
        //{
        //    public static string EMAILKEY = "9BE744B6F2379746";
        //    public static string KEY = "37974F8A5997A49B";
        //    public static string LKEY = "74F8A997A49B59A997A3749B53774F89";
        //}
       

        public class CashRequest
        {
            public string customerid { get; set; }
            public string id { get; set; }
            public string transactiontype { get; set; }
            public string transactionid { get; set; }
            public string branchname { get; set; }
            public string utrno { get; set; }
            public string amount { get; set; }
            public string cashsource { get; set; }
            public string remarks { get; set; }
            public string status { get; set; }
            public string updatedby { get; set; }
            public string ipaddress { get; set; }
            public string hash { get; set; }
        }

        public class CashRequestDetails
        {
            public string customerid { get; set; }
            public string flag { get; set; }
            public string fromdate { get; set; }
            public string todate { get; set; }
            public string hash { get; set; }
        }

        public class PrintDetails
        {
            public string transactionid { get; set; }
            public string id { get; set; }
            public string hash { get; set; }
        }

        public class PgReport
        {
            public string adminid { get; set; }
            public string flag { get; set; }
            public string fromdate { get; set; }
            public string todate { get; set; }
        }

        public class LoginRequest
        {
            public string userid { get; set; }
            public string customerId { get; set; }
            public string mobOTP { get; set; }
            public string password { get; set; }
            public string usertype { get; set; }
            public string Captcha { get; set; }
            public string StatusCode { get; set; }
            public string Status { get; set; }

        }
        public class VerifyUserData
        {
            public string customerId { get; set; }
            public string userType { get; set; }
            public string ipAddress { get; set; }
            public string browser { get; set; }
            public string deviceName { get; set; }
            public string lastOTPSentDate { get; set; }
            public string currentDate { get; set; }
            public string mobileOTP { get; set; }
            public string mobileNo { get; set; }
            public string emailId { get; set; }
            public string latitude { get; set; }
            public string longitude { get; set; }
            public string error { get; set; }
            public string flag { get; set; }
        }

        public class ChangePasswordRequest
        {
            public string mobileNo { get; set; }
            public string emailAddress { get; set; }
            public string password { get; set; }
            public string tpin { get; set; }
            public string modifiedby { get; set; }
            public string flag { get; set; } // 1 - password 2 -- tpin
        }

        public class commonReq
        {
            public string customerId { get; set; }
            public string usertype { get; set; }
            public string mobileno { get; set; }
        }

        public class RegistrationRequest
        {
            public string customerId { get; set; }
            public string shopname { get; set; }
            public string title { get; set; }
            public string firstName { get; set; }
            public string middleName { get; set; }
            public string lastName { get; set; }
            public string gender { get; set; }
            public string mobileNo { get; set; }
            public string emailAddress { get; set; }
            public string address1 { get; set; }
            public string address2 { get; set; }
            public string address3 { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string country { get; set; }
            public string pincode { get; set; }
            public string gst { get; set; }
            public string pan { get; set; }
            public string userPhotoURL { get; set; }
            public string idProofType { get; set; }
            public string idProofNumber { get; set; }
            public string idProofURL { get; set; }
            public string addressProofType { get; set; }
            public string addressProofNumber { get; set; }
            public string addressProofURL { get; set; }
            public string password { get; set; }
            public string tpin { get; set; }
            public string usertype { get; set; }
            public string commissiontype { get; set; }
            public string agenttype { get; set; }
            public string status { get; set; }
            public string tpinstatus { get; set; }
            public string kycstatus { get; set; }
            public string passwordcount { get; set; }
            public string passwordexpiry { get; set; }
            public string cdatetime { get; set; }
            public string mdatetime { get; set; }
            public string modifiedby { get; set; }
            public string createdby { get; set; }
            public string remarks { get; set; }
            public string aesKey { get; set; }
            public string consumerkey { get; set; }
            public string consumersecret { get; set; }
            public string StatusCode { get; set; }
            public string StatusDesc { get; set; }
            public string vAccountId { get; set; }
            public string vIFSCCode { get; set; }
            public string vBankName { get; set; }
            public string mapusertype { get; set; }
            public string mapUserCode { get; set; }
            public string kitStatus { get; set; }
         public string drivingLicense { get; set; }
        public string voterId { get; set; }
        public string aadhaarNo { get; set; }
        public string dob { get; set; }

        //@usertype int, -- 1- admin - 2- super distributor 3 - distributor 4- retailer
        //@commissiontype int, -- 0 - ADMIN 1 - WL - 2 - API - 3- PG
        //@agenttype int, -- 1- admin - 2- super distributor 3 - distributor 4- retailer
        //@status int, -- 0 - pending 1 - active 2 - blocked 3 - in-active
    }
        public class PgLinkFilter
        {
            public string pglinktype { get; set; }

        }
        public class PGCharges
        {
            public string sno { get; set; }
            public string apicharges { get; set; }
            public string admincharges { get; set; }
            public string merchantcharges { get; set; }
            public string master { get; set; }

        }










        public class RoleRes
        {
            public string featurename { get; set; }
            public string usertype_array { get; set; }
            public string val_array { get; set; }


        }

        public class validation
        {

            public string task { get; set; }

            public string pancardNo { get; set; }
            public string gstNo { get; set; }
            public string refId { get; set; }
            public string panDetails { get; set; }
        }
        public class SearchBank
        {
            public string searchtext { get; set; }
        }


        public class UserRoleRess
        {
            public string featurename { get; set; }
            public string UserType { get; set; }
            public string CustomerId { get; set; }
            public string status { get; set; }

        }

        public class JsonResponseOnb
        {
            public string StatusCode { get; set; }
            public string Status { get; set; }
            public string CustomerId { get; set; }
            public object ResponseContent { get; set; }
            public object ResponseContent1 { get; set; }
            public object ResponseContent2 { get; set; }


            public string RefId { get; set; }
            public string Flag { get; set; }

            public string id { get; set; }
            public string userId { get; set; }
            public string patronId { get; set; }
            public result getres { get; set; }
            public string GroupID { get; set; }


            public string UserType { get; set; }
            public string shopname { get; set; }
            public string firstname { get; set; }
            public string uniId { get; set; }


        }

        public class validationJsonResponse
        {

            public string StatusCode { get; set; }
            public string Status { get; set; }
            public string authuid { get; set; }
            public string userId { get; set; }
            public string ttl { get; set; }
            public string created { get; set; }
            public string requestId { get; set; }
            public string aadhaarNo { get; set; }
            public string Aadharno { get; set; }
            public object ResponseContent { get; set; }
            public string emailId { get; set; }
            public string request_id { get; set; }
            public string otp { get; set; }
        }
        public class result
        {
            public string url { get; set; }
            public string requestId { get; set; }
            public string name { get; set; }
            public string number { get; set; }
        }



        public class Themeobj
        {
            public string ThemeName { get; set; }
            public string Active { get; set; }
            public string ThemeColor { get; set; }
            public string filepath { get; set; }
            public string ID { get; set; }
        }
        public class Configobj
        {
            public string ProjectName { get; set; }
            public string ModuleName { get; set; }
            public string Environment { get; set; }
            public string Keys { get; set; }
            public string Value { get; set; }
            public string Cdatetime { get; set; }
            public string Cuser { get; set; }
            public string Mdatetime { get; set; }
            public string Muser { get; set; }
        }

        public class kycget
        {
            public string sno { get; set; }
            public string name { get; set; }
            public string refId { get; set; }
            public string customerId { get; set; }
            public string shopNumber { get; set; }
            public string shopName { get; set; }
            public string addressLine1 { get; set; }
            public string addressLine2 { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            //public string pincode { get; set; }
            public string zipcode { get; set; }
            public string country { get; set; }
            public string gstNo { get; set; }
            //  public HttpPostedFileBase gstSoftcopy { get; set; }
            public string gst_imgString { get; set; }
            public string gst_imgName { get; set; }
            public string pancardNo { get; set; }
            // public HttpPostedFileBase panSoftcopy { get; set; }
            public string pan_imgString { get; set; }
            public string pan_imgName { get; set; }
            public string aadhaarNo { get; set; }
            //  public HttpPostedFileBase aadhaarfrontSoftcopy { get; set; }
            public string aadhaarfront_imgString { get; set; }
            public string aadharback_imgString { get; set; }
            public string aadharfront_imgName { get; set; }
            //  public HttpPostedFileBase aadharbackSoftcopy { get; set; }
            public string aadhaarback_imgName { get; set; }

            public string mobileNo { get; set; }
            public string mobile2 { get; set; }
            public string emailId { get; set; }
            public string usertype { get; set; }
            public string mailOTP { get; set; }
            public string mobOTP { get; set; }
            public string flag { get; set; }
            public string mobileOtp { get; set; }


            // public string Zipcode { get; set; }



        }
        public class MappingUserplan
        {
            public MappingUserplan()
            {
                ResponseContent = new List<UserMapping>();
            }
            public string StatusCode { get; set; }
            public string Status { get; set; }
            public string AgentName { get; set; }
            public string CustomerId { get; set; }
            public string UserType { get; set; }
            public string shopname { get; set; }
            public string firstname { get; set; }


            public List<UserMapping> ResponseContent { get; set; }

        }


        public class UserMapping
        {

            public string FeatureName { get; set; }
            public string status { get; set; }

            public string GroupID { get; set; }
            public string GroupName { get; set; }
            //public List<SelectListItem> GROUPLIST { get; set; }
            //public string UserTypeID { get; set; }

            //public List<SelectListItem> PlanName { get; set; }
            public string PlanMasterID { get; set; }
            public string customerId { get; set; }

            public string lastName { get; set; }
            public string Assign { get; set; }
            public string[] users { get; set; }
            public string Users { get; set; }
            public string StatusCode { get; set; }
            public string Status { get; set; }
            public object ResponseContent { get; set; }
            public string CustomerId { get; set; }
            public string UserType { get; set; }
            public string SuperDistributorsName { get; set; }
            public string AdminUsersName { get; set; }
            public string DistributorsName { get; set; }
            public string AgentName { get; set; }
            public string distributorboth { get; set; }
            //public List<SelectListItem> AdminUsers { get; set; }
            //public List<SelectListItem> SuperDistributors { get; set; }
            //public List<SelectListItem> Distributors { get; set; }
            //public List<SelectListItem> Distributor_both { get; set; }
            //public List<SelectListItem> Agent { get; set; }
            public string FirstName { get; set; }
            public string firstName { get; set; }
            public string shopName { get; set; }
            public string ShopName { get; set; }

            public string mapusertype { get; set; }
            public string Features { get; set; }

        }
        public class UserTypes

        {
            public string GroupID { get; set; }
            public string GroupName { get; set; }
            public string groupname { get; set; }
            public string UserTypeID { get; set; }
            public string UserType { get; set; }
            public string PlanName { get; set; }
            public string PlanMasterID { get; set; }
            public string customerId { get; set; }
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string Assign { get; set; }
            public string[] users { get; set; }
            public string Users { get; set; }

        }

        //public class validation
        //{
        //    public string username { get; set; }
        //    public string password { get; set; }
        //    public string url { get; set; }
        //    public string patronId { get; set; }
        //    public string accesstoken { get; set; }
        //    public string requestId { get; set; }

        //}

        public class results
        {
            public string url { get; set; }
            public string requestId { get; set; }

            public string uid { get; set; }
        }
        public class Aadhaarobj
        {
            public Aadhaarobj()
            {
                responseContent = new results();
            }
            public string StatusCode { get; set; }
            public string Status { get; set; }
            public string authuid { get; set; }

            public string userId { get; set; }
            public results responseContent { get; set; }

        }




        public class GetAadharJsonResponse
        {
            public GetAadharJsonResponse()
            {
                result = new getresults();
            }
            public getresults result { get; set; }
            public string ReferId { get; set; }
            public string emailId { get; set; }
            public string aadhaarNo { get; set; }
            public string pancardNo { get; set; }
            public string gstNo { get; set; }
            public string flag { get; set; }
        }
        public class getresults
        {
            public getresults()
            {
                splitAddress = new splitAddr();
            }
            public string name { get; set; }
            public string dob { get; set; }
            public string gender { get; set; }
            public string address { get; set; }
            public string photo { get; set; }
            public string ReferId { get; set; }
            public string Emailid { get; set; }
            public string aadhaarNo { get; set; }
            public string pancardNo { get; set; }
            public string gstNo { get; set; }
            public splitAddr splitAddress { get; set; }

        }
        public class splitAddr
        {
            //public splitAddr()
            //{
            //    district = new List<districtArray>();
            //}
            public List<string> district { get; set; }
            public List<string> city { get; set; }
            public List<string> country { get; set; }
            public List<string[]> state { get; set; }
            public string pincode { get; set; }
        }


        public class GetJsonResponse
        {

            public string ReferId { get; set; }
            public string EmailId { get; set; }
            public string AadhaarNo { get; set; }
            public string PanNo { get; set; }
            public string GstNo { get; set; }
            public string GST_Detail { get; set; }
            public string Aadhaar_Details { get; set; }
            public string PAN_Details { get; set; }


        }
        public class MapusertypeRes1
        {
            public MapusertypeRes1()
            {
                ResponseContent = new List<UserTypes>();
            }
            public string StatusCode { get; set; }
            public string Status { get; set; }
            public List<UserTypes> ResponseContent { get; set; }



        }
        public class UserTypes1
        {

            public string GroupName { get; set; }
            public string PlanName { get; set; }

        }
        public class MapusertypeRes
        {
            public MapusertypeRes()
            {
                ResponseContent = new List<kyc>();
            }
            public string StatusCode { get; set; }
            public string Status { get; set; }

            public List<kyc> ResponseContent { get; set; }

        }

       
        public class kycResponse
        {
            public string StatusCode { get; set; }
            public string Status { get; set; }
            public object ResponseContent { get; set; }
        }
        public class userData
        {
            public userData()
            {
                result = new results();
            }
            public string id { get; set; }
            public string ttl { get; set; }
            public string created { get; set; }
            public string userId { get; set; }
            public results result { get; set; }

            //newdata
            public string request_id { get; set; }
        }
       
        public class users
        {
            public string usertype { get; set; }
            public string PlanName { get; set; }
            public string PlanID { get; set; }

        }
        public class mapuserplan
        {
            public string usertype { get; set; }
            public string usertypeid { get; set; }
            public string PlanID { get; set; }
            public string planname { get; set; }
            public string multiuser { get; set; }
            public string Users { get; set; }
            public string GroupId { get; set; }
            public string GroupName { get; set; }
            public string FeatureName { get; set; }
        }
        public class plan

        {
            public string SuperDistributorsName { get; set; }
            public string AdminUsersName { get; set; }
            public string DistributorsName { get; set; }
            public string AgentName { get; set; }

            public string FirstName { get; set; }

            public string mapusertype { get; set; }
            public string customerId { get; set; }

            public string State { get; set; }
            public string Country { get; set; }
            public string GroupName { get; set; }

            public string PlanMasterID { get; set; }
            public string PlandetailsID { get; set; }
            public string PlanName { get; set; }
            public string UserType { get; set; }
            public string FeatureName { get; set; }
            public string FeatureID { get; set; }
            public string ActiveUser { get; set; }
            public string ActiveFeature { get; set; }
            public string UserTypeName { get; set; }
            public string IsActive { get; set; }
            public string CreatedDate { get; set; }
            public string Users { get; set; }


        }
        public class createusermap
        {
            public string mapusercode { get; set; }
            public string mapusertype { get; set; }
            public string customerID { get; set; }
            public string usertype { get; set; }
            public string Users { get; set; }
        }
        public class UserRoleRes
        {
            public string featurename { get; set; }
            public string UserType { get; set; }
            public string CustomerId { get; set; }
            public string status { get; set; }

        }
        public class kyc
        {

            public string sno { get; set; }
            public string status { get; set; }
            public string name { get; set; }
            public string refId { get; set; }

            public string customerId { get; set; }

            public string shopNumber { get; set; }
            public string shopName { get; set; }
            public string password { get; set; }
            public string tpin { get; set; }

            public string addressLine1 { get; set; }
            public string addressLine2 { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public string pincode { get; set; }
            public string country { get; set; }
            public string gstNo { get; set; }
            public string gstSoftcopy { get; set; }
            public string gst_imgString { get; set; }
            public string gst_imgName { get; set; }
            public string imgPath { get; set; }
            public string pancardNo { get; set; }
            public string panSoftcopy { get; set; }
            public string pan_imgString { get; set; }
            public string pan_imgName { get; set; }
            public string aadhaarNo { get; set; }
            public string aadhaarfrontSoftcopy { get; set; }
            public string aadhaarfront_imgString { get; set; }
            public string aadharfront_imgName { get; set; }
            public string aadharbackSoftcopy { get; set; }
            public string aadharback_imgString { get; set; }
            public string aadhaarback_imgName { get; set; }
            public string mobileNo { get; set; }
            public string mobile2 { get; set; }
            public string emailId { get; set; }
            public string usertype { get; set; }
            public string mobOTP { get; set; }
            public string mailOTP { get; set; }
            public string ipAddress { get; set; }
            public string onboardType { get; set; }
            public string aesKey { get; set; }
            public string consumerkey { get; set; }
            public string consumersecret { get; set; }
            public string mapusertype { get; set; }
            public string mapUserCode { get; set; }
            public string uniqueId { get; set; }

        }
    public class KitAndUserData
    {
        public KitAndUserData()
        {
            kitDetails = new List<KitData>();
            UserDetails = new List<OnboardKycEkycModel>();
        }
 
        public List<KitData> kitDetails { get; set; }
        public List<OnboardKycEkycModel> UserDetails { get; set; }

    }
    public class KitData
    {
        public int KitId { get; set; }
        public string KitReferenceNumber { get; set; }
        public string CardNo { get; set; }
        public DateTime? CardExpiryDate { get; set; }
        public string CardType { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyAdminCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsAssigned { get; set; }
        public bool IsActivated { get; set; }
        public string CardCategory { get; set; }



    }
    public class getDetailRequest
        {
            public string merchantid { get; set; }
            public string fromdate { get; set; }
            public string todate { get; set; }
            public string flag { get; set; }
            public string ipaddress { get; set; }
            public string hash { get; set; }
        }
        public class sendLink
        {
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string emailId { get; set; }
            public string mobileNumber { get; set; }
            public string uniqueId { get; set; }
            public string mapUsercode { get; set; }
            public string supUsertype { get; set; }
        }

        public class datetime
        {
            public string merchantid { get; set; }
            public string fromdate { get; set; }
            public string todate { get; set; }
            public string status { get; set; }
            public string flag { get; set; }
            public string hash { get; set; }
            public string usertype { get; set; }
            public string customerId { get; set; }

        }

        public class dataOfUsers
        {


            public aadharResult result { get; set; }

        }
        public class aadharResult
        {
            public string user_full_name { get; set; }
            public string user_aadhaar_number { get; set; }
            public string user_dob { get; set; }
            public string address_zip { get; set; }
            public address user_address { get; set; }
        }
        public class address
        {
            public string country { get; set; }
            public string dist { get; set; }
            public string state { get; set; }
            public string house { get; set; }
            public string street { get; set; }
            public string loc { get; set; }
            public string po { get; set; }


        }
    }

