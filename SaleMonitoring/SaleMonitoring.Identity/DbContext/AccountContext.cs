using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using SaleMonitoring.Identity.Models.Account;

namespace SaleMonitoring.Identity.DbContext
{
    public class AccountContext : IdentityDbContext<AccountUser>
    {
        public AccountContext() : base("OrdersDB") { }

        public static AccountContext Create()
        {
            return new AccountContext();
        }
        
    }
}