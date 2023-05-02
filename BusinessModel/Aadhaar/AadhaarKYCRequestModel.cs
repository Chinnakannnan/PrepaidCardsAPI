using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.Aadhaar
{
    public class AadhaarKYCRequestModel
    {
        public AadhaarKYCData data { get; set; }

    }

    public class AadhaarKYCData
    {
        public string request_id { get; set; }
        public string otp { get; set; }
        public string consent { get; set; }
        public string consent_text { get; set; }
    }
}
