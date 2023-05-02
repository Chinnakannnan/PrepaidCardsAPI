using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.Wallet
{
    public class CardReplacementResponse
    {
        public string result { get; set; }
        public WalletException exception { get; set; }
        public string pagination { get; set; }
    }
}
