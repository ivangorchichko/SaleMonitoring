using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaleMonitoring.DAL.DataBaseContext.Configuration;
using SaleMonitoring.DomainModel.DataModel;


namespace SaleMonitoring.DAL.DataBaseContext
{
    public class PurchaseContext : DbContext
    {
        public PurchaseContext() : base("OrdersDB")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PurchaseContext, Migrations.Configuration>());
        }

        public DbSet<ClientEntity> Clients { get; set; }

        public DbSet<PurchaseEntity> Purchases { get; set; }

        public DbSet<ProductEntity> Products { get; set; }

        public DbSet<ManagerEntity> Managers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PurchaseEntityConfiguration());
            modelBuilder.Configurations.Add(new ProductEntityConfiguration());
            modelBuilder.Configurations.Add(new ClientEntityConfiguration());
            modelBuilder.Configurations.Add(new ManagerEntityConfiguration());
        }
    }
}
