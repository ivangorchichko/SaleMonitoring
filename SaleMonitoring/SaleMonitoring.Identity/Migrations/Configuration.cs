namespace SaleMonitoring.Identity.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SaleMonitoring.Identity.DbContext.AccountContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SaleMonitoring.Identity.DbContext.AccountContext context)
        {
           
        }
    }
}
