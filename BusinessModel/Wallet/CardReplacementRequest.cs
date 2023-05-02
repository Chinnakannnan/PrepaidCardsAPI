using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.Wallet
{
    public class CardReplacementRequest
    {
        public string entityId { get; set; }
        public string oldKitNo { get; set; }
        public string newKitNo { get; set; }
    }
}
