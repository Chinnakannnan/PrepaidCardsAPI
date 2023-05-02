using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessModel.Common
{
    public static class CommonConstants
    {
        public const string YappayBaseAddress = "https://sit-secure.yappay.in/";
        public const string LoadWalletRequestURL = "Yappay/txn-manager/create";
        public const string ContentType = "Content-Type";
        public const string ContentTypeValue = "application/json; charset=utf-8";
        public const string Tenant = "TENANT";
        public const string M2PTenant = "M2P";
        
        public const string Authorization = "Authorization";
        public const string TenantValue = "M2B";
        public const string ApplicationJson = "application/json";

        public const string RefundCustomerWalletUrl = "Yappay/txn-manager/create/direct";
        public const string RefundCustomerTenantValue = "TCNXTIGN";

        public const string FetchCustomerBalanceUrl = "Yappay/business-entity-manager/fetchbalance";
        public const string GetCardDetailsRequestURL = "Yappay/business-entity-manager/getCardList";
        public const string GetCardDetailsV3RequestURL = "Yappay/business-entity-manager/v3/getCardList";
        public const string FetchTransactionStatusUrl = "Yappay/txn-manager/fetch";
        public const string GetCVVRequestURL = "Yappay/business-entity-manager/generateCVV";
        public const string BlockCardUrl = "Yappay/business-entity-manager/block";

        public const string CardReplacementUrl = "Yappay/business-entity-manager/replaceCard";

        public const string UpdateCustomerUrl = "Yappay/business-entity-manager/updateentity";

        public const string CustomerTransactionLimitUrl = "Yappay/business-entity-manager/setPreferences";
        public const string AddCardUrl = "Yappay/business-entity-manager/addCard";
        public const string M2BPublicCerticateURL = @"C:/certificates/m2p/m2psolutions_pub.der";
        public const string AccupaydPrivateCerticateURL = @"C:/certificates/accupayd/api.accupayd.com.pkcs8";
        public const string CustomerTenantValue = "TCNXTIGN";
        //public const string M2BPublicCerticateURL =@"D:/projects/old/m2psolutions_pub.der";
        //public const string AccupaydPrivateCerticateURL = @"D:/projects/certificate key/api.accupayd.com.pkcs8";
        
        public const string CustomerPreferencesExternalUrl = "Yappay/business-entity-manager/setPreferences";

        public const string UpdateKYCStatusUrl = "Yappay/business-entity-manager/addKycDetails";

        public const string FetchCardWidgetUrl = "Yappay/bitUrl/cardDetails";
        public const string FetchSetPinWidgetUrl = "Yappay/bitUrl/setPin";

        public const string SetPinUrl = "Yappay/business-entity-manager/setPin";

        public const string FetchTransactionsUrl = "Yappay/txn-manager/fetch/success/entity";

        public const string FetchTransactionsByDatesUrl = "Yappay/txn-manager/fetchTnxByEntityIdBetween";

        public const string PaymentForMerchant = "Yappay/payment-manager/payment";

        public const string CustomerPreferencesNewExternalUrl = "Yappay/business-entity-manager/updatePreferenceExternal";

        public const string FetchCustomerPreferencesNewUrl = "Yappay/business-entity-manager/fetchPreference";

        public const string ToEntityId = "TCNXTIGN01";
        public const string ProductId = "GENERAL";
        public const string Description = "Change";
        public const string TransactionType = "C2M";
        public const string TransactionOrigin = "MOBILE";
        public const string BusinessType = "TCNXTIGN";
        public const string BusinessEntityId = "TCNXTIGN";

        public const string AppGuid = "123dase";
        public const string Business = "TCNXTIGN";
        public const string CallBackUrl = "https://www.google.com";


    }

    public static class CommonStatusCode
    {
        public const string InvalidCode = "004";
    }
}
