using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using SaleMonitoring.Identity.DbContext;
using SaleMonitoring.Identity.Models.Role;

namespace SaleMonitoring.Identity.Models.Manager
{
    public class IdentityRoleManager : RoleManager<AccountRole>
    {
        public IdentityRoleManager(IRoleStore<AccountRole, string> store) : base(store)
        {
        }

        public static IdentityRoleManager Create(IdentityFactoryOptions<IdentityRoleManager> options,
            IOwinContext context)
        {
            return new IdentityRoleManager(new
                RoleStore<AccountRole>(context.Get<AccountContext>()));
        }
    }
}