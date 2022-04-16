using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaleMonitoring.DomainModel.Contract;

namespace SaleMonitoring.DomainModel.DataModel
{
    public class ManagerEntity : IGenericProperty
    {
        public int Id { get; set; }

        public string ManagerName { get; set; }

        public string ManagerTelephone { get; set; }

        public DateTime Date { get; set; }
            
        public string ManagerRank { get; set; }

        public virtual ICollection<PurchaseEntity> Purchases { get; set; }

        public ManagerEntity()
        {
            Purchases = new List<PurchaseEntity>();
        }
    }
}
