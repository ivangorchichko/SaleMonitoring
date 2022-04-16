using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using SaleMonitoring.BL.DIConfig;
using SaleMonitoring.Identity.DbContext;
using SaleMonitoring.Identity.Models.Account;
using SaleMonitoring.Identity.Models.Manager;
using SaleMonitoring.PageHelper;
using SaleMonitoring.PageHelper.Contacts;
using SaleMonitoring.Service;
using SaleMonitoring.Service.Contracts;

[assembly: OwinStartupAttribute(typeof(SaleMonitoring.App_Start.Startup))]
namespace SaleMonitoring.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            RegistrContainer();
            app.CreatePerOwinContext<AccountContext>(AccountContext.Create);
            app.CreatePerOwinContext<IdentityUserManager>(IdentityUserManager.Create);
            app.CreatePerOwinContext<IdentityRoleManager>(IdentityRoleManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }

        private void RegistrContainer()
        {
            var builder = AutofacConfiguration.ConfigureContainer();
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterType<PageService>().As<IPageService>();
            builder.RegisterType<PurchaseMapper>().As<IPurchaseMapper>();
            builder.RegisterType<ManagerMapper>().As<IManagerMapper>();
            builder.RegisterType<ProductMapper>().As<IProductMapper>();
            builder.RegisterType<ClientMapper>().As<IClientMapper>();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));
        }
    }
}