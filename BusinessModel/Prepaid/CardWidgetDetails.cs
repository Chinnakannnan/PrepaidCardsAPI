using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.Prepaid
{
    public class CardWidgetsRequest
    {
        public string kitNo { get; set; }
        public string entityId { get; set; }
        public string dob { get; set; }
    }
    public class CardWidgetDetailsRequest
    {
        public string token { get; set; }
        public string kitNo { get; set; }
        public string entityId { get; set; }
        public string appGuid { get; set; }
        public string business { get; set; }
        public string callbackUrl { get; set; }
        public string dob { get; set; }
    }

    public class CardWidgetDetailsResonse
    {
        public string result { get; set; }
        public CardWidgetException Exception { get; set; }
        public string Pagination { get; set; }
    }
    public class CardWidgetException
    {
        public string detailMessage { get; set; }
        public string cause { get; set; }
        public string shortMessage { get; set; }
        public string languageCode { get; set; }
        public string errorCode { get; set; }
        public string[] fieldErrors { get; set; }
        public string message { get; set; }
        public string localizedMessage { get; set; }
        public string[] suppressed { get; set; }
    }
}
