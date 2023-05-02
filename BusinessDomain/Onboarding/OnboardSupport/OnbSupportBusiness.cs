using BusinessModel.Common;
using BusinessModel.OnBoarding.OnboardSupport;
using DataAccess.Onboarding.Onboarding_Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.Onboarding.OnboardSupport
{
    public class OnbSupportBusiness : IOnbSupportBusiness
    {

        private readonly IOnboardSupportDA _OnboardRepository;

        public OnbSupportBusiness(IOnboardSupportDA ONBRepositoryInstance)
        {
            _OnboardRepository = ONBRepositoryInstance;
        }


        public StatusModel VerifyUserdata(verifyUserData data)
        {
            return _OnboardRepository.VerifyUserdata(data);
        }
        public StatusModel CreateShop(OnboardSupportModel data)
        {
            return _OnboardRepository.CreateShop(data);
        }
        public List<OnboardSupportModel> GotoRef(detailsOnb data)
        {
         
            return _OnboardRepository.GotoRef(data);
        }
        public StatusModel SaveGSTDetails(detailsOnb data)
        {
            return _OnboardRepository.SaveGSTDetails(data);
        }
        public StatusModel SavePANDetails(detailsOnb data)
        {
            return _OnboardRepository.SavePANDetails(data);
        }
        public StatusModel GetUserDetails(getDetailRequest data)
        {
            return _OnboardRepository.GetUserDetails(data);
        } 
        public StatusModel InsertUserPlan(mapuserplan data)
        {
            return _OnboardRepository.InsertUserPlan(data);
        }
        public StatusModel Selectuserplan(createusermap data)
        {
            return _OnboardRepository.Selectuserplan(data);
        }
        public StatusModel CreateUserMapping(createusermap data)
        {
            return _OnboardRepository.CreateUserMapping(data);
        } 
        public StatusModel ListDist(createusermap data)
        {
            return _OnboardRepository.ListDist(data);
        }
        
        public StatusModel SuperDisgrp(createusermap data)
        {
            return _OnboardRepository.SuperDisgrp(data);
        }
           
        public StatusModel GET_GROUP1(plan data)
        {
            return _OnboardRepository.GET_GROUP1(data);
        }
        public StatusModel adminstatus(OnboardSupportModel data)
        {
            return _OnboardRepository.adminstatus(data);
        } 
        public StatusModel UpdUserRoleMap(UserRoleRes data)
        {
            return _OnboardRepository.UpdUserRoleMap(data);
        } 
        public StatusModel OnBoardget(OnboardSupportModel data)
        {
            return _OnboardRepository.OnBoardget(data);
        } 
        public StatusModel UpdRoleMap(UserRoleRes data)
        {
            return _OnboardRepository.UpdRoleMap(data);
        }
           public StatusModel UserRoleMap(UserRoleRes data)
        {
            return _OnboardRepository.UpdRoleMap(data);
        }






    }

}

