using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.Aadhaar
{
    public class AadhaarKYCResponse
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public string ContactNumber { get; set; }
        public string AlternateContactNumber { get; set; }
        public string PermanentAddress { get; set; }
        public string SecondaryAddress { get; set; }
        public bool Success { get; set; }
        public string ResponseMessage { get; set; }
        public string ReasonMessage { get; set; }


    }
}
