using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SaleMonitoring.BL.Contacts;
using SaleMonitoring.BL.Enums;
using SaleMonitoring.BL.MapperBLHelper;
using SaleMonitoring.BL.Models;
using SaleMonitoring.DAL.Repository.Contract;
using SaleMonitoring.DomainModel.DataModel;

namespace SaleMonitoring.BL.Service
{
    public class ProductService : IProductService
    {
        private static IMapper _mapper;
        private static IRepository _repository;

        public ProductService(IRepository repository)
        {
            _mapper = new Mapper(AutoMapperBLConfig.Configure());
            _repository = repository;
        }

        public IEnumerable<ProductDto> GetProductsDto()
        {
            var productEntities = _repository.Get<ProductEntity>();
            var products =
                _mapper.Map<IEnumerable<ProductDto>>(productEntities);
            return products;
        }

        public IEnumerable<ProductDto> GetProductsDto(int page, Expression<Func<ProductEntity, bool>> predicate = null)
        {
            List<ProductEntity> productEntities;
            if (predicate != null)
            {
                productEntities = _repository.Get<ProductEntity>(3, page, predicate).ToList();
            }
            else
            {
                productEntities = _repository.Get<ProductEntity>(3, page).ToList();
            }

            var products =
                _mapper.Map<IEnumerable<ProductDto>>(productEntities);
            return products;
        }

        public void AddProduct(ProductDto productDto)
        {
            var product = _repository.Get<ProductEntity>()
                .FirstOrDefault(
                    p => p.ProductName == productDto.ProductName &&
                         p.Price == productDto.Price);
            if (product != null)
            {

            }else _repository.Add<ProductEntity>(_mapper.Map<ProductEntity>(productDto));
        }

        public void ModifyProduct(ProductDto productDto)
        {
            var productEntity = _repository.Get<ProductEntity>().ToList()
                .Find(product => product.Id == productDto.Id);
            productEntity.ProductName = productDto.ProductName;
            productEntity.Price = productDto.Price;
            productEntity.Id = productDto.Id;
            _repository.Save();
        }

        public void RemoveProduct(ProductDto productDto)
        {
            var productEntity = _repository.Get<ProductEntity>()
                .ToList()
                .Find(product => product.Id == productDto.Id);
            _repository.Remove(productEntity);
        }

        public IEnumerable<ProductDto> GetFilteredProductDto(TextFieldFilter filter, string fieldString, int page)
        { 
            switch (filter)
            {
                case TextFieldFilter.ProductName:
                {
                    return GetProductsDto(page, p => p.ProductName == fieldString).ToList();

                }
                case TextFieldFilter.Price:
                {
                    var parseField = Double.Parse(fieldString);
                    return GetProductsDto(page, p => p.Price == parseField).ToList();
                }
            }

            return GetProductsDto(page);
        }
    }
}
