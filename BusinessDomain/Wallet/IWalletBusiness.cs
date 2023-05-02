using BusinessModel.Card;
using BusinessModel.Common;
using BusinessModel.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.Wallet
{
    public interface IWalletBusiness
    {
        StatusResponseModel LoadCustomerWalletByCard(LoadAPIRequest loadWalletRequest);
        LoadWalletResponse LoadCustomerWallet(LoadWalletRequest loadWalletRequest);
        LoadWalletResponse RefundCustomerWallet(RefundWalletRequest refundWalletRequest);
        //LoadWalletResponse RefundCustomerWallet(RefundCustomerWalletRequest refundCustomerWalletRequest);
        FetchBalanceResponse FetchCustomerBalance(string entityId);
        TransactionStatusResponse FetchTransactionStatus(string extTrxId);
        BlockCardResponse BlockCard(BlockCardRequest blockCardRequest);
        CardReplacementResponse CardReplacement(CardReplacementRequest cardReplacementRequest);
        FetchTransactionsResponse FetchTransactions(string extTrxId);
        MerchantPaymentResponse PaymentForMerchant(MerchantPaymentRequest merchantPaymentRequest);
        CardModelResponse GetCardDetails(CardModelRequest cardDetails);
        CardCVVModelResponse GetCardCVVDetails(GetCVVCardModelRequest cardDetails);
        List<TransactionModelResponse> FetchTransactionsByDates(FetchTransactionsbyDatesResult trnResult);
    }
}
