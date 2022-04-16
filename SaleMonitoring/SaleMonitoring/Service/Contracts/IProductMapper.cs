using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaleMonitoring.BL.Models;
using SaleMonitoring.Models.Product;

namespace SaleMonitoring.Service.Contracts
{
    public interface IProductMapper
    {
        ProductViewModel GetProductViewModel(int? id);

        IEnumerable<ProductViewModel> GetProductViewModel();

        ProductDto GetProductDto(ProductViewModel productViewModel);
    }
}
