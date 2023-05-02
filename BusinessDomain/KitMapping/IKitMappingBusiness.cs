using BusinessModel.Common;
using BusinessModel.KitMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.KitMapping
{
    public interface IKitMappingBusiness
    {
        StatusResponseModel SaveKitMappingDetails(KitMappingModel kitMappingModel);
        StatusModel UpdateKitDetails(KitMappingModel kitMappingModel);
        List<KitMappingModel> GetKitMappingDetailsById(int id);
        List<KitMappingForCustomer> GetAssignedKitMappingDetailsByCustomer(string customerId);
    }
}
