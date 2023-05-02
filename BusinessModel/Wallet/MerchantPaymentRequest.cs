using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.Wallet
{
    public class MerchantPaymentRequest
    {
        public string merchantData { get; set; }
        public string fromEntityId { get; set; }
        public string toEntityId { get; set; }
        public string productId { get; set; }
        public string description { get; set; }
        public string amount { get; set; }
        public string transactionType { get; set; }
        public string transactionOrigin { get; set; }
        public string externalTransactionId { get; set; }
        public string businessType { get; set; }
        public string businessEntityId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string contactNo { get; set; }
        public string senderCardNo { get; set; }
        public string billRefNo { get; set; }
        public string additionalData { get; set; }
    }
}
