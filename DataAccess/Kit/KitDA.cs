using BusinessModel.Common;
using BusinessModel.Kit;
using Dapper;
using DocumentFormat.OpenXml.Spreadsheet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataAccess.Kit
{
    public class KitDA : DapperRepository<object>, IKitDA
    {
        public KitDA(IDatabaseConfig databaseConfig) : base(databaseConfig)
        {

        }
        
        public StatusResponseModel AddBulkKitDetails(Object dataSet)
        {
            StatusResponseModel objres = new StatusResponseModel();
            try
            {
                IList<KitModel> data = JsonConvert.DeserializeObject<IList<KitModel>>(dataSet.ToString());
                foreach (var item in data)
                {
                    var dParam = new DynamicParameters();
                    dParam.Add("@KitId", 0);
                    dParam.Add("@KitReferenceNumber", item.KitReferenceNumber);
                    dParam.Add("@CardNo", item.CardNo);
                    dParam.Add("@CardExpiryDate", item.CardExpiryDate);
                    dParam.Add("@CardType", item.CardType);
                    dParam.Add("@CompanyCode", item.CompanyCode);
                    dParam.Add("@CompanyAdminCode", item.CompanyAdminCode); ;
                    dParam.Add("@IsActive", item.IsActive);
                    var result = QuerySP<StatusResponseModel>("sp_KitAddEdit", dParam).FirstOrDefault();                  
                    objres.statuscode = result.statuscode;
                    objres.statusdesc = result.statusdesc;
                }               
                return objres;
            }
            catch (Exception ex)
            {
                throw ex;
            }

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
