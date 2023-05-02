using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessModel.Prepaid;

namespace BusinessModel.Card
{
    public class GetCVVCardModelRequest
    {
        public string entityId { get; set; }
        public string kitNo { get; set; }
        public string expiryDate { get; set; }
        public string dob { get; set; }
    }
    public class CardModelRequest
    {
        public string entityId { get; set; }
    }
    public class CardModelResponse
    {
        public CardModelReseult result { get; set; }
        public CardModelException Exception { get; set; }
        public string Pagination { get; set; }
    }
    public class CardModelReseult
    {
        //public string cvv { get; set; }
        public string[] cardList { get; set; }
        public string[] kitList { get; set; }
        public string[] expiryDateList { get; set; }
        public string[] cardStatusList { get; set; }
        public string[] cardTypeList { get; set; }
        public string[] networkTypeList { get; set; }
    }
    public class CardModelException
    {
        public string detailMessage { get; set; }
        public string cause { get; set; }
        public string shortMessage { get; set; }
        public string languageCode { get; set; }
        public string errorCode { get; set; }
        public string[] fieldErrors { get; set; }
        //suppressed
    }

    public class CardCVVModelResponse
    {
        public CardCVVModelRequest result { get; set; }
        public CardModelException Exception { get; set; }
        public string Pagination { get; set; }
    }
    public class CardCVVModelRequest
    {
        public string cvv { get; set; }
    }
}