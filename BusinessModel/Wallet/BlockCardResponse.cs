using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.Wallet
{
    public class BlockCardResponse
    {
        public string result { get; set; }
        public WalletException exception { get; set; }
        public string pagination { get; set; }
    }
    public class BlockCardResult
    {
        public string Success { get; set; }
    }
}
