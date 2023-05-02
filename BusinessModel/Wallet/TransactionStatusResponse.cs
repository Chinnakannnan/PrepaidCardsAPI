using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.Wallet
{
    public class TransactionStatusResponse
    {
        public TransactionStatusResult Result { get; set; }
        public WalletException Exception { get; set; }
        public string Pagination { get; set; }
    }

    public class TransactionStatusModel
    {
        public double Amount { get; set; }
        public double Balance { get; set; }
        public string TransactionType { get; set; }
        public string Type { get; set; }
        public string Time { get; set; }
        public string TxRef { get; set; }
        public string BusinessId { get; set; }
        public string BeneficiaryName { get; set; }
        public string BeneficiaryType { get; set; }
        public string BeneficiaryId { get; set; }
        public string Description { get; set; }
        public string OtherPartyName { get; set; }
        public string OtherPartyId { get; set; }
        public string TxnOrigin { get; set; }
        public string TransactionStatus { get; set; }
        public string Status { get; set; }
        public string YourWallet { get; set; }
        public string BeneficiaryWallet { get; set; }
        public string ExternalTransactionId { get; set; }
        public string RetrivalReferenceNo { get; set; }
        public string AuthCode { get; set; }
        public string BillRefNo { get; set; }
        public string BankTid { get; set; }
    }

    public class TransactionStatusResult
    {
        public TransactionStatusModel Transaction { get; set; }
        public string Balance { get; set; }
    }
}
