using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.Aadhaar
{
    public class AadhaarOTPRequestModel
    {
        public AadhaarOTPData data { get; set; }
    }

    public class AadhaarOTPData
    {
        public string customer_aadhaar_number { get; set; }
        public string consent { get; set; }
        public string consent_text { get; set; }
    }
}
