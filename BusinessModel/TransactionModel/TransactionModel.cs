using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.TransactionModel
{
    public class TransactionModel
    {
        public string fromdate { get; set; }
        public string todate { get; set; }
        public string customerId { get; set; }
        public string userType { get; set; }






    }
    public class GetStatus
    {
        public string fromdate { get; set; }
        public string todate { get; set; }
        public string status { get; set; }
        public Int32? TranStatusId { get; set; }
        public string merchantid { get; set; }
        public string CRNNo { get; set; }
        public string UTRNo { get; set; }
        public string RequestData { get; set; }
        public string ResponseData { get; set; }
        public string ResponseStatus { get; set; }
        public string StatusDescription { get; set; }
        public string BatchNo { get; set; }
        public string TransactionStatus { get; set; }
        public DateTime? cdatetime { get; set; }
        public string ProcessingDate { get; set; }
        public string CorpCode { get; set; }
        public string ResponseCode { get; set; }
        public string Message { get; set; }
        public bool? IsDone { get; set; }
        public bool? IsProcessing { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsActive { get; set; }
    }
    public class PayoutHistoryDetails
    {


        public string customerId { get; set; }
        public string credit { get; set; }
        public string debit { get; set; }
        public string description { get; set; }
        public string balance { get; set; }
        public string openingBalance { get; set; }
        public string closingBalance { get; set; }
        public string cdatetime { get; set; }

        public string transactionAmount { get; set; }




    }
}
