using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Serilog;
using SaleMonitoring.BL.Contacts;
using SaleMonitoring.DAL.DataBaseContext;
using SaleMonitoring.DAL.Repository.Contract;
using SaleMonitoring.DomainModel.DataModel;
using SaleMonitoring.Service.Contracts;

namespace SaleMonitoring.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPurchaseMapper _purchaseMapper;
        private readonly ILogger _logger;

        public HomeController(IPurchaseMapper purchaseMapper, ILogger logger)
        {
            _purchaseMapper = purchaseMapper;
            _logger = logger;
        }

        public ActionResult Index()
        {
            _logger.Debug("Running Index Get method in HomeController");
            if (_purchaseMapper.GetPurchaseViewModel().ToList().Count == 0)
            {
                _logger.Debug("Fill database");
                FillDb();
            }
            _logger.Debug("Sharing Index view");
            return View(_purchaseMapper.GetPurchaseViewModel().ToList());
        }

        public void FillDb()
        {
            using (var context = new PurchaseContext())
            {
                List<ClientEntity> clientEntities = new List<ClientEntity>()
                {
                    new ClientEntity() {ClientName = "Mike", ClientTelephone = "+375293647144",Date = DateTime.Now, Id = 1},
                    new ClientEntity() {ClientName = "John", ClientTelephone = "+375333647144",Date = DateTime.Now, Id = 2},
                    new ClientEntity() {ClientName = "James", ClientTelephone = "+375291234567",Date = DateTime.Now, Id = 3},
                    new ClientEntity() {ClientName = "Rose", ClientTelephone = "+375331234567",Date = DateTime.Now, Id = 4},
                    new ClientEntity() {ClientName = "Michael", ClientTelephone = "+375297654321",Date = DateTime.Now, Id = 5},
                };
                List<ProductEntity> productEntities = new List<ProductEntity>()
                {
                    new ProductEntity() {Price = 1.2, ProductName = "juice",Date = DateTime.Now, Id = 1},
                    new ProductEntity() {Price = 1.0, ProductName = "water",Date = DateTime.Now, Id = 2},
                    new ProductEntity() {Price = 3.6, ProductName = "mellow",Date = DateTime.Now, Id = 3},
                    new ProductEntity() {Price = 1.2, ProductName = "apple",Date = DateTime.Now, Id = 4},
                    new ProductEntity() {Price = 100.5, ProductName = "car",Date = DateTime.Now, Id = 5},
                };
                List<ManagerEntity> managerEntities = new List<ManagerEntity>()
                {
                    new ManagerEntity() {ManagerName = "Ivan", ManagerTelephone = "+375333647144", Date = DateTime.Now, ManagerRank = "senor", Id = 1},
                    new ManagerEntity() {ManagerName = "Victor", ManagerTelephone = "+375291324576", Date = DateTime.Now, ManagerRank = "junior", Id = 2},
                    new ManagerEntity() {ManagerName = "Kate", ManagerTelephone = "+375292413657", Date = DateTime.Now, ManagerRank = "middle", Id = 3},
                };
                List<PurchaseEntity> purchaseEntities = new List<PurchaseEntity>()
                {
                    new PurchaseEntity()
                    {
                        ClientId = clientEntities[0].Id,
                        Date = DateTime.Now,
                        ProductId = productEntities[0].Id,
                        Client = clientEntities[0],
                        Product = productEntities[0],
                        ContractNumber = 112233,
                        Manager = managerEntities[0],
                        ManagerId = managerEntities[0].Id,
                        Id = 1,
                    },
                    new PurchaseEntity()
                    {
                        ClientId = clientEntities[1].Id,
                        Date = DateTime.Now,
                        ProductId = productEntities[1].Id,
                        Client = clientEntities[1],
                        Product = productEntities[1],
                        ContractNumber = 112234,
                        Manager = managerEntities[1],
                        ManagerId = managerEntities[1].Id,
                        Id = 2,
                    },
                    new PurchaseEntity()
                    {
                        ClientId = clientEntities[2].Id,
                        Date = DateTime.Now,
                        ProductId = productEntities[2].Id,
                        Client = clientEntities[2],
                        Product = productEntities[2],
                        ContractNumber = 112235,
                        Manager = managerEntities[2],
                        ManagerId = managerEntities[2].Id,
                        Id = 3,
                    },
                    new PurchaseEntity()
                    {
                        ClientId = clientEntities[3].Id,
                        Date = DateTime.Now,
                        ProductId = productEntities[3].Id,
                        Client = clientEntities[3],
                        Product = productEntities[3],
                        ContractNumber = 112236,
                        Manager = managerEntities[0],
                        ManagerId = managerEntities[0].Id,
                        Id = 4,
                    },
                    new PurchaseEntity()
                    {
                        ClientId = clientEntities[4].Id,
                        Date = DateTime.Now,
                        ProductId = productEntities[4].Id,
                        Client = clientEntities[4],
                        Product = productEntities[4],
                        ContractNumber = 112237,
                        Manager = managerEntities[0],
                        ManagerId = managerEntities[0].Id,
                        Id = 5,
                    },
                    new PurchaseEntity()
                    {
                        ClientId = clientEntities[4].Id,
                        Date = DateTime.Now,
                        ProductId = productEntities[1].Id,
                        Client = clientEntities[4],
                        Product = productEntities[1],
                        ContractNumber = 112238,
                        Manager = managerEntities[1],
                        ManagerId = managerEntities[1].Id,
                        Id = 6,
                    },
                };
                context.Purchases.AddRange(purchaseEntities);
                context.SaveChanges();
            }
        }
    }
}