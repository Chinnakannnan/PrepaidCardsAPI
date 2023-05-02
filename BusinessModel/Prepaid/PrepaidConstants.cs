using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.Prepaid
{
    public static class PrepaidConstants
    {
        public const string PrepaidBaseAddress = "https://kycuat.yappay.in/";
        public const string PrepaidBaseAddressByEncry = "https://ssltest.yappay.in/";
        public const string PrepaidBaseAddressByDirect= "https://sit-secure.yappay.in/";
        public const string OTPRequestURL = "kyc/customer/generate/otp";
        public const string Authorization = "Authorization";
        public const string Tenant = "TENANT";
        public const string PartnerId = "partnerId";
        public const string TenantValue = "TCNXTIGN";
        public const string PartnerToken = "partnerToken";
        public const string PartnerTokenValue = "Basic VENOWFRJR04=";
        public const string ContentType = "Content-Type";
        public const string ContentTypeValue = "application/json; charset=utf-8";
        public const string Accept = "Accept";
        public const string AcceptValue = "application/json; charset=utf-8";
        public const string ApplicationJson = "application/json";

        public const string UserRegisterURL = "kyc/v2/register";
    }
}
