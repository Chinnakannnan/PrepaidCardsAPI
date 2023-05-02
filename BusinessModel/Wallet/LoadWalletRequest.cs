using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.Wallet
{
    public class LoadWalletRequest
    {
        public string toEntityId { get; set; }
        public string fromEntityId { get; set; }
        public decimal amount { get; set; }
        public string productId { get; set; }
        public string yapcode { get; set; }
        public string business { get; set; }
        public string businessEntityId { get; set; }
        public string description { get; set; }
        public string transactionOrigin { get; set; }
        public string transactionType { get; set; }
        public string externalTransactionId { get; set; }

    }

    public class LoadAPIRequest
    {
        public string CustomerId { get; set; }
        public string CardReferenceId { get; set; }
        public decimal Amount { get; set; }
        public string OrderId { get; set; }
        public string TrnType { get; set; }
    }
}
