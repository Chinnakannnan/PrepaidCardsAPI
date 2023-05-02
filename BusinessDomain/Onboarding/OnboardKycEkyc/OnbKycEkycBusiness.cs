using BusinessModel.Common;
using BusinessModel.OnBoarding.OnboardKycEkyc;
using DataAccess.Onboarding.Onboarding_KycEkyc;
using Microsoft.Extensions.Configuration;
using NeoBank.API.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.Onboarding.OnboardKycEkyc
{
    public class OnbKycEkycBusiness : IOnbKycEkycBusiness
    {
        private readonly IOnboardKycEkycDA _OnboardKycEkycRepository;
        private readonly IConfiguration _configuration;

        public OnbKycEkycBusiness(IOnboardKycEkycDA ONBKycEkycRepositoryInstance, IConfiguration configuration)
        {
            _OnboardKycEkycRepository = ONBKycEkycRepositoryInstance;
            _configuration = configuration; 
        }
    
        public List<RegistrationRequest> GetUser()
        {
            return _OnboardKycEkycRepository.GetUser();
        }
        public StatusResponseModel UserRegistration(RegistrationRequest data)
        {
            return _OnboardKycEkycRepository.UserRegistration(data);
        }
        // public string  UpdateStatus(RegistrationRequest data)
        public StatusResponseModel UpdateStatus(RegistrationRequest data)
     
        {
          
         var result= _OnboardKycEkycRepository.UpdateStatus(data);
            // var result= "hello";
            string imgUrl = _configuration.GetSection("ApplicationSettings").GetSection("IMAGEURLPATH").Value;
            string fromAddr = _configuration.GetSection("ApplicationSettings").GetSection("FROMMAILADDRESS").Value;
            string smtpAddr = _configuration.GetSection("ApplicationSettings").GetSection("SMTPADDRESS").Value;
            string smtpPort = _configuration.GetSection("ApplicationSettings").GetSection("SMTPPORT").Value;
            string mailSecret = _configuration.GetSection("ApplicationSettings").GetSection("FROMMAILSECRET").Value;
            //string smtpAddr = _configuration.GetSection("ApplicationSettings").GetSection("SMTPADDRESS").Value;

            string EmailId=Crypto.AES_ENCRYPT(data.emailAddress, COMMON.EMAILKEY);
            string Password =data.password;
            MailServices.UserApproval(EmailId, Password, fromAddr, smtpAddr, smtpPort, mailSecret, imgUrl);
         return result;
        }
        public StatusModel VerifyUserdataEkyc(CheckUserData data)
        {
            return _OnboardKycEkycRepository.VerifyUserdataEkyc(data);
        }
        public StatusModel CreateShopKycEkyc(OnboardKycEkycModel data)
        {
            return _OnboardKycEkycRepository.CreateShopEkyc(data);
        }
        public List<OnboardKycEkycModel> GotoRefKycEkyc(detailsOnboard data)
        {
            return _OnboardKycEkycRepository.GotoRefEkyc(data);
        }
        public StatusModel SaveGSTDetailsKycEkyc(detailsOnboard data)
        {
            return _OnboardKycEkycRepository.SaveGSTDetailsEkyc(data);
        }
        public StatusModel SavePANDetailsKycEkyc(detailsOnboard data)
        {
            return _OnboardKycEkycRepository.SavePANDetailsEkyc(data);
        }
        public StatusModel SendLink(sendLink data)
        {
            return _OnboardKycEkycRepository.SendLink(data);
        }

        public StatusModel CheckUniqueId(sendLink data)
        {
            return _OnboardKycEkycRepository.CheckUniqueId(data);
        }
        public StatusModel RegisterKYCdata(OnboardKycEkycModel data)
        {
            return _OnboardKycEkycRepository.RegisterKYCdata(data);
        }
        public StatusModel SaveAadhaarData(detailsOnboard data)
        {
            return _OnboardKycEkycRepository.SaveAadhaarData(data);
        } 
        public StatusModel ApprovedData(sendLink data)
        {
            return _OnboardKycEkycRepository.ApprovedData(data);
        }  
        public StatusModel RejectedData(sendLink data)
        {
            return _OnboardKycEkycRepository.RejectedData(data);
        } 
        public StatusModel WaitingForApproval(sendLink data)
        {
            return _OnboardKycEkycRepository.WaitingForApproval(data);
        }






    }
}
