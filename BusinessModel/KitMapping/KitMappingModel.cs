using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.KitMapping
{
    public class KitMappingModel
    {
        public int KitMappingId { get; set; }
        public string KitReferenceNumber { get; set; }
        public string CardType { get; set; }
        public string CardCategory { get; set; }
        public string EntityId { get; set; }
        public string CustomerId { get; set; }
        public string ProductId { get; set; }
        public DateTime DOB { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
        public bool IsActive { get; set; }
        public Decimal NonKyc_Dom_ATM_Limit { get; set; }
        public Decimal NonKyc_Dom_tab_Limit { get; set; }
        public Decimal NonKyc_Dom_Online_Limit { get; set; }
        public Decimal NonKyc_Dom_Outlet_Limit { get; set; }
        public Decimal Kyc_Dom_ATM_Limit { get; set; }
        public Decimal Kyc_Dom_tab_Limit { get; set; }
        public Decimal Kyc_Dom_Online_Limit { get; set; }
        public Decimal Kyc_Dom_Outlet_Limit { get; set; }
    }


    public class KitMappingForCustomer
    {
        public string KitMappingId { get; set; }
        public string KitReferenceNumber { get; set; }
        public string CardNo { get; set; }
        public string CardType { get; set; }
        public string EntityId { get; set; }
        public string CardStatus { get; set; }
        public DateTime DOB { get; set; }
    }
    public class KITMap
    {
        public string customerId { get; set; }
    }
}
