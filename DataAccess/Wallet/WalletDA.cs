using BusinessModel.Common;
using BusinessModel.KitMapping;
using BusinessModel.Prepaid;
using BusinessModel.Wallet;
using Dapper;
using DataAccess.Prepaid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessModel.Common;
using BusinessModel.Prepaid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Wallet
{
    public class WalletDA: DapperRepository<object>, IWalletDA
    {
        public WalletDA(IDatabaseConfig databaseConfig) : base(databaseConfig)
        {

        }
        public StatusResponseModel LoadCustomerFund(LoadAPIRequest loadRequest)
        {
            try
            {
                var dParam = new DynamicParameters();
                string uniqRanNumber = "DE"+Utility.GetTransID().Substring(18 - 12);
                dParam.Add("@customerid", loadRequest.CustomerId);
                dParam.Add("@KitRefNo", loadRequest.CardReferenceId);
                dParam.Add("@amount", loadRequest.Amount);
                dParam.Add("@mtransactionid", uniqRanNumber);
                dParam.Add("@apitransactionid", loadRequest.OrderId);
                dParam.Add("@orderId", loadRequest.OrderId);
                dParam.Add("@txntype", "DEBIT");
                dParam.Add("@txnmode", "LoadWalet");
                dParam.Add("@txnpaytype", 1);
                dParam.Add("@request", "");
                dParam.Add("@response", "");
                dParam.Add("@remarks", "");
                dParam.Add("@ipaddress", "");

                StatusResponseModel result = QuerySP<StatusResponseModel>("sp_insertcardtransaction", dParam).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public StatusResponseModel UpdateCustomerFund(LoadAPIRequest loadRequest,int status)
        {
            try
            {
                var dParam = new DynamicParameters();
                dParam.Add("@customerid", loadRequest.CustomerId);
                dParam.Add("@KitRefNo", loadRequest.CardReferenceId);
                dParam.Add("@amount", loadRequest.Amount);
                dParam.Add("@mtransactionid", loadRequest.OrderId);
                dParam.Add("@apitransactionid", loadRequest.OrderId);
                dParam.Add("@orderId", loadRequest.OrderId);
                dParam.Add("@txntype", "CREDIT");
                dParam.Add("@txnmode", "LoadWalet");
                dParam.Add("@txnpaytype", 1);
                dParam.Add("@request", "");
                dParam.Add("@response", "");
                dParam.Add("@remarks", "");
                dParam.Add("@ipaddress", "");
               dParam.Add("@status", status); 

                StatusResponseModel result = QuerySP<StatusResponseModel>("sp_upd_walletloadtrn", dParam).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public StatusResponseModel ReplaceKitNo(CardReplacementRequest cardReplacementRequest)
        {
            try
            {
                var dParam = new DynamicParameters();
                dParam.Add("@entityId", cardReplacementRequest.entityId);
                dParam.Add("@NewKit", cardReplacementRequest.newKitNo);
                dParam.Add("@OldKit", cardReplacementRequest.oldKitNo);           
                StatusResponseModel result = QuerySP<StatusResponseModel>("sp_ins_KitRepalcedDetails", dParam).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public AvailableCards AvailableNewCards()
        {
            try
            {
                var dParam = new DynamicParameters();
                AvailableCards result = QuerySP<AvailableCards>("sp_get_AvailableNewCards", dParam).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public StatusResponseModel CardBlockRequest(BlockCardRequest blockCardRequest)
        {
            try

            {
                var dParam = new DynamicParameters();
                dParam.Add("@entityId", blockCardRequest.entityId);
                dParam.Add("@flag", blockCardRequest.flag);
                dParam.Add("@kitNo ", blockCardRequest.kitNo);
                StatusResponseModel result = QuerySP<StatusResponseModel>("sp_ins_CardBlockRequest", dParam).FirstOrDefault();
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }

}
