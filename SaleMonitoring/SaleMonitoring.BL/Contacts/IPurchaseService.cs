using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SaleMonitoring.BL.Enums;
using SaleMonitoring.BL.Models;
using SaleMonitoring.DomainModel.DataModel;

namespace SaleMonitoring.BL.Contacts
{
    public interface IPurchaseService
    {
        IEnumerable<PurchaseDto> GetPurchaseDto();

        IEnumerable<PurchaseDto> GetPurchaseDto(int page, Expression<Func<PurchaseEntity, bool>> predicate = null);

        void AddPurchase(PurchaseDto purchaseDto);

        void ModifyPurchase(PurchaseDto purchaseDto);

        void RemovePurchase(PurchaseDto purchaseDto);

        IEnumerable<PurchaseDto> GetFilteredPurchaseDto(TextFieldFilter filter, string fieldString, int page);
    }
}
