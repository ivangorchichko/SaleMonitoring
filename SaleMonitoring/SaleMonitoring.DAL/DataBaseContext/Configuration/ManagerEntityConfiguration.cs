using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaleMonitoring.DomainModel.DataModel;

namespace SaleMonitoring.DAL.DataBaseContext.Configuration
{
    public class ManagerEntityConfiguration : EntityTypeConfiguration<ManagerEntity>
    {
        public ManagerEntityConfiguration()
        {
            ToTable("dbo.Manager");

            HasKey(p => p.Id);
        }
    }
}
