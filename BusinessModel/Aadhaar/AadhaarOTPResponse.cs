using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.Aadhaar
{
    public class AadhaarOTPResponse
    {
        public string RequestId { get; set; }
        public bool Success { get; set; }
        public string ResponseMessage { get; set; }
    }
}
