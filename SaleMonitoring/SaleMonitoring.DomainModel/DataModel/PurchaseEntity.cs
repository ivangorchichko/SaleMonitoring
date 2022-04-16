using System;
using SaleMonitoring.DomainModel.Contract;

namespace SaleMonitoring.DomainModel.DataModel
{
    public class PurchaseEntity : IGenericProperty
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int ContractNumber { get; set; }

        public int ClientId { get; set; }

        public virtual ClientEntity Client { get; set; }

        public int ProductId { get; set; }

        public virtual ProductEntity Product { get; set; }

        public int ManagerId { get; set; }

        public virtual ManagerEntity Manager { get; set; }
    }
}