using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.Wallet
{
    public class MerchantPaymentResponse
    {
        public MerchantPaymentResult result { get; set; }
        public WalletException Exception { get; set; }
        public string Pagination { get; set; }
    }

    public class MerchantPaymentResult
    {
        public string txId { get; set; }
        public string retrivalReferenceNo { get; set; }
        public string authCode { get; set; }
        public string action { get; set; }

    }


}
