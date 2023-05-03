using BusinessModel.Common;
using BusinessModel.Kit;
using DataAccess.Kit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.Kit
{
   public class KitBusiness : IKitBusiness
    {
        private readonly IKitDA _kitRepository;

        public KitBusiness(IKitDA kitRepositoryInstance)
        {
            _kitRepository = kitRepositoryInstance;
        }
        public StatusResponseModel CreateKitDetails(KitModel kitModel)
        {
            return _kitRepository.CreateKitDetails(kitModel);
        }
        public StatusResponseModel AddBulkKitDetails(Object dataset)
        {
            return _kitRepository.AddBulkKitDetails(dataset);
        }


       
        public StatusResponseModel UpdateKitDetails(KitModel kitModel)
        {
            return _kitRepository.UpdateKitDetails(kitModel);
        }
        public List<KitModel> GetKitDetailsById(int id)
        {
            return _kitRepository.GetKitDetailsById(id);
        }

        public List<KitModel> GetKitDetails()
        {
            return _kitRepository.GetKitDetails();
        }

    }
}
