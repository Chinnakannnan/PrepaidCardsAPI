using BusinessModel.Common;
using BusinessModel.OnBoarding.OnboardKycEkyc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Onboarding.Onboarding_KycEkyc
{
    public interface IOnboardKycEkycDA
    {
        List<RegistrationRequest> GetUser();
        StatusResponseModel UserRegistration(RegistrationRequest data);
        StatusResponseModel UpdateStatus(RegistrationRequest data);
        StatusModel VerifyUserdataEkyc(CheckUserData data);
        StatusModel CreateShopEkyc(OnboardKycEkycModel data);
        List<OnboardKycEkycModel> GotoRefEkyc(detailsOnboard data);
        StatusModel SaveGSTDetailsEkyc(detailsOnboard data);
        StatusModel SavePANDetailsEkyc(detailsOnboard data);


        //need to create kyc data too
        StatusModel SendLink(sendLink data);
        StatusModel CheckUniqueId(sendLink data);
        StatusModel RegisterKYCdata(OnboardKycEkycModel data);
        StatusModel SaveAadhaarData(detailsOnboard data);
        StatusModel ApprovedData(sendLink data);
        StatusModel RejectedData(sendLink data);
        StatusModel WaitingForApproval(sendLink data);
    }
}
