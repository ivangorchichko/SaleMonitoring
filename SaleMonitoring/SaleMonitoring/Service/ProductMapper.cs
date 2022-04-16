using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using SaleMonitoring.BL.Contacts;
using SaleMonitoring.BL.Models;
using SaleMonitoring.MapperWebConfig;
using SaleMonitoring.Models.Manager;
using SaleMonitoring.Models.Product;
using SaleMonitoring.Models.Purchase;
using SaleMonitoring.Service.Contracts;

namespace SaleMonitoring.Service
{
    public class ProductMapper : IProductMapper
    {
        private readonly IMapper _mapper;
        private readonly IProductService _productService;

        public ProductMapper(IProductService productService)
        {
            _productService = productService;

            _mapper = new Mapper(AutoMapperWebConfig.Configure());
        }

        public ProductViewModel GetProductViewModel(int? id)
        {
            return _mapper.Map<IEnumerable<ProductViewModel>>(_productService.GetProductsDto())
                    .ToList().Find(x => x.Id == id);
        }
        public IEnumerable<ProductViewModel> GetProductViewModel()
        {
            return _mapper.Map<IEnumerable<ProductViewModel>>(_productService.GetProductsDto())
                .OrderBy(d => d.Date);
        }


        public ProductDto GetProductDto(ProductViewModel productViewModel)
        {
            return _mapper.Map<ProductDto>(productViewModel);
        }

    }
}