using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SaleMonitoring.BL.Enums;
using SaleMonitoring.BL.Models;
using SaleMonitoring.DomainModel.DataModel;

namespace SaleMonitoring.BL.Contacts
{
    public interface IProductService
    {
        IEnumerable<ProductDto> GetProductsDto();
        IEnumerable<ProductDto> GetProductsDto(int page, Expression<Func<ProductEntity, bool>> predicate = null);
        void AddProduct(ProductDto productDto);
        void ModifyProduct(ProductDto productDto);
        void RemoveProduct(ProductDto productDto);
        IEnumerable<ProductDto> GetFilteredProductDto(TextFieldFilter filter, string fieldString, int page);
    }
}
