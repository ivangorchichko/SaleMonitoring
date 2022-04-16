using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaleMonitoring.DomainModel.DataModel;

namespace SaleMonitoring.DAL.DataBaseContext.Configuration
{
    public class PurchaseEntityConfiguration : EntityTypeConfiguration<PurchaseEntity>
    {
        public PurchaseEntityConfiguration()
        {
            ToTable("dbo.Purchase");

            HasKey(p => p.Id);

            HasRequired(x => x.Client)
                .WithMany(x => x.Purchases)
                .HasForeignKey(x => x.ClientId);

            HasRequired(x => x.Product)
                .WithMany(x => x.Purchases)
                .HasForeignKey(x => x.ProductId);

            HasRequired(x => x.Manager)
                .WithMany(x => x.Purchases)
                .HasForeignKey(x => x.ManagerId);
        }
    }
}
