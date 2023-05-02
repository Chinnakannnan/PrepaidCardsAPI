using APIService;
using BusinessModel.Aadhaar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.Aadhaar
{
    public class AadhaarBusiness : IAadhaarBusiness
    {
        public AadhaarBusiness()
        {

        }
        public AadhaarOTPResponse GenerateOTP(AadhaarOTPRequest aadhaarOTPRequest)
        {
            AadhaarProvider aadhaarProvider = new AadhaarProvider();
            var result = aadhaarProvider.GenerateOTP(aadhaarOTPRequest);
            return result;
        }

        public AadhaarKYCResponse KYCWithOTP(AadhaarKYCRequest aadhaarKYCRequest)
        {
            AadhaarProvider aadhaarProvider = new AadhaarProvider();
            var result = aadhaarProvider.KYCWithOTP(aadhaarKYCRequest);
            return result;
        }
    }

}
