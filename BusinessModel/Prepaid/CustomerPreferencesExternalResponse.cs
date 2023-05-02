using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.Prepaid
{
    public class CustomerPreferencesExternalResponse
    {
        public string result { get; set; }
        public PrepaidException Exception { get; set; }
        public string Pagination { get; set; }
    }

    public class FetchCustomerPreferencesResponse
    {
        public FetchCustomerPreferencesResult result { get; set; }
        public PrepaidException Exception { get; set; }
        public string Pagination { get; set; }
    }
    public class FetchCustomerPreferencesResult
    {
        public string atm { get; set; }
        public string pos { get; set; }
        public string ecom { get; set; }
        public string international { get; set; }
        public string dcc { get; set; }
        public string contactless { get; set; }
    }
}
