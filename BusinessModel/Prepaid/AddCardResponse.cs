using BusinessModel.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.Prepaid
{
    public class AddCardResponse
    {
        public AddCardResult Result { get; set; }
        public WalletException Exception { get; set; }
        public string Pagination { get; set; }
    }
    public class AddCardResult
    {
        public string Success { get; set; }
    }
}
