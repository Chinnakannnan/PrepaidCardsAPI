using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.Wallet
{
    public class FetchTransactionsResponse
    {
        public List<FetchTransactionResult> result { get; set; }
        public WalletException exception { get; set; }
        public Pagination pagination { get; set; }
    }
    public class Pagination
    {
        public bool? isList { get; set; }
        public int? pageSize { get; set; }
        public int? pageNo { get; set; }
        public int? totalPages { get; set; }
        public int? totalElements { get; set; }
    }
    public class FetchTransactionModel
    {
        public decimal amount { get; set; }
        public decimal balance { get; set; }
        public string transactionType { get; set; }
        public string type { get; set; }
        public string time { get; set; }
        public string txRef { get; set; }
        public string businessId { get; set; }
        public string beneficiaryName { get; set; }
        public string beneficiaryType { get; set; }
        public string beneficiaryId { get; set; }
        public string description { get; set; }
        public string otherPartyName { get; set; }
        public string otherPartyId { get; set; }
        public string txnOrigin { get; set; }
        public string transactionStatus { get; set; }
        public string status { get; set; }
        public string yourWallet { get; set; }
        public string beneficiaryWallet { get; set; }
        public string externalTransactionId { get; set; }
        public string retrivalReferenceNo { get; set; }
        public string authCode { get; set; }
        public string billRefNo { get; set; }
        public string bankTid { get; set; }
        public string acquirerId { get; set; }
        public string mcc { get; set; }
        public string convertedAmount { get; set; }
        public string networkType { get; set; }
        public string limitCurrencyCode { get; set; }
        public string kitNo { get; set; }
        public string sorTxnId { get; set; }
        public string transactionCurrencyCode { get; set; }
        public string fxConvDetails { get; set; }
        public string convDetails { get; set; }
        public string disputedDto { get; set; }
        public string disputeRef { get; set; }
        public string accountNo { get; set; }
    }

    public class FetchTransactionsClientResponse
    {
        public List<TransactionModelResponse> result { get; set; }
        //public WalletException exception { get; set; }
        public WalletException exception { get; set; }
        public Pagination pagination { get; set; }
    }
    public class TransactionModelResponse
    {
        public decimal amount { get; set; }
        public decimal balance { get; set; }
        public string type { get; set; }
        public string time { get; set; }
        public string txRef { get; set; }
        public string externalTransactionId { get; set; }
        public string kitNo { get; set; }

    }

    public class FetchTransactionResult
    {
        public FetchTransactionModel transaction { get; set; }
        public string balance { get; set; }
    }

    public class FetchTransactionsbyDatesResult
    {
        public string entityId { get; set; }
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
    }
}
