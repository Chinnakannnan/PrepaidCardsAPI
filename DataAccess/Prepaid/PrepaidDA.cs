using BusinessModel.Common;
using BusinessModel.KitMapping;
using BusinessModel.Prepaid;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Prepaid
{
    public class PrepaidDA : DapperRepository<object>, IPrepaidDA
    {
        public PrepaidDA(IDatabaseConfig databaseConfig) : base(databaseConfig)
        {

        }
        public string GetCustomerMobile(string CustomerID)
        {
            var dParam = new DynamicParameters();

            dParam.Add("@CustomerID", CustomerID);
            string result = QuerySP<string>("sp_GetCustomerMobile", dParam).FirstOrDefault();
            return result;
        }
        public StatusResponseModel InsertRegisterOTP(string response, string kitRefNo,string entityId)
        {
            try
            {
                var dParam = new DynamicParameters();

                dParam.Add("@Response", response);
                dParam.Add("@KitRefNo", kitRefNo);
                dParam.Add("@EntityID", entityId);
                StatusResponseModel result = QuerySP<StatusResponseModel>("sp_inserCustomerRegisterOTP", dParam).FirstOrDefault();
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
          
        }

        public StatusResponseModel InsertCustomerRegister(string response, string kitRefNo, string entityId, int resStatus)
        {
            try
            {
                var dParam = new DynamicParameters();
                dParam.Add("@Response", response);
                dParam.Add("@KitRefNo", kitRefNo);
                dParam.Add("@EntityID", entityId);
                dParam.Add("@ResStatus", resStatus);
                StatusResponseModel result = QuerySP<StatusResponseModel>("sp_inserCustomerrRegisterByTrancorp", dParam).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public UserCardMappingModel GetUserDetailsbyCustomerandKitRef(string customerID, string kitRefId)
        {
            var dParam = new DynamicParameters();
            dParam.Add("@CustomerID", customerID);
            dParam.Add("@KitRefNo", kitRefId);

            UserCardMappingModel result = QuerySP<UserCardMappingModel>("sp_GetUserDetailsbyCustomerandKitRef", dParam).FirstOrDefault();

            return result;
        }

    }
}
