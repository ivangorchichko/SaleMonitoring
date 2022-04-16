using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using SaleMonitoring.BL.Contacts;
using SaleMonitoring.BL.Models;
using SaleMonitoring.DomainModel.DataModel;
using SaleMonitoring.MapperWebConfig;
using SaleMonitoring.Models.Manager;
using SaleMonitoring.Models.Purchase;
using SaleMonitoring.Service.Contracts;

namespace SaleMonitoring.Service
{
    public class PurchaseMapper : IPurchaseMapper
    {
        private readonly IMapper _mapper;
        private readonly IPurchaseService _purchaseService;
        private readonly IManagerService _managerService;

        public PurchaseMapper(IPurchaseService purchaseService, IManagerService managerService)
        {
            _purchaseService = purchaseService;
            _managerService = managerService;
            _mapper = new Mapper(AutoMapperWebConfig.Configure());
        }

        public PurchaseViewModel GetPurchaseViewModel(int? id)
        {
            return _mapper.Map<IEnumerable<PurchaseViewModel>>(_purchaseService.GetPurchaseDto())
                    .ToList().Find(x => x.Id == id);
        }
        public IEnumerable<PurchaseViewModel> GetPurchaseViewModel()
        {
            return _mapper.Map<IEnumerable<PurchaseViewModel>>(_purchaseService.GetPurchaseDto())
                .OrderBy(d => d.Date);
        }

        public PurchaseViewModel GetPurchaseViewModel(CreatePurchaseViewModel createPurchase)
        {
            return _mapper.Map<PurchaseViewModel>(createPurchase);
        }

        public PurchaseDto GetPurchaseDto(PurchaseViewModel purchaseViewModel)
        {
            return _mapper.Map<PurchaseDto>(purchaseViewModel);
        }

        public PurchaseDto GetPurchaseDto(CreatePurchaseViewModel createPurchaseViewModel)
        {
            var manager = GetManagersDto().FirstOrDefault(m => m.ManagerName == createPurchaseViewModel.ManagerName);
            var purchaseViewModel = _mapper.Map<PurchaseViewModel>(createPurchaseViewModel);
            purchaseViewModel.ManagerRank = manager.ManagerRank;
            purchaseViewModel.ManagerTelephone = manager.ManagerTelephone;
            return _mapper.Map<PurchaseDto>(purchaseViewModel);
        }

        public PurchaseDto GetPurchaseDto(ModifyPurchaseViewModel modifyPurchaseViewModel)
        {
            var manager = GetManagersDto().FirstOrDefault(m => m.ManagerName == modifyPurchaseViewModel.ManagerName);
            var purchaseViewModel = _mapper.Map<PurchaseViewModel>(modifyPurchaseViewModel);
            purchaseViewModel.ManagerRank = manager.ManagerRank;
            purchaseViewModel.ManagerTelephone = manager.ManagerTelephone;
            return _mapper.Map<PurchaseDto>(purchaseViewModel);
        }

        private IEnumerable<ManagerViewModel> GetManagersDto()
        {
            return _mapper.Map<IEnumerable<ManagerViewModel>>(_managerService.GetManagersDto());
        }

        public CreatePurchaseViewModel GetManagersViewModel()
        {
            return new CreatePurchaseViewModel()
            {
                Managers = new SelectList(GetManagersDto(), "ManagerName", "ManagerName"),
            };
        }

        public ModifyPurchaseViewModel GetModifyPurchaseViewModel(int? id)
        {
            var purchase = _mapper.Map<ModifyPurchaseViewModel>(GetPurchaseViewModel(id));
            purchase.Managers = new SelectList(GetManagersDto(), "ManagerName", "ManagerName");
            return purchase;
        }
    }
}