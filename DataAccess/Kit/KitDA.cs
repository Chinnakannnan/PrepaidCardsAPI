using BusinessModel.Common;
using BusinessModel.Kit;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Kit
{
    public class KitDA : DapperRepository<object>, IKitDA
    {
        public KitDA(IDatabaseConfig databaseConfig) : base(databaseConfig)
        {

        }
        public StatusResponseModel CreateKitDetails(KitModel kitModel)
        {
            try
            {
                var dParam = new DynamicParameters();
                dParam.Add("@KitId", 0);
                dParam.Add("@KitReferenceNumber", kitModel.KitReferenceNumber);
                dParam.Add("@CardNo", kitModel.CardNo);
                dParam.Add("@CardExpiryDate", kitModel.CardExpiryDate);
                dParam.Add("@CardType", kitModel.CardType);
                dParam.Add("@CompanyCode", kitModel.CompanyCode);
                dParam.Add("@CompanyAdminCode", kitModel.CompanyAdminCode); ;
                dParam.Add("@IsActive", kitModel.IsActive);

                var result = QuerySP<StatusResponseModel>("sp_KitAddEdit", dParam).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
         ;
        }

        public StatusResponseModel UpdateKitDetails(KitModel kitModel)
        {
            try
            {
                var dParam = new DynamicParameters();
                dParam.Add("@KitId", 1);
                dParam.Add("@KitReferenceNumber", kitModel.KitReferenceNumber);
                dParam.Add("@CardNo", kitModel.CardNo);
                dParam.Add("@CardExpiryDate", kitModel.CardExpiryDate);
                dParam.Add("@CardType", kitModel.CardType);
                dParam.Add("@CompanyCode", kitModel.CompanyCode);
                dParam.Add("@CompanyAdminCode", kitModel.CompanyAdminCode); ;
                dParam.Add("@IsActive", kitModel.IsActive);

                var result = QuerySP<StatusResponseModel>("sp_KitAddEdit", dParam).FirstOrDefault();
                return result;

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public List<KitModel> GetKitDetailsById(int id)
        {
            var dParam = new DynamicParameters();
            dParam.Add("@KitId", id);

            List<KitModel> result = QuerySP<KitModel>("sp_GetKitDetailsbyId", dParam).ToList();

            return result;
        }
        public List<KitModel> GetKitDetails()
        {
            var dParam = new DynamicParameters();
            //dParam.Add("@KitId", id);

            List<KitModel> result = QuerySP<KitModel>("sp_GetKitDetails", null).ToList();

            return result;
        }
    }
}
