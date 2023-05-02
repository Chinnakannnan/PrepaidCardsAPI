using BusinessModel.Common;
using BusinessModel.KitMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.KitMapping
{
    public interface IKitMappingDA
    {
        StatusResponseModel SaveKitMappingDetails(KitMappingModel kitMappingModel);
        StatusModel UpdateKitDetails(KitMappingModel kitMappingModel);
        List<KitMappingModel> GetKitMappingDetailsById(int id);
        List<KitMappingForCustomer> GetAssignedKitMappingDetailsByCustomer(string customerId);
        List<KitMappingForCustomer> GetKitMappingDetailsByCusID(string id);
    }
}
