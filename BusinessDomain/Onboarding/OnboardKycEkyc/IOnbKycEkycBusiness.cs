using BusinessModel.Common;
using BusinessModel.OnBoarding.OnboardKycEkyc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.Onboarding.OnboardKycEkyc
{
    public interface IOnbKycEkycBusiness
    {
        StatusResponseModel UserRegistration(RegistrationRequest data);
        StatusResponseModel UpdateStatus(RegistrationRequest data);
       // string UpdateStatus(RegistrationRequest data);
        List<RegistrationRequest> GetUser();
        StatusModel VerifyUserdataEkyc(CheckUserData data);

        StatusModel CreateShopKycEkyc(OnboardKycEkycModel data);
        List<OnboardKycEkycModel> GotoRefKycEkyc(detailsOnboard data);
        StatusModel SaveGSTDetailsKycEkyc(detailsOnboard data);
        StatusModel SavePANDetailsKycEkyc(detailsOnboard data);

        // we need to create kyc part too

        StatusModel SendLink(sendLink data);
        StatusModel CheckUniqueId(sendLink data);
        StatusModel RegisterKYCdata(OnboardKycEkycModel data);
        StatusModel SaveAadhaarData(detailsOnboard data);
      //  StatusModel SaveAadhaarData(detailsOnboard data);
        StatusModel ApprovedData(sendLink data);
        StatusModel RejectedData(sendLink data);
        StatusModel WaitingForApproval (sendLink data);

    }
}
