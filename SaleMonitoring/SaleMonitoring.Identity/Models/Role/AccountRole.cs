using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SaleMonitoring.Identity.Models.Role
{
    public class AccountRole : IdentityRole
    {
        public AccountRole() { }

        public string Description { get; set; }
    }
}