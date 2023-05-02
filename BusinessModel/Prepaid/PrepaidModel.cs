using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BusinessModel.Prepaid
{
    public class PrepaidModel
    {

        public string entityId { get; set; }
        public string otp { get; set; }
        public string channelName { get; set; }
        public string entityType { get; set; }
        public string businessType { get; set; }
        public string businessId { get; set; }
        public string title { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string gender { get; set; }
        public bool isNRICustomer { get; set; }
        public bool isMinor { get; set; }
        public bool isDependant { get; set; }
        public string maritalStatus { get; set; }
        public string countryCode { get; set; }
        public string employmentIndustry { get; set; }
        public string employmentType { get; set; }
        public string plasticCode { get; set; }
        public List<KitInfo> kitInfo { get; set; }
        public List<AddressInfo> addressInfo { get; set; }
        public List<CommunicationInfo> communicationInfo { get; set; }
        public List<KycInfo> kycInfo { get; set; }
        public List<DateInfo> dateInfo { get; set; }
    }
    public class SendOtpMob
    {
        public string CompanyCode { get; set; }
        public string MerchantCode { get; set; }
        public string MerchantId { get; set; }
        public string CustomerId { get; set; }
        public string UserId { get; set; }
        public string MobileNo { get; set; }
        public string MobileOTP { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
    }

    public class PpiRegister
    {
        // public string entityId { get; set; }
        public string Otp { get; set; }
        //public string channelName { get; set; }
        //public string entityType { get; set; }
        //public string businessType { get; set; }
        // public string businessId { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string IsNRICustomer { get; set; }
        public string IsMinor { get; set; }
        public string IsDependant { get; set; }
        public string MaritalStatus { get; set; }
        public string CountryCode { get; set; }
        public string EmploymentIndustry { get; set; }
        public string EmploymentType { get; set; }
        public string PlasticCode { get; set; }

    }
    public class PrepaidSendOTPRequest
    {
        public string KitReferenceNumber { get; set; }
        public string entityId { get; set; }
        public string CustomerId { get; set; }
        public string mobileNumber { get; set; }
        public string OTP { get; set; }

    }

    public class PrepaidSendOTPRequestWithID
    {
        public string entityId { get; set; }
        public string mobileNumber { get; set; }

    }

    public class PrepaidEncrtptedData
    {
        public string body { get; set; }
        public string entity { get; set; }
        public string key { get; set; }
        public string refNo { get; set; }
        public string token { get; set; }

    }

    public class PrepaidResponse
    {
        public PrepaidSendOTPResultModel result { get; set; }
        public PrepaidException Exception { get; set; }
        public string Pagination { get; set; }
    }

    public class PrepaidException
    {
        public string detailMessage { get; set; }
        public string cause { get; set; }
        public string shortMessage { get; set; }
        public string languageCode { get; set; }
        public string errorCode { get; set; }
        //public string fieldErrors { get; set; }
        //public IList<string> fieldErrors { get; set; }
        public string[] fieldErrors { get; set; }
        public string message { get; set; }

        public string localizedMessage { get; set; }
        //public IList<string> suppressed { get; set; }
        public string[] suppressed { get; set; }
        //suppressed
        //suppressed
    }

    public class PrepaidSendOTPResultModel
    {
        public string Success { get; set; }
        public string EntityId { get; set; }

    }

    public class KitInfo
    {
        public string cardType { get; set; }
        public string kitNo { get; set; }
        public string cardCategory { get; set; }
        public string cardRegStatus { get; set; }
        public string aliasName { get; set; }
        public string fourthLine { get; set; }

    }
    public class AddressInfo
    {
        public string addressCategory { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string address3 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string pinCode { get; set; }
    }


    public class CommunicationInfo
    {
        public string contactNo { get; set; }
        public bool notification { get; set; }
        public string emailId { get; set; }


    }

    public class KycInfo
    {
        public string documentType { get; set; }
        public string documentNo { get; set; }
        public string documentExpiry { get; set; }
    }

    public class DateInfo
    {
        public string dateType { get; set; }
        public string date { get; set; }
    }


    public class UserCardMappingModel
    {
        public string uniquevalue { get; set; }
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
        public string addressProofType { get; set; }
        public string addressProofNumber { get; set; }
        public int status { get; set; }
        public int kycstatus { get; set; }
        public string EntityID { get; set; }


        public string cardType { get; set; }
        public string KitReferenceNumber { get; set; }
        public string cardCategory { get; set; }
        public string cardRegStatus { get; set; }
    }

}

