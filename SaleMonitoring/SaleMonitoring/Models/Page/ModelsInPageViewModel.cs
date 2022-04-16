using System.Collections.Generic;
using SaleMonitoring.Models.Client;
using SaleMonitoring.Models.Manager;
using SaleMonitoring.Models.Product;
using SaleMonitoring.Models.Purchase;

namespace SaleMonitoring.Models.Page
{
    public class ModelsInPageViewModel
    {
        public IEnumerable<ClientViewModel> Clients { get; set; }

        public IEnumerable<ProductViewModel> Products { get; set; }

        public IEnumerable<PurchaseViewModel> Purchases { get; set; }

        public IEnumerable<ManagerViewModel> Managers { get; set; }

        public PageInfo PageInfo { get; set; }
    }
}