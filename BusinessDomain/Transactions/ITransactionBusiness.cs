using BusinessModel.TransactionModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.Transactions
{
    public interface ITransactionBusiness
    {

        List<PayoutHistoryDetails> GetTransaction(TransactionModel data);
    }
}
