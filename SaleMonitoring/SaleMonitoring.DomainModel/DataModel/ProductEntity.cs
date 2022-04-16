using System;
using System.Collections.Generic;
using SaleMonitoring.DomainModel.Contract;

namespace SaleMonitoring.DomainModel.DataModel
{
    public class ProductEntity : IGenericProperty
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public double Price { get; set; }

        public DateTime Date { get; set; }

        public virtual ICollection<PurchaseEntity> Purchases { get; set; }

        public ProductEntity()
        {
            Purchases = new List<PurchaseEntity>();
        }
    }
}