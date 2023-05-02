using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.Wallet
{
    public class RefundCustomerWalletRequest
    {
        public string toEntityId { get; set; }
        public string fromEntityId { get; set; }
        public string productId { get; set; }
        public string description { get; set; }
        public decimal amount { get; set; }
        public string transactionType { get; set; }
        public string transactionOrigin { get; set; }
        public string businessType { get; set; }
        public string businessEntityId { get; set; }
        public string externalTransactionId { get; set; }
    }

    public class RefundWalletRequest
    {
        public string fromEntityId { get; set; }
        public decimal amount { get; set; }
    }
}
