using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessModel.Card;
using BusinessModel.Common;

namespace BusinessDomain.CardDetails
{
    public interface ICardDetails
    {
        StatusResponseModel GetCardDetails(CardModelRequest cardDetails);
    }
}
