using BusinessModel.Common;
using BusinessModel.Kit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.Kit
{
    public interface IKitBusiness
    {
        StatusResponseModel CreateKitDetails(KitModel kitModel);
        StatusResponseModel AddBulkKitDetails(Object dataset);
        StatusResponseModel UpdateKitDetails(KitModel kitModel);
        List<KitModel> GetKitDetailsById(int id);
        List<KitModel> GetKitDetails();
    }
}
