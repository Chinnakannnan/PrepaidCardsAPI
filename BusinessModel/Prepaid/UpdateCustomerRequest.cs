using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.Prepaid
{
    public class UpdateCustomerRequest
    {
        public string EntityId { get; set; }
        public string emailAddress { get; set; }
        //public string contactNo { get; set; }
        //public UpdateCustomerAddressDto AddressDto { get; set; }
    }

    public class UpdateCustomerAddressDto
    {
        public List<UpdateCustomerAddress> Address { get; set; }
    }
    public class UpdateCustomerAddress
    {
        public string Title { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PinCode { get; set; }
    }
}
