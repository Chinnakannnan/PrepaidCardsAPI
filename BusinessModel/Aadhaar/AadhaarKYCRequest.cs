using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.Aadhaar
{
    public class AadhaarKYCRequest
    {
        public string RequestId { get; set; }
        public string OTP { get; set; }
    }
    public class OnboardAadhaarKYCRequest
    {
        public string RequestId { get; set; }
        public string OTP { get; set; }
        public string EmailId { get; set; }
        public string AadhaarNo { get; set; }
    }
}
