using BusinessModel.Common;
using BusinessModel.KitMapping;
using DataAccess.KitMapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.KitMapping
{
    public class KitMappingBusiness : IKitMappingBusiness
    {
        private readonly IKitMappingDA _kitMappingRepository;

        public KitMappingBusiness(IKitMappingDA kitMappingRepositoryInstance)
        {
            _kitMappingRepository = kitMappingRepositoryInstance;
        }
        public StatusResponseModel SaveKitMappingDetails(KitMappingModel kitMappingModel)
        {
            return _kitMappingRepository.SaveKitMappingDetails(kitMappingModel);
        }
        public StatusModel UpdateKitDetails(KitMappingModel kitMappingModel)
        {
            return _kitMappingRepository.UpdateKitDetails(kitMappingModel);
        }
        public List<KitMappingModel> GetKitMappingDetailsById(int id)
        {
            return _kitMappingRepository.GetKitMappingDetailsById(id);
        }
        public List<KitMappingForCustomer> GetAssignedKitMappingDetailsByCustomer(string customerId)
        {
            return _kitMappingRepository.GetAssignedKitMappingDetailsByCustomer(customerId);
        }
    }
}
