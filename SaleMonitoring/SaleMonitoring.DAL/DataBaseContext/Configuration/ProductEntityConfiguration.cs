using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaleMonitoring.DomainModel.DataModel;

namespace SaleMonitoring.DAL.DataBaseContext.Configuration
{
    public class ProductEntityConfiguration : EntityTypeConfiguration<ProductEntity>
    {
        public ProductEntityConfiguration()
        {
            ToTable("dbo.Product");

            HasKey(p => p.Id);

        }
    }
}
