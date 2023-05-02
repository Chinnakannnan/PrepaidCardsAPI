using Autofac;
using DataAccess.Auth;
using DataAccess.Company;
using DataAccess.Kit;
using DataAccess.KitMapping;
using DataAccess.Login;
using DataAccess.Onboarding.Onboarding_KycEkyc;
using DataAccess.Onboarding.Onboarding_Support;
using DataAccess.User;
using DataAccess.Wallet;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Prepaid;
using DataAccess.Transactions;

namespace DataAccess
{

    public class RepositoryModule : Module
    {
        private readonly IConfiguration _config;
        private readonly string _dbConnection;

        public RepositoryModule(IConfiguration configInstance)
        {
            _config = configInstance;
            _dbConnection = _config.GetConnectionString("DevDatabase");
        }
        public RepositoryModule()
        {
            var configurationBuilder = new ConfigurationBuilder();
            string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configurationBuilder.AddJsonFile(path, false);
            _dbConnection = configurationBuilder.Build().GetSection("ConnectionStrings:DevDatabase").Value;
        }

        protected override void Load(ContainerBuilder builder)
        {
            try
            {
                if (builder == null)
                {
                    string builderNull = "Repository Builder is Null";
                    throw new ArgumentNullException(builderNull);
                }

                // Register here your Repository Service                
                builder.RegisterType<LoginDA>().As<ILogInDA>().WithParameter("databaseConfig", new DatabaseConfig(_dbConnection));
                builder.RegisterType<AuthDA>().As<IAuthDA>().WithParameter("databaseConfig", new DatabaseConfig(_dbConnection));
                builder.RegisterType<CompanyDA>().As<ICompanyDA>().WithParameter("databaseConfig", new DatabaseConfig(_dbConnection));
                builder.RegisterType<UserDA>().As<IUserDA>().WithParameter("databaseConfig", new DatabaseConfig(_dbConnection));
                builder.RegisterType<KitDA>().As<IKitDA>().WithParameter("databaseConfig", new DatabaseConfig(_dbConnection));
                builder.RegisterType<KitMappingDA>().As<IKitMappingDA>().WithParameter("databaseConfig", new DatabaseConfig(_dbConnection));
                builder.RegisterType<OnboardSupportDA>().As<IOnboardSupportDA>().WithParameter("databaseConfig", new DatabaseConfig(_dbConnection));
                builder.RegisterType<OnboardKycEkycDA>().As<IOnboardKycEkycDA>().WithParameter("databaseConfig", new DatabaseConfig(_dbConnection));
                builder.RegisterType<PrepaidDA>().As<IPrepaidDA>().WithParameter("databaseConfig", new DatabaseConfig(_dbConnection));
                builder.RegisterType<WalletDA>().As<IWalletDA>().WithParameter("databaseConfig", new DatabaseConfig(_dbConnection));
                builder.RegisterType<TransactionDA>().As<ITransactionDA>().WithParameter("databaseConfig", new DatabaseConfig(_dbConnection));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
