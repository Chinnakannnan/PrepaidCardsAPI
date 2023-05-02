using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessModel.Card;
using BusinessModel.Common;
using BusinessModel.Wallet;
using APIService;
using Newtonsoft.Json;
using DataAccess.Wallet;

namespace BusinessDomain.CardDetails
{
    public class CardDetails:ICardDetails
    {
        public const string lstrFolderName = "PPI Card";
        Log objLog = new Log();
        public CardDetails()
        {
        }
        public StatusResponseModel GetCardDetails(CardModelRequest cardDetails)
        {
            PPICard cardProvider = new();
            StatusResponseModel respModel = new();
            var result = cardProvider.GetCardModelByEncry(cardDetails);
            //var result = walletProvider.LoadCustomerWalletByEncry(walletRequest);
            string respSerlize = JsonConvert.SerializeObject(result);
            if (result.result is null)
            {
                if (result.Exception.errorCode == "Y104")
                {
                    respModel.statuscode = ResponseCode.Failed;
                    respModel.statusdesc = ResponseMsg.Failed;
                }
                else
                {
                    respModel.statuscode = ResponseCode.Failed;
                    respModel.statusdesc = ResponseMsg.Failed;
                }
                return respModel;
            }
            return respModel;
        }
    }
    
}
