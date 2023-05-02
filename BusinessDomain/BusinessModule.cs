
namespace BusinessDomain
{
    using Autofac;
    using BusinessDomain.Aadhaar;
    using BusinessDomain.Auth;
    using BusinessDomain.Company;
    using BusinessDomain.Kit;
    using BusinessDomain.KitMapping;
    using BusinessDomain.Login;
    using BusinessDomain.MobileOTP;
    using BusinessDomain.Onboarding.OnboardKycEkyc;
    using BusinessDomain.Onboarding.OnboardSupport;
    using BusinessDomain.PAN;
    using BusinessDomain.Prepaid;
    using BusinessDomain.Transactions;
    using BusinessDomain.User;
    using BusinessDomain.Wallet;
    using BusinessDomain.CardDetails;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class BusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            try
            {

                if (builder == null)
                {
                    throw new ArgumentNullException(nameof(builder));
                }

                // Register your service here
                builder.RegisterType<LoginBusiness>().AsImplementedInterfaces();
                builder.RegisterType<AuthBusiness>().AsImplementedInterfaces();
                builder.RegisterType<CompanyBusiness>().AsImplementedInterfaces();
                builder.RegisterType<UserBusiness>().AsImplementedInterfaces();
                builder.RegisterType<KitBusiness>().AsImplementedInterfaces();
                builder.RegisterType<AadhaarBusiness>().AsImplementedInterfaces();
                builder.RegisterType<PrepaidBusiness>().AsImplementedInterfaces();
                builder.RegisterType<KitMappingBusiness>().AsImplementedInterfaces();
                builder.RegisterType<OnbSupportBusiness>().AsImplementedInterfaces();
                builder.RegisterType<PanBusiness>().AsImplementedInterfaces();
                builder.RegisterType<MobileOTPBusiness>().AsImplementedInterfaces();
                builder.RegisterType<OnbKycEkycBusiness>().AsImplementedInterfaces();
                builder.RegisterType<WalletBusiness>().AsImplementedInterfaces();
                builder.RegisterType<TransactionBusiness>().AsImplementedInterfaces();
                //builder.RegisterType<CardDetails>().AsImplementedInterfaces();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
