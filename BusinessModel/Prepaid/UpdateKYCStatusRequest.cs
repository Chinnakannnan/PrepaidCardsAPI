using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.Prepaid
{
    public class UpdateKYCStatusRequest
    {
        public string entityId { get; set; }
        public string idType { get; set; }
        public string idNumber { get; set; }
        public string kycType { get; set; }
        public string registeredDate { get; set; }
        public string description { get; set; }
    }
}
