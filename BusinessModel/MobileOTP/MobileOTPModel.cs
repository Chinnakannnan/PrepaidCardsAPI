using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.MobileOTP
{
    public class MobileOTPModel
    {
       
        public string mobileNo { get; set; }
        public string mobileOTP { get; set; }
        public string emailAddress { get; set; }
    }
    public static class MOBILEOTP
    {
        public const string BaseURL = "https://api.itextos.com/genericapi/";
        public const string UserDetail = "MQSRequestReceiver?username=ACCUPAYDTXN&password=q6TVEIswNBuK&to=";
        public const string TEMPLATE1 = "&from=ACUPYD&content=";
        public const string TEMPLATE2 = "Dear Customer,";
        public const string TEMPLATE3 = "is your ACCUPAYD TECH - Transcorp  Card OTP. Don't share the OTP with anyone. - Team ACCUPAYD TECH";

        public const string ContentType = "Content-Type";
        public const string ContentTypeValue = "application/json; charset=utf-8";
        public const string Accept = "Accept";
        public const string AcceptValue = "application/json; charset=utf-8";
        public const string ApplicationJson = "application/json";
    }
}
