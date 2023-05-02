using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.Prepaid
{
    public class CustomerDailyTransactionLimitRequest
    {
        public string entityId { get; set; }
        public LimitConfigModel limitConfig { get; set; }
    }

    public class CustomerTransactionLimitRequest
    {
        public string entityId { get; set; }
        public bool contactless { get; set; }
        public bool atm { get; set; }
        public bool pos { get; set; }
        public bool ecom { get; set; }
        public LimitConfigModel limitConfig { get; set; }
    }
    public class LimitConfigModel
    {
        public string txnType { get; set; }
        public string dailyLimitValue { get; set; }
    }
}
