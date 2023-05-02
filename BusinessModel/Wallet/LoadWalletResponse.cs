using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.Wallet
{
    public class LoadWalletResponse
    {
        public LoadWalletTransaction result { get; set; }
        public WalletException Exception { get; set; }
        public string pagination { get; set; }
    }
    public class WalletException
    {
        public string detailMessage { get; set; }
        public string cause { get; set; }
        public string shortMessage { get; set; }
        public string languageCode { get; set; }
        public string errorCode { get; set; }
        //public string fieldErrors { get; set; }
        //public IList<string> fieldErrors { get; set; }
        public string[] fieldErrors { get; set; }
        public string message { get; set; }

        public string localizedMessage { get; set; }
        //public IList<string> suppressed { get; set; }
        public string[] suppressed { get; set; }
        //suppressed
    }

    public class LoadWalletTransaction
    {
        public string txId { get; set; }
    }


}
