using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.Aadhaar
{
    public class AadhaarKYCResponseModel
    {
        public string request_id { get; set; }
        public string task_id { get; set; }
        public string group_id { get; set; }
        public bool success { get; set; }
        public string response_code { get; set; }
        public string response_message { get; set; }
        public AadhaarKYCResultModel result { get; set; }
        public MetadataModel metadata { get; set; }
        public DateTime request_timestamp { get; set; }
        public DateTime response_timestamp { get; set; }
    }

    public class AadhaarKYCResultModel
    {
        public string user_full_name { get; set; }
        public string user_aadhaar_number { get; set; }
        public string user_dob { get; set; }
        public string user_gender { get; set; }
        public AadhaarUserAddress user_address { get; set; }
        public string address_zip { get; set; }
        public string user_profile_image { get; set; }
        public bool user_has_image { get; set; }
        public string aadhaar_xml_raw { get; set; }
        public string user_zip_data { get; set; }
        public string user_parent_name { get; set; }
        public string aadhaar_share_code { get; set; }
        public bool user_mobile_verified { get; set; }
        public string reference_id { get; set; }
    }
    public class AadhaarUserAddress
    {
        public string country { get; set; }
        public string dist { get; set; }
        public string state { get; set; }
        public string po { get; set; }
        public string loc { get; set; }
        public string vtc { get; set; }
        public string subdist { get; set; }
        public string street { get; set; }
        public string house { get; set; }
        public string landmark { get; set; }
    }
}
