using APIService;
using BusinessModel.MobileOTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.MobileOTP
{
   public class MobileOTPBusiness : IMobileOTPBusiness
    {
        public string SendOTPtoMobile(MobileOTPModel mobileData)
        {
            MobileOTPProvider MobileOTP= new MobileOTPProvider();
            var result = MobileOTP.SendOTPToMobile(mobileData);
            return result;
        }


    }
}
