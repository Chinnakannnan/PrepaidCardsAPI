using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.Aadhaar
{
    public class AadhaarOTPResponseModel
    {
        public string request_id { get; set; }
        public string task_id { get; set; }
        public string group_id { get; set; }
        public bool success { get; set; }
        public string response_code { get; set; }
        public string response_message { get; set; }
        public AadhaarOTPResultModel result { get; set; }
        public MetadataModel metadata { get; set; }
        public DateTime request_timestamp { get; set; }
        public DateTime response_timestamp { get; set; }
    }

    public class AadhaarOTPResultModel
    {
        public bool is_otp_sent { get; set; }
        public bool is_number_linked { get; set; }
        public bool is_aadhaar_valid { get; set; }

    }

    public class MetadataModel
    {
        public string billable { get; set; }
        public string reason_message { get; set; }
        
    }
}
