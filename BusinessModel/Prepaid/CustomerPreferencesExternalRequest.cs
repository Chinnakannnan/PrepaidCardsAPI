using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.Prepaid
{
    public class CustomerPreferencesExternalRequest
    {
        public string entityId { get; set; }
        public string status { get; set; }
        public string type { get; set; }
    }

    public class FetchCustomerPreferenceslRequest
    {
        public string entityId { get; set; }
    }
}
