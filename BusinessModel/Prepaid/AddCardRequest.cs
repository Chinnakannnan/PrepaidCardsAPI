using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.Prepaid
{
    public class AddCardRequest
    {
        public string entityId { get; set; }
        public string kitNo { get; set; }
        public string cardType { get; set; }
        public string business { get; set; }
    }
}
