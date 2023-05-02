using APIService;
using BusinessModel.Card;
using BusinessModel.Common;
using BusinessModel.Prepaid;
using BusinessModel.Wallet;
using Dapper;
using DataAccess.Prepaid;
using DataAccess.Wallet;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.Wallet
{
    public class WalletBusiness : IWalletBusiness
    {
        private readonly IWalletDA _prepaidRepository;
        public const string lstrFolderName = "Wallet";
        Log objLog = new Log();
        public WalletBusiness(IWalletDA prepaidRepositoryInstance)
        {
            _prepaidRepository = prepaidRepositoryInstance;
        }
        public StatusResponseModel LoadCustomerWalletByCard(LoadAPIRequest loadWalletRequest)
        {
            LoadWalletRequest walletRequest = new();
            StatusResponseModel respModel = new();
            WalletProvider walletProvider = new WalletProvider();
            // fetch date from table 
            string uniqRanNumber = "ACCU" + Utility.GetTransID().Substring(18 - 12);
            loadWalletRequest.OrderId = uniqRanNumber;
            StatusResponseModel resultWalletRequestData = _prepaidRepository.LoadCustomerFund(loadWalletRequest);
            if (resultWalletRequestData.statuscode == "000")
            {
                walletRequest.toEntityId = resultWalletRequestData.entityId;
                walletRequest.fromEntityId = CommonConstants.ToEntityId; 
                //walletRequest.fromEntityId = resultWalletRequestData.entityId;
                walletRequest.amount = loadWalletRequest.Amount;
                walletRequest.productId = WalletValues.ProductId;
                walletRequest.yapcode = WalletValues.yapcode;
                walletRequest.business = WalletValues.business;
                walletRequest.businessEntityId = WalletValues.businessEntityId;
                walletRequest.description = "LOAD";
                walletRequest.transactionOrigin = WalletValues.transactionOrigin;
                walletRequest.transactionType = WalletValues.transactionType;
                walletRequest.externalTransactionId = uniqRanNumber;

                var result = walletProvider.LoadCustomerWalletByEncry(walletRequest);
                //var result = walletProvider.LoadCustomerWalletByEncry(walletRequest);
                string respSerlize = JsonConvert.SerializeObject(result);
                if (result.result is null)
                {
                    respModel = _prepaidRepository.UpdateCustomerFund(loadWalletRequest, 0);
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
            }
            respModel = _prepaidRepository.UpdateCustomerFund(loadWalletRequest, 1);
            return respModel;

        }
        public LoadWalletResponse LoadCustomerWallet(LoadWalletRequest loadWalletRequest)
        {
            WalletProvider walletProvider = new WalletProvider();
            var result = walletProvider.LoadCustomerWallet(loadWalletRequest);
            return result;
        }

        public LoadWalletResponse RefundCustomerWallet(RefundWalletRequest refundWalletRequest)
        {
            string uniqRanNumber = Utility.GetTransID().Substring(18 - 12);
            WalletProvider walletProvider = new WalletProvider();
            RefundCustomerWalletRequest refundCustomerWalletRequest = new RefundCustomerWalletRequest();
            refundCustomerWalletRequest.toEntityId = CommonConstants.ToEntityId;
            refundCustomerWalletRequest.fromEntityId = refundWalletRequest.fromEntityId;
            refundCustomerWalletRequest.productId = CommonConstants.ProductId;
            refundCustomerWalletRequest.description = CommonConstants.Description;
            refundCustomerWalletRequest.amount = refundWalletRequest.amount;
            refundCustomerWalletRequest.transactionType = CommonConstants.TransactionType;
            refundCustomerWalletRequest.transactionOrigin = CommonConstants.TransactionOrigin;
            refundCustomerWalletRequest.businessType = CommonConstants.BusinessType;
            refundCustomerWalletRequest.businessEntityId = CommonConstants.BusinessEntityId;
            refundCustomerWalletRequest.externalTransactionId = uniqRanNumber;

            var result = walletProvider.RefundCustomerWallet(refundCustomerWalletRequest);
            return result;
        }


      
            public FetchBalanceResponse FetchCustomerBalance(string entityId)
        {
            WalletProvider walletProvider = new WalletProvider();
            var result = walletProvider.FetchCustomerBalance(entityId);
            return result;
        }

        public TransactionStatusResponse FetchTransactionStatus(string extTrxId)
        {
            WalletProvider walletProvider = new WalletProvider();
            var result = walletProvider.FetchTransactionStatus(extTrxId);
            return result;
        }

        public BlockCardResponse BlockCard(BlockCardRequest blockCardRequest)
        {
            WalletProvider walletProvider = new WalletProvider();
            var result = walletProvider.BlockCard(blockCardRequest);
            return result;
        }

        public List<TransactionModelResponse> FetchTransactionsByDates(FetchTransactionsbyDatesResult trnResult)
        {
            WalletProvider walletProvider = new WalletProvider();
            var result = walletProvider.FetchTransactionsByDates(trnResult);

            var queryResult = from c in result.result.Select(p => p.transaction).ToList()
                              select new TransactionModelResponse
                              {
                                  amount = c.amount,
                                  balance = c.balance,
                                  type = c.type,
                                  time = c.time,
                                  txRef = c.txRef,
                                  externalTransactionId = c.externalTransactionId,
                                  kitNo = c.kitNo
                              };
            return queryResult.ToList();
        }
        public CardReplacementResponse CardReplacement(CardReplacementRequest cardReplacementRequest)
        {
            WalletProvider walletProvider = new WalletProvider();
            var result = walletProvider.CardReplacement(cardReplacementRequest);
            if(string.IsNullOrEmpty(result.result))
            {               
                return result;
            }
            this._prepaidRepository.ReplaceKitNo(cardReplacementRequest);
            return result;
        }

        public FetchTransactionsResponse FetchTransactions(string extTrxId)
        {
            WalletProvider walletProvider = new WalletProvider();

            objLog.WriteAppLog("Fetch Transactions - Request  :" + extTrxId, lstrFolderName);

            var result = walletProvider.FetchTransactions(extTrxId);

            string jsonRes = JsonConvert.SerializeObject(result);
            objLog.WriteAppLog("Fetch Transactions - Response  :" + jsonRes, lstrFolderName);

            return result;
        }

        public MerchantPaymentResponse PaymentForMerchant(MerchantPaymentRequest merchantPaymentRequest)
        {
            WalletProvider walletProvider = new WalletProvider();

            string jsonReq = JsonConvert.SerializeObject(merchantPaymentRequest);
            objLog.WriteAppLog("Payment For Merchant - Request  :" + jsonReq, lstrFolderName);

            var result = walletProvider.PaymentForMerchant(merchantPaymentRequest);

            string jsonRes = JsonConvert.SerializeObject(result);
            objLog.WriteAppLog("Payment For Merchant - Response  :" + jsonRes, lstrFolderName);

            return result;
        }

        public CardModelResponse GetCardDetails(CardModelRequest cardDetails)
        {
            WalletProvider walletProvider = new WalletProvider();
            CardModelResponse respModel = new();
            var result = walletProvider.GetCardModelByEncry(cardDetails);
            //var result = walletProvider.LoadCustomerWalletByEncry(walletRequest);
            //string respSerlize = JsonConvert.SerializeObject(result);
            
            return result;
        }

        public CardCVVModelResponse GetCardCVVDetails(GetCVVCardModelRequest cardDetails)
        {
            WalletProvider walletProvider = new WalletProvider();
 
            var result = walletProvider.GetCardCVVModelByEncry(cardDetails);
            //var result = walletProvider.LoadCustomerWalletByEncry(walletRequest);
            //string respSerlize = JsonConvert.SerializeObject(result); 
            return result;
        }
    }
}
