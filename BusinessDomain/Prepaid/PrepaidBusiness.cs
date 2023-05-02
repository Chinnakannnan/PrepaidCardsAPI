using APIService;
using BusinessModel.Prepaid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Prepaid;
using BusinessModel.Common;
using Newtonsoft.Json;
using Microsoft.VisualBasic;
using DataAccess.Kit;

namespace BusinessDomain.Prepaid
{
    public class PrepaidBusiness : IPrepaidBusiness
    {
        private readonly IPrepaidDA _prepaidRepository;
        public const string lstrFolderName = "Prepaid Cards";
        Log objLog = new Log();

        public PrepaidBusiness(IPrepaidDA prepaidRepositoryInstance)
        {
            _prepaidRepository = prepaidRepositoryInstance;
        }
        public StatusResponseModel SendMobOTP(PrepaidSendOTPRequest prepaidSendOTP)
        {
            try
            {
                StatusResponseModel respModel = new StatusResponseModel();
                string customeMobile = _prepaidRepository.GetCustomerMobile(prepaidSendOTP.CustomerId);
                prepaidSendOTP.mobileNumber = customeMobile;
                PrepaidCardProvider prepaidProvider = new PrepaidCardProvider();
                //var result = prepaidProvider.SendMobOTPByPlainJSON(prepaidSendOTP);
                var result = prepaidProvider.SendMobileOTPByEncry(prepaidSendOTP);
                string respSerlize = JsonConvert.SerializeObject(result);
                if (result.result is null)
                {
                    respModel = _prepaidRepository.InsertRegisterOTP(respSerlize, prepaidSendOTP.KitReferenceNumber, prepaidSendOTP.entityId);
                    respModel.statuscode = ResponseCode.Failed;
                    respModel.statusdesc = ResponseMsg.Failed;
                    return respModel;
                }
                respModel = _prepaidRepository.InsertRegisterOTP(respSerlize, prepaidSendOTP.KitReferenceNumber, prepaidSendOTP.entityId);
                objLog.WriteAppLog("Send OTP to Register Customer - Response  :" + result, lstrFolderName);
                return respModel;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public StatusResponseModel RegisterCustomerWithOTP(PrepaidSendOTPRequest prepaidModel)
        {
            try
            {
                StatusResponseModel respModel = new();
                PrepaidCardProvider prepaidProvider = new PrepaidCardProvider();
                string strReq = JsonConvert.SerializeObject(prepaidModel);
                objLog.WriteAppLog("Customer on boarding - Request from fronrend app  :" + strReq, lstrFolderName);
                PrepaidModel prepaidDataModel = new();
                UserCardMappingModel resultuserMapData = _prepaidRepository.GetUserDetailsbyCustomerandKitRef(prepaidModel.CustomerId, prepaidModel.KitReferenceNumber);
                objLog.WriteAppLog("Customer on boarding - Response  :" + "DB Data", lstrFolderName);

                prepaidDataModel.entityId = resultuserMapData.EntityID;
                prepaidDataModel.otp = prepaidModel.OTP;
                prepaidDataModel.channelName = PrepaidValues.ChannelName;
                prepaidDataModel.entityType = PrepaidValues.EntityType;
                prepaidDataModel.businessType = PrepaidValues.BusinessType;
                prepaidDataModel.businessId = resultuserMapData.mobileNo;
                prepaidDataModel.title = resultuserMapData.title;
                prepaidDataModel.firstName = resultuserMapData.firstName;
                prepaidDataModel.middleName = resultuserMapData.middleName;
                prepaidDataModel.lastName = resultuserMapData.lastName;
                prepaidDataModel.gender = resultuserMapData.gender;
                prepaidDataModel.isMinor = PrepaidValues.IsMinor;
                prepaidDataModel.isNRICustomer = PrepaidValues.IsNRICustomer;
                prepaidDataModel.isDependant = PrepaidValues.IsDependant;
                prepaidDataModel.maritalStatus = PrepaidValues.MaritalStatus;
                prepaidDataModel.countryCode = PrepaidValues.CountryCode;
                prepaidDataModel.employmentIndustry = PrepaidValues.EmploymentIndustry;
                prepaidDataModel.employmentType = PrepaidValues.EmploymentType;
                prepaidDataModel.plasticCode = PrepaidValues.PlasticCode;
                prepaidDataModel.kitInfo = new List<KitInfo>
            {
                new KitInfo()
                {
                    cardType=resultuserMapData.cardType,
                    kitNo=resultuserMapData.KitReferenceNumber,
                    cardCategory=resultuserMapData.cardCategory,
                    cardRegStatus=resultuserMapData.cardRegStatus,
                    aliasName=resultuserMapData.firstName +" "+ resultuserMapData.lastName,
                    fourthLine=PrepaidValues.FourthLine
                }
            };

                prepaidDataModel.addressInfo = new List<AddressInfo>
            {
                new AddressInfo()
                {
                    addressCategory=PrepaidValues.AddressCategory,
                    address1=resultuserMapData.address1,
                    address2=resultuserMapData.address2,
                    address3=resultuserMapData.address3,
                    city=resultuserMapData.city,
                    state=resultuserMapData.state,
                    country=resultuserMapData.country,
                    pinCode=resultuserMapData.pincode,

                },
                new AddressInfo()
                {
                    addressCategory=PrepaidValues.AddressCategory_COMMUNICATION,
                    address1=resultuserMapData.address1,
                    address2=resultuserMapData.address2,
                    address3=resultuserMapData.address3,
                    city=resultuserMapData.city,
                    state=resultuserMapData.state,
                    country=resultuserMapData.country,
                    pinCode=resultuserMapData.pincode,

                }

            };

                prepaidDataModel.communicationInfo = new List<CommunicationInfo> { new CommunicationInfo()
            {
                contactNo="+91"+resultuserMapData.mobileNo,
                notification=PrepaidValues.Notification,
                emailId=Crypto.AES_DECRYPT(resultuserMapData.emailAddress, COMMON.EMAILKEY)
            }


            };

                prepaidDataModel.kycInfo = new List<KycInfo>()
            {
                new KycInfo()
                {
                    documentType=PrepaidValues.DocumentType_PAN,
                    documentExpiry=PrepaidValues.DocumentExpiry,
                    documentNo=resultuserMapData.pan,
                }
            };

                prepaidDataModel.dateInfo = new List<DateInfo>()
            {
                new DateInfo()
                {
                    dateType = PrepaidValues.DateType,
                    date = PrepaidValues.Date,
                }

            };
                string stringContent = JsonConvert.SerializeObject(prepaidDataModel);
                objLog.WriteAppLog("Customer on boarding - Request extraction  :" + stringContent, lstrFolderName);
                var result = prepaidProvider.RegisterUserByEncry(prepaidDataModel);
                string respSerlize = JsonConvert.SerializeObject(result);
                if (result.result is null)
                {
                    respModel = _prepaidRepository.InsertCustomerRegister(respSerlize, prepaidModel.KitReferenceNumber, prepaidModel.entityId, 0);
                    if (result.Exception.errorCode == "K146")
                    {
                        respModel.statuscode = ResponseCode.Kit_Assigned_Already;
                        respModel.statusdesc = ResponseMsg.Kit_Assigned_Already;
                    }
                    else
                    {
                        respModel.statuscode = ResponseCode.Failed;
                        respModel.statusdesc = ResponseMsg.Failed;
                    }


                    return respModel;
                }
                respModel = _prepaidRepository.InsertCustomerRegister(respSerlize, prepaidModel.KitReferenceNumber, prepaidModel.entityId, 1);
                objLog.WriteAppLog("Send OTP to Register Customer - Response  :" + respSerlize, lstrFolderName);
                return respModel;

            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        public PrepaidResponse RegisterUser(PrepaidModel prepaidModel)
        {
            PrepaidCardProvider prepaidProvider = new PrepaidCardProvider();
            var result = prepaidProvider.RegisterUserPlainJSON(prepaidModel);
            return result;
        }

        public UpdateCustomerResponse UpdateCustomer(UpdateCustomerRequest updateCustomerRequest)
        {
            PrepaidCardProvider prepaidProvider = new PrepaidCardProvider();
            var result = prepaidProvider.UpdateCustomer(updateCustomerRequest);
            return result;
        }

        public CustomerTransactionLimitResponse CustomerTransactionLimit(CustomerDailyTransactionLimitRequest customerTransactionLimitRequest)
        {
            PrepaidCardProvider prepaidProvider = new PrepaidCardProvider();
            var result = prepaidProvider.CustomerTransactionLimit(customerTransactionLimitRequest);
            return result;
        }

        public AddCardResponse AddCard(AddCardRequest addCardRequest)
        {
            PrepaidCardProvider prepaidProvider = new PrepaidCardProvider();
            var result = prepaidProvider.AddCard(addCardRequest);
            return result;
        }

        public CustomerPreferencesExternalResponse CustomerPreferencesExternal(CustomerPreferencesExternalRequest customerPreferencesExternalRequest)
        {
            PrepaidCardProvider prepaidProvider = new PrepaidCardProvider();

            string jsonReq = JsonConvert.SerializeObject(customerPreferencesExternalRequest);
            objLog.WriteAppLog("Customer Preferences External - Request  :" + jsonReq, lstrFolderName);

            var result = prepaidProvider.CustomerPreferencesExternal(customerPreferencesExternalRequest);
            string jsonRes = JsonConvert.SerializeObject(result);

            objLog.WriteAppLog("Customer Preferences External - Response  :" + jsonRes, lstrFolderName);

            return result;
        }
        public FetchCustomerPreferencesResponse FetchCustomerPreferences(FetchCustomerPreferenceslRequest customerPreferencesExternalRequest)
        {
            PrepaidCardProvider prepaidProvider = new PrepaidCardProvider();

            string jsonReq = JsonConvert.SerializeObject(customerPreferencesExternalRequest);
            objLog.WriteAppLog("Customer Preferences External - Request  :" + jsonReq, lstrFolderName);

            var result = prepaidProvider.FetchCustomerPreferencesExternal(customerPreferencesExternalRequest);
            string jsonRes = JsonConvert.SerializeObject(result);

            objLog.WriteAppLog("Customer Preferences External - Response  :" + jsonRes, lstrFolderName);

            return result;
        }
        public UpdateKYCStatusResponse UpdateKYCStatus(UpdateKYCStatusRequest updateKYCStatusRequest)
        {
            PrepaidCardProvider prepaidProvider = new PrepaidCardProvider();

            string jsonReq = JsonConvert.SerializeObject(updateKYCStatusRequest);
            objLog.WriteAppLog("Update KYC Status - Request  :" + jsonReq, lstrFolderName);

            var result = prepaidProvider.UpdateKYCStatus(updateKYCStatusRequest);
            string jsonRes = JsonConvert.SerializeObject(result);

            objLog.WriteAppLog("Update KYC Status - Response  :" + jsonRes, lstrFolderName);

            return result;
        }

        public SetPinResponse SetPin(SetPinRequest setPinRequest)
        {
            PrepaidCardProvider prepaidProvider = new PrepaidCardProvider();

            string jsonReq = JsonConvert.SerializeObject(setPinRequest);
            objLog.WriteAppLog("SetPin - Request  :" + jsonReq, lstrFolderName);

            var result = prepaidProvider.SetPin(setPinRequest);
            string jsonRes = JsonConvert.SerializeObject(result);

            objLog.WriteAppLog("SetPin - Response  :" + jsonRes, lstrFolderName);

            return result;
        }
        public CardWidgetDetailsResonse FetchCardWidget(CardWidgetsRequest cardWidgetRequest)
        {
            PrepaidCardProvider prepaidProvider = new PrepaidCardProvider();
            return prepaidProvider.FetchCardWidget(cardWidgetRequest);

        }

        public CardWidgetDetailsResonse SetPinWidget(CardWidgetsRequest cardWidgetRequest)
        {
            PrepaidCardProvider prepaidProvider = new PrepaidCardProvider();
            return prepaidProvider.SetPinWidget(cardWidgetRequest);

        }


    }
}
