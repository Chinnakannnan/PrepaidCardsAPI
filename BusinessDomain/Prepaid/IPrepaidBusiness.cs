using BusinessModel.Common;
using BusinessModel.Prepaid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.Prepaid
{
    public interface IPrepaidBusiness
    {
        StatusResponseModel SendMobOTP(PrepaidSendOTPRequest prepaidSendOTP);
        PrepaidResponse RegisterUser(PrepaidModel prepaidModel);
        UpdateCustomerResponse UpdateCustomer(UpdateCustomerRequest updateCustomerRequest);
        CustomerTransactionLimitResponse CustomerTransactionLimit(CustomerDailyTransactionLimitRequest customerTransactionLimitRequest);
        AddCardResponse AddCard(AddCardRequest addCardRequest);
        StatusResponseModel RegisterCustomerWithOTP(PrepaidSendOTPRequest prepaidModel);
        CustomerPreferencesExternalResponse CustomerPreferencesExternal(CustomerPreferencesExternalRequest customerPreferencesExternalRequest);
        UpdateKYCStatusResponse UpdateKYCStatus(UpdateKYCStatusRequest updateKYCStatusRequest);
        SetPinResponse SetPin(SetPinRequest setPinRequest);
        FetchCustomerPreferencesResponse FetchCustomerPreferences(FetchCustomerPreferenceslRequest customerPreferencesExternalRequest);
        CardWidgetDetailsResonse FetchCardWidget(CardWidgetsRequest cardWidgetRequest);
        CardWidgetDetailsResonse SetPinWidget(CardWidgetsRequest cardWidgetRequest);


    }
}
