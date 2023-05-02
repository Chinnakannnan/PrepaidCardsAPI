using BusinessModel.Common;
using BusinessModel.Prepaid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Prepaid
{
    public interface IPrepaidDA
    {
        public string GetCustomerMobile(string CustomerID);
        StatusResponseModel InsertRegisterOTP(string response, string kitRefNo, string entityId);

        StatusResponseModel InsertCustomerRegister(string response, string kitRefNo, string entityId,int resStatus);
        UserCardMappingModel GetUserDetailsbyCustomerandKitRef(string customerID, string kitRefId);
    }
}
