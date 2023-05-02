using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.Prepaid
{
    public class UpdateCustomerResponse
    {
        public string result { get; set; }
        public PrepaidException Exception { get; set; }
        public string Pagination { get; set; }
    }
}
