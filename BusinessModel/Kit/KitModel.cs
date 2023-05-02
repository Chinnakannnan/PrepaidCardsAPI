using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.Kit
{
    public class KitModel
    {
        public int KitId { get; set; }
        public string KitReferenceNumber { get; set; }
		public string CardNo { get; set; }
		public DateTime? CardExpiryDate { get; set; }
		public string CardType { get; set; }
		public string CompanyCode { get; set; }
        public string CompanyAdminCode { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }
		public bool IsAssigned { get; set; }
		public bool IsActivated { get; set; }
		public string CardCategory { get; set; }
    }
}
