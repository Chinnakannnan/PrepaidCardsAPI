using BusinessModel.Common;
using BusinessModel.Wallet;


namespace DataAccess.Wallet
{
    public interface IWalletDA
    {
        StatusResponseModel LoadCustomerFund(LoadAPIRequest loadRequest);
        StatusResponseModel UpdateCustomerFund(LoadAPIRequest loadRequest, int status);
        StatusResponseModel ReplaceKitNo(CardReplacementRequest cardReplacementRequest);
        StatusResponseModel CardBlockRequest(BlockCardRequest blockCardRequest);
        AvailableCards AvailableNewCards();
        
        }
}
