using BusinessModel.TransactionModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Transactions
{
    public interface ITransactionDA
    {
        List<PayoutHistoryDetails> GetTransaction(TransactionModel data);



    }
}
