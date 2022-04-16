using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using SaleMonitoring.BL.Contacts;
using SaleMonitoring.BL.Enums;
using SaleMonitoring.DAL.Repository.Contract;
using SaleMonitoring.MapperWebConfig;
using SaleMonitoring.Models.Client;
using SaleMonitoring.Models.Manager;
using SaleMonitoring.Models.Page;
using SaleMonitoring.Models.Product;
using SaleMonitoring.Models.Purchase;
using SaleMonitoring.PageHelper.Contacts;

namespace SaleMonitoring.PageHelper
{
    public class PageService : IPageService
    {
        private readonly IPurchaseService _purchaseService;
        private readonly IProductService _productService;
        private readonly IClientService _clientService;
        private readonly IManagerService _managerService;
        private readonly IMapper _mapper;

        public PageService(IPurchaseService purchaseService, IClientService clientService, IProductService productService, IManagerService managerService)
        {
            _clientService = clientService;
            _productService = productService;
            _managerService = managerService;
            _purchaseService = purchaseService;
            _mapper = new Mapper(AutoMapperWebConfig.Configure());

        }

        public ModelsInPageViewModel GetModelsInPageViewModel<TModel> (int page) where TModel : class
        {
            if (typeof(TModel) == typeof(PurchaseViewModel))
            {
                var modelsDto = _purchaseService.GetPurchaseDto(page);
                var modelsPerPage = _mapper.Map<IEnumerable<PurchaseViewModel>>(modelsDto);
                PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = 3, TotalItems = _purchaseService.GetPurchaseDto().ToList().Count };
                return new ModelsInPageViewModel() {PageInfo = pageInfo, Purchases = modelsPerPage};
            }
            if (typeof(TModel) == typeof(ProductViewModel))
            {
                var modelsDto = _productService.GetProductsDto(page);
                var modelsPerPage = _mapper.Map<IEnumerable<ProductViewModel>>(modelsDto);
                PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = 3, TotalItems = _productService.GetProductsDto().ToList().Count };
                return new ModelsInPageViewModel() { PageInfo = pageInfo, Products = modelsPerPage };
            }
            if (typeof(TModel) == typeof(ClientViewModel))
            {
                var modelsDto = _clientService.GetClientsDto(page);
                var modelsPerPage = _mapper.Map<IEnumerable<ClientViewModel>>(modelsDto);
                PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = 3, TotalItems = _clientService.GetClientsDto().ToList().Count };
                return new ModelsInPageViewModel() { PageInfo = pageInfo, Clients = modelsPerPage };
            }
            if (typeof(TModel) == typeof(ManagerViewModel))
            {
                var modelsDto = _managerService.GetManagersDto(page);
                var modelsPerPage = _mapper.Map<IEnumerable<ManagerViewModel>>(modelsDto);
                PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = 3, TotalItems = _managerService.GetManagersDto().ToList().Count };
                return new ModelsInPageViewModel() { PageInfo = pageInfo, Managers = modelsPerPage };
            }

            return null;
        }

        public ModelsInPageViewModel GetFilteredModelsInPageViewModel<TModel>(TextFieldFilter filter, string fieldString, int page) where TModel : class
        {
            if(filter == TextFieldFilter.Default)
            {
               return GetModelsInPageViewModel<TModel>(page);
            }
            if (typeof(TModel) == typeof(PurchaseViewModel))
            {
                var modelsDto = _purchaseService.GetFilteredPurchaseDto(filter, fieldString, page);
                var pagingModelDto = modelsDto.Skip((page - 1) * 3).Take(3).ToList();
                var modelsPerPage = _mapper.Map<IEnumerable<PurchaseViewModel>>(pagingModelDto);
                PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = 3, TotalItems = modelsDto.ToList().Count };
                return new ModelsInPageViewModel() { PageInfo = pageInfo, Purchases = modelsPerPage };
            }
            if (typeof(TModel) == typeof(ProductViewModel))
            {
                var modelsDto = _productService.GetFilteredProductDto(filter, fieldString, page);
                var pagingModelDto = modelsDto.Skip((page - 1) * 3).Take(3).ToList();
                var modelsPerPage = _mapper.Map<IEnumerable<ProductViewModel>>(pagingModelDto);
                PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = 3, TotalItems = modelsDto.ToList().Count };
                return new ModelsInPageViewModel() { PageInfo = pageInfo, Products = modelsPerPage };
            }
            if (typeof(TModel) == typeof(ClientViewModel))
            {
                var modelsDto = _clientService.GetFilteredClientDto(filter, fieldString, page);
                var pagingModelDto = modelsDto.Skip((page - 1) * 3).Take(3).ToList();
                var modelsPerPage = _mapper.Map<IEnumerable<ClientViewModel>>(pagingModelDto);
                PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = 3, TotalItems = modelsDto.ToList().Count };
                return new ModelsInPageViewModel() { PageInfo = pageInfo, Clients = modelsPerPage };
            }
            if (typeof(TModel) == typeof(ManagerViewModel))
            {
                var modelsDto = _managerService.GetFilteredManagerDto(filter, fieldString, page);
                var pagingModelDto = modelsDto.Skip((page - 1) * 3).Take(3).ToList();
                var modelsPerPage = _mapper.Map<IEnumerable<ManagerViewModel>>(pagingModelDto);
                PageInfo pageInfo = new PageInfo { PageNumber = page, PageSize = 3, TotalItems = modelsDto.ToList().Count };
                return new ModelsInPageViewModel() { PageInfo = pageInfo, Managers = modelsPerPage };
            }
            return null;
        }
    }
}