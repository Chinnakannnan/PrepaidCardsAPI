using Autofac;
using BusinessDomain;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeoBank.API.Test
{
    public class ContainerResolver
    {
        public IContainer Container
        {
            get; set;
        }

        public ContainerResolver()
        {
            try
            {
                var containerBuilder = new ContainerBuilder();
                containerBuilder.RegisterModule(new BusinessModule());
                containerBuilder.RegisterModule(new RepositoryModule());
                Container = containerBuilder.Build();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public void Dispose()
        {
            Container.Dispose();
        }
    }
}
