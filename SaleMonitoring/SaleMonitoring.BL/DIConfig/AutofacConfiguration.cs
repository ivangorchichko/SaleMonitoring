using System.Runtime.CompilerServices;
using Autofac;
using SaleMonitoring.BL.Contacts;
using SaleMonitoring.BL.Logger;
using SaleMonitoring.BL.Service;
using SaleMonitoring.DAL.Repository.Contract;
using SaleMonitoring.DAL.Repository.Model;
using IContainer = System.ComponentModel.IContainer;

namespace SaleMonitoring.BL.DIConfig
{
    public static class AutofacConfiguration
    {

        public static ContainerBuilder ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ClientService>().As<IClientService>();
            builder.RegisterType<ProductService>().As<IProductService>();
            builder.RegisterType<PurchaseService>().As<IPurchaseService>();
            builder.RegisterType<Repository>().As<IRepository>();
            builder.RegisterType<ManagerService>().As<IManagerService>();
            builder.RegisterInstance(LoggerFactory.GetLogger());


            return builder;
        }
    }
}
