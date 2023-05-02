using BusinessModel.Aadhaar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessDomain.Aadhaar
{
    public interface IAadhaarBusiness
    {
        AadhaarOTPResponse GenerateOTP(AadhaarOTPRequest aadhaarOTPRequest);
        AadhaarKYCResponse KYCWithOTP(AadhaarKYCRequest aadhaarKYCRequest);
    }
}
