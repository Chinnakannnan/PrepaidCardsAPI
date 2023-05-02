using BusinessModel.Common;
using BusinessModel.GST_Validation;
using BusinessModel.OnBoarding.OnboardSupport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Onboarding.Onboarding_Support
{
   public interface IOnboardSupportDA
   {

        StatusModel VerifyUserdata(verifyUserData data);
        StatusModel CreateShop(OnboardSupportModel data);
        List<OnboardSupportModel> GotoRef(detailsOnb data);
        StatusModel SaveGSTDetails(detailsOnb data);
        StatusModel SavePANDetails(detailsOnb data);
        StatusModel GetUserDetails(getDetailRequest data);
        StatusModel InsertUserPlan(mapuserplan data);
         StatusModel Selectuserplan(createusermap data);
        StatusModel CreateUserMapping(createusermap data);
        StatusModel ListDist(createusermap data);
        StatusModel SuperDisgrp(createusermap data);
        StatusModel GET_GROUP1(plan data);
        StatusModel adminstatus(OnboardSupportModel data);
        StatusModel UpdUserRoleMap(UserRoleRes data);
        StatusModel OnBoardget(OnboardSupportModel data);
        StatusModel UpdRoleMap(UserRoleRes data);
        StatusModel UserRoleMap(UserRoleRes data);



   }
}
