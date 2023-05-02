using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.Prepaid
{
    public class SetPinRequest
    {
        public string entityId { get; set; }
        public string pin { get; set; }
        public string kitNo { get; set; }
        public string expiryDate { get; set; }
        public string dob { get; set; }
    }

    public class CVVPINWidget
    {
        public string entityId { get; set; }
        public string kitNo { get; set; }
        public string dob { get; set; }
    }
}
