using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaleMonitoring.BL.Models;
using SaleMonitoring.Models.Manager;
using SaleMonitoring.Models.Purchase;

namespace SaleMonitoring.Service.Contracts
{
    public interface IPurchaseMapper
    {
        PurchaseViewModel GetPurchaseViewModel(int? id);

        PurchaseDto GetPurchaseDto(PurchaseViewModel purchaseViewModel);

        PurchaseDto GetPurchaseDto(CreatePurchaseViewModel createPurchaseViewModel);

        PurchaseDto GetPurchaseDto(ModifyPurchaseViewModel modifyPurchaseViewModel);

        IEnumerable<PurchaseViewModel> GetPurchaseViewModel();

        PurchaseViewModel GetPurchaseViewModel(CreatePurchaseViewModel createPurchase);

        CreatePurchaseViewModel GetManagersViewModel();

        ModifyPurchaseViewModel GetModifyPurchaseViewModel(int? id);
    }
}
