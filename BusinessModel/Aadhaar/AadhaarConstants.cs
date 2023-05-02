using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.Aadhaar
{
    public static class AadhaarConstants
    {
        public const string AadhaarBaseAddress = "https://test.zoop.one/";
        public const string OTPRequestURL = "in/identity/okyc/otp/request";
        public const string APIKey = "api-key";
        public const string APIKeyValue = "FJVTS74-RAAM91G-P2Y0DHG-3Z1QGW9";
        public const string AppId = "app-id";
        public const string AppIdValue = "62c7df712d4a96001d8dfb23";
        public const string ContentType = "Content-Type";
        public const string ContentTypeValue = "application/json; charset=utf-8";
        public const string Accept = "Accept";
        public const string AcceptValue = "application/json; charset=utf-8";
        public const string ApplicationJson = "application/json";

        public const string KYCRequestURL = "in/identity/okyc/otp/verify";

        public const string Consent = "consent";
        public const string ConsentValue = "Y";
        public const string ConsentText = "consent_text";
        public const string ConsentTextValue = "I hear by declare my consent agreement for fetching my information via ZOOP API.";


    }
}
