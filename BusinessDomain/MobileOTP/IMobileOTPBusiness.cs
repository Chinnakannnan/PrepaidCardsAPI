using BusinessModel.MobileOTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.MobileOTP
{
   public interface IMobileOTPBusiness
    {

        public string SendOTPtoMobile(MobileOTPModel mobileData);

    }
}
