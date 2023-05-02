using BusinessModel.Common;
using BusinessModel.KitMapping;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.KitMapping
{
    public class KitMappingDA : DapperRepository<object>, IKitMappingDA
    {
        public KitMappingDA(IDatabaseConfig databaseConfig) : base(databaseConfig)
        {

        }
        public StatusResponseModel SaveKitMappingDetails(KitMappingModel kitMappingModel)
        {
            var dParam = new DynamicParameters();
            //dParam.Add("@KitMappingId", kitMappingModel.KitMappingId);
            dParam.Add("@KitReferenceNumber", kitMappingModel.KitReferenceNumber);
            dParam.Add("@CustomerId", kitMappingModel.CustomerId);
            dParam.Add("@NonKyc_Dom_ATM_Limit", kitMappingModel.NonKyc_Dom_ATM_Limit);
            dParam.Add("@NonKyc_Dom_tab_Limit", kitMappingModel.NonKyc_Dom_tab_Limit);
            dParam.Add("@NonKyc_Dom_Online_Limit", kitMappingModel.NonKyc_Dom_Online_Limit);
            dParam.Add("@NonKyc_Dom_Outlet_Limit", kitMappingModel.NonKyc_Dom_Outlet_Limit);
            dParam.Add("@Kyc_Dom_ATM_Limit", kitMappingModel.Kyc_Dom_ATM_Limit);
            dParam.Add("@Kyc_Dom_tab_Limit", kitMappingModel.Kyc_Dom_tab_Limit);
            dParam.Add("@Kyc_Dom_Online_Limit", kitMappingModel.Kyc_Dom_Online_Limit);
            dParam.Add("@Kyc_Dom_Outlet_Limit", kitMappingModel.Kyc_Dom_Outlet_Limit);

            var result = QuerySP<StatusResponseModel>("sp_KitMappingAddEdit", dParam).FirstOrDefault();
            return result;
        }

        public StatusModel UpdateKitDetails(KitMappingModel kitMappingModel)
        {
            var dParam = new DynamicParameters();

            dParam.Add("@KitMappingId", kitMappingModel.KitMappingId);
            dParam.Add("@KitReferenceNumber", kitMappingModel.KitReferenceNumber);
            dParam.Add("@CardType", kitMappingModel.CardType);
            dParam.Add("@CardCategory", kitMappingModel.CardCategory);
            dParam.Add("@EntityId", kitMappingModel.EntityId);
            dParam.Add("@CustomerId", kitMappingModel.CustomerId);
            dParam.Add("@ProductId", kitMappingModel.ProductId);
            dParam.Add("@ModifiedDate", kitMappingModel.ModifiedDate);
            dParam.Add("@ModifiedBy", kitMappingModel.ModifiedBy);
            dParam.Add("@IsActive", kitMappingModel.IsActive);

            var result = QuerySP<StatusModel>("sp_KitMappingAddEdit", dParam).FirstOrDefault();
            return result;
        }

        public List<KitMappingForCustomer> GetKitMappingDetailsByCusID(string id)
        {
            var dParam = new DynamicParameters();
            dParam.Add("@customerId", id);

            List<KitMappingForCustomer> result = QuerySP<KitMappingForCustomer>("sp_get_kitdetails", dParam).ToList();

            return result;
        }
        public List<KitMappingModel> GetKitMappingDetailsById(int id)
        {
            var dParam = new DynamicParameters();
            dParam.Add("@KitMappingId", id);

            List<KitMappingModel> result = QuerySP<KitMappingModel>("sp_GetKitMappingDetailsbyId", dParam).ToList();

            return result;
        }
        public List<KitMappingForCustomer> GetAssignedKitMappingDetailsByCustomer(string customerId)
        {
            var dParam = new DynamicParameters();
            dParam.Add("@CustomerID", customerId);

            List<KitMappingForCustomer> result = QuerySP<KitMappingForCustomer>("sp_GetAssignedKitMappingDetailsbyUser", dParam).ToList();

            return result;
        }
    }
}
