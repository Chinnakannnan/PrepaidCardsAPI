using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.Prepaid
{
    public class SetPinResponse
    {
        public SetPinResult result { get; set; }
        public PrepaidException Exception { get; set; }
        public string Pagination { get; set; }
    }

    public class SetPinResult
    {
        public string status { get; set; }
    }
}
