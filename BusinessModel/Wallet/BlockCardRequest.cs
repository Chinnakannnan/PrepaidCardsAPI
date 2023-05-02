using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.Wallet
{
    public class BlockCardRequest
    {
        public string entityId { get; set; }
        public string flag { get; set; }
        public string kitNo { get; set; }
        public string reason { get; set; }
    }
}
