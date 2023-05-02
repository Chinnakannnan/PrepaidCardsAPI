using BusinessDomain.Onboarding.OnboardKycEkyc;
using BusinessModel.TransactionModel;
using DataAccess.Onboarding.Onboarding_KycEkyc;
using DataAccess.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.Transactions
{


    public class TransactionBusiness : ITransactionBusiness
    {
        private readonly ITransactionDA _TransactionBusinessRepository;

        public TransactionBusiness(ITransactionDA TransactionInstance)
        {
            _TransactionBusinessRepository = TransactionInstance;
        }
        //public class TransactionBusiness
        //{
        //}

        public List<PayoutHistoryDetails> GetTransaction(TransactionModel data)
        {
            return _TransactionBusinessRepository.GetTransaction(data);
        }
    }
}
