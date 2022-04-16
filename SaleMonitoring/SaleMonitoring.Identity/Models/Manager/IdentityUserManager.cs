using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using SaleMonitoring.Identity.DbContext;
using SaleMonitoring.Identity.Models.Account;

namespace SaleMonitoring.Identity.Models.Manager
{
    public class IdentityUserManager : UserManager<AccountUser>
    {
        public IdentityUserManager(IUserStore<AccountUser> store)
            : base(store)
        {
        }
        public static IdentityUserManager Create(IdentityFactoryOptions<IdentityUserManager> options,
            IOwinContext context)
        {
            AccountContext db = context.Get<AccountContext>();
            IdentityUserManager manager = new IdentityUserManager(new UserStore<AccountUser>(db));
            return manager;
        }
    }
}