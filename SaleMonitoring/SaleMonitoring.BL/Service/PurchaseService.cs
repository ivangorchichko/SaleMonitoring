using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using SaleMonitoring.BL.Contacts;
using SaleMonitoring.BL.Enums;
using SaleMonitoring.BL.MapperBLHelper;
using SaleMonitoring.BL.Models;
using SaleMonitoring.DAL.Repository.Contract;
using SaleMonitoring.DomainModel.DataModel;

namespace SaleMonitoring.BL.Service
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public PurchaseService(IRepository repository)
        {
            _mapper = new Mapper(AutoMapperBLConfig.Configure());
            _repository = repository;
        }
        public IEnumerable<PurchaseDto> GetPurchaseDto(int page, Expression<Func<PurchaseEntity, bool>> predicate = null)
        {
            List<PurchaseEntity> purchasesEntities;
            if (predicate != null)
            {
                purchasesEntities = _repository.Get<PurchaseEntity>(3, page, predicate).ToList();
            }
            else
            {
                purchasesEntities = _repository.Get<PurchaseEntity>(3, page).ToList();
            }

            var purchases =
                _mapper.Map<IEnumerable<PurchaseDto>>(purchasesEntities);
            return purchases;
        }
        public IEnumerable<PurchaseDto> GetPurchaseDto()
        {
            var purchasesEntities = _repository.Get<PurchaseEntity>().ToList();
            var purchases =
                _mapper.Map<IEnumerable<PurchaseDto>>(purchasesEntities);
            return purchases;
        }

        public void AddPurchase(PurchaseDto purchaseDto)
        {
            purchaseDto.Id = GetPurchaseDto().ToList().Count;
            purchaseDto.Client.Date = purchaseDto.Date;
            purchaseDto.Product.Date = purchaseDto.Date;
            purchaseDto.Client.Id = purchaseDto.Id;
            purchaseDto.Product.Id = purchaseDto.Id;

            var client = _repository.Get<ClientEntity>()
                .FirstOrDefault(
                    c => c.ClientName == purchaseDto.Client.ClientName &&
                    c.ClientTelephone == purchaseDto.Client.ClientTelephone);
            var product = _repository.Get<ProductEntity>()
                .FirstOrDefault(
                    p => p.ProductName == purchaseDto.Product.ProductName &&
                    p.Price == purchaseDto.Product.Price);
            var manager = _repository.Get<ManagerEntity>()
                .FirstOrDefault(m => m.ManagerName == purchaseDto.Manager.ManagerName
                );

            var purchase = _mapper.Map<PurchaseEntity>(purchaseDto);
            if (client != null && product != null && manager != null)
            {
                purchase.Client = client;
                purchase.Product = product;
                purchase.Manager = manager;
            }else
            if (client != null)
            {
                purchase.Client = client;
            }
            if (product != null)
            {
                purchase.Product = product;
            }
            if (manager != null)
            {
                purchase.Manager = manager;
            }

            _repository.Add<PurchaseEntity>(purchase);
        }

        public void ModifyPurchase(PurchaseDto purchaseDto)
        {
            var purchaseEntity = _repository.Get<PurchaseEntity>().ToList()
                .Find(purchase => purchase.Id == purchaseDto.Id);
            purchaseEntity.Product.ProductName = purchaseDto.Product.ProductName;
            purchaseEntity.Product.Price = purchaseDto.Product.Price;
            purchaseEntity.Client.ClientName = purchaseDto.Client.ClientName;
            purchaseEntity.Client.ClientTelephone = purchaseDto.Client.ClientTelephone;
            purchaseEntity.Manager.ManagerName = purchaseDto.Manager.ManagerName;
            purchaseEntity.Manager.ManagerTelephone = purchaseDto.Manager.ManagerTelephone;
            purchaseEntity.Manager.ManagerRank = purchaseDto.Manager.ManagerRank;
            purchaseEntity.Date = purchaseDto.Date;
            _repository.Save();
        }

        public void RemovePurchase(PurchaseDto purchaseDto)
        {
            var purchaseEntity = _repository.Get<PurchaseEntity>()
                .ToList()
                .Find(purchase => purchase.Id == purchaseDto.Id);
            _repository.Remove(purchaseEntity);
        }

        public IEnumerable<PurchaseDto> GetFilteredPurchaseDto(TextFieldFilter filter, string fieldString, int page)
        {
            switch (filter)
            {
                case TextFieldFilter.ClientName:
                {
                    return GetPurchaseDto(page, p => p.Client.ClientName == fieldString).ToList();
                    
                }
                case TextFieldFilter.ProductName:
                {
                    return GetPurchaseDto(page, p => p.Product.ProductName == fieldString).ToList();
                }
                case TextFieldFilter.Date:
                {
                    var parseDate = DateTime.Parse(fieldString);
                    return GetPurchaseDto(page, p => p.Date.Day == parseDate.Day
                                                     && p.Date.Month == parseDate.Month
                                                     && p.Date.Year == parseDate.Year).ToList();
                }
            }
            return GetPurchaseDto(page);
        }
    }
}