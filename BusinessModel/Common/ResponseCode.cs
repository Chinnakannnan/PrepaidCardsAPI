using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BusinessModel.Common
{
   public class ResponseCode
    {
            public const string Success = "000";
            public const string Failed = "001";
            public const string Request_Empty = "002";
            public const string User_ID_AlreadyExists = "003";
            public const string Invalid_UserID = "004";
            public const string Invalid_MobileNo = "005";
            public const string MobileNo_Already_Exists = "006";
            public const string Invalid_EmailID = "007";
            public const string EmailID_Already_Exists = "008";
            public const string Invalid_GSTNO = "009";
            public const string GSTNO_Already_Exists = "010";
            public const string Invalid_PANNO = "011";
            public const string PANNO_Already_Exists = "012";
            public const string Please_Try_Again = "013";
            public const string Plese_Try_Again_DSNull = "014";
            public const string Plese_Try_Again_TableNull = "015";
            public const string Invalid_Response = "016";
            public const string Activation_Pending = "017";
            public const string User_Blocked = "018";
            public const string Account_Blocked = "019";
            public const string Invalid_Password = "020";
            public const string User_Inactive = "021";
            public const string Invalid_HeaderValue = "022";
            public const string Invalid_ApiKey = "023";
            public const string Invalid_Decrypt_request = "024";
            public const string Invalid_Hash = "025";
            public const string Invalid_OTP = "026";
            public const string Invalid_NXT_TransactionID = "027";
            public const string Invalid_TransactionID = "028";
            public const string Invalid_Amount = "029";
            public const string Insufficient_Balance = "030";
            public const string Invalid_Paytype = "031";
            public const string Transaction_Pending = "032";
            public const string Transaction_Failed = "033";
            public const string Transaction_Already_Processed = "034";
            public const string Transaction_Already_Reversed = "035";
            public const string OTP_Already_Verified = "036";
            public const string Invalid_OTP_Status = "037";
            public const string OTP_Expired = "038";
            public const string Remitter_Already_Exists = "039";
            public const string TransactionID_Already_Exists = "040";
            public const string Invalid_Status = "041";
            public const string Invalid_UserType = "042";
            public const string Invalid_Remarks = "043";
            public const string Invalid_PaymentID = "044";
            public const string Transaction_Refunded = "045";
            public const string User_Exist = "046";
            public const string Kit_Assigned_Already = "K146";
            public const string InvalidKitOrEmpty = "K148";
            public const string Invalid_ClinetID_Secrect = "K004";
    }

        public class ResponseMsg
        {
            public const string Success = "Success";
            public const string Failed = "Please Try Again";
            public const string Request_Empty = "Request Empty";
            public const string User_ID_AlreadyExists = "User ID Already Exists";
            public const string Invalid_UserID = "Invalid User ID";
            public const string Invalid_MobileNo = "Invalid Mobile Number";
            public const string MobileNo_Already_Exists = "Mobile Number Already Exists";
            public const string Invalid_EmailID = "Invalid Email ID";
            public const string EmailID_Already_Exists = "Email ID Already Exists";
            public const string Invalid_GSTNO = "Invalid GST Number";
            public const string GSTNO_Already_Exists = "GST Number Already Exists";
            public const string Invalid_PANNO = "Invalid PAN Number";
            public const string PANNO_Already_Exists = "PAN Number Already Exists";
            public const string Please_Try_Again = "Please Try Again";
            public const string Plese_Try_Again_DSNull = "Please Try Again";
            public const string Plese_Try_Again_TableNull = "Please Try Again";
            public const string Invalid_Response = "Invalid Response";
            public const string Activation_Pending = "Activation Pending";
            public const string User_Blocked = "User Blocked";
            public const string Account_Blocked = "Account Blocked";
            public const string Invalid_Password = "Invalid Password";
            public const string User_Inactive = "User Inactive";
            public const string Invalid_HeaderValue = "Invalid Header Value";
            public const string Invalid_ApiKey = "Invalid API Key";
            public const string Invalid_Decrypt_request = "Invalid Decrypt Request";
            public const string Invalid_Hash = "Invalid Hash";
            public const string Invalid_OTP = "Invalid OTP";
            public const string Invalid_NXT_TransactionID = "Invalid NXT TransactionID";
            public const string Invalid_TransactionID = "Invalid TransactionID";
            public const string Invalid_Amount = "Invalid Amount";
            public const string Insufficient_Balance = "Insufficient Balance";
            public const string Invalid_Paytype = "Invalid PayType";
            public const string Transaction_Pending = "Transaction Pending";
            public const string Transaction_Failed = "Transaction Failed";
            public const string Transaction_Already_Processed = "Transaction Already Processed";
            public const string Transaction_Already_Reversed = "Transaction Already Reversed";
            public const string OTP_Already_Verified = "OTP Already Verified";
            public const string Invalid_OTP_Status = "Invalid OTP Status";
            public const string OTP_Expired = "OTP Expired";
            public const string Remitter_Already_Exists = "Remitter Already Exists";
            public const string TransactionID_Already_Exists = "TransactionID Already Exists";
            public const string Invalid_Status = "Invalid Status";
            public const string Invalid_UserType = "Invalid UserType";
            public const string Invalid_Remarks = "Invalid Remarks";
            public const string Invalid_PaymentID = "Invalid PaymentID";
            public const string Transaction_Refunded = "Transaction Refunded";
            public const string User_Exist = "User Already Exists.";
            public const string Kit_Assigned_Already = "Kit Assigned Already";
            public const string InvalidKitOrEmpty = "Please insert KitNo / Card Ending No";
            public const string Invalid_ClinetID_Secrect = "Invalid Client / Secrect";
    }
    }

