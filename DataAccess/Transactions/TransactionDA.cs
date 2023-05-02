using BusinessModel.OnBoarding.OnboardKycEkyc;
using BusinessModel.TransactionModel;
using Dapper;
using DataAccess.Onboarding.Onboarding_KycEkyc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Transactions
{

    class TransactionDA : DapperRepository<object>, ITransactionDA
    {

        public TransactionDA(IDatabaseConfig databaseConfig) : base(databaseConfig)
        {
     

        }
        public List<PayoutHistoryDetails> GetTransaction(TransactionModel data)
        {
            var dParam = new DynamicParameters();

            dParam.Add("@fromdate", data.fromdate);
            dParam.Add("@todate", data.todate);
            dParam.Add("@customerId", data.customerId);
            dParam.Add("@userType", data.userType);
            List<PayoutHistoryDetails> _userList = QuerySP<PayoutHistoryDetails>("SP_GetPPITransactionDetails", dParam).ToList();
            return _userList;
        }

    }
}
