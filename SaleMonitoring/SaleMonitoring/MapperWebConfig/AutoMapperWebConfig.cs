using AutoMapper;
using SaleMonitoring.BL.Models;
using SaleMonitoring.Models.Client;
using SaleMonitoring.Models.Manager;
using SaleMonitoring.Models.Product;
using SaleMonitoring.Models.Purchase;

namespace SaleMonitoring.MapperWebConfig
{
    public static class AutoMapperWebConfig
    {
        public static MapperConfiguration Configure()
        {
            return new MapperConfiguration(conf =>
            {
                //Purchase//
                conf.CreateMap<PurchaseDto, PurchaseViewModel>()
                    .ForPath(x => x.ClientName, o => o.MapFrom(z => z.Client.ClientName))
                    .ForPath(x => x.ClientTelephone, y => y.MapFrom(z => z.Client.ClientTelephone))
                    .ForPath(x => x.ProductName, y => y.MapFrom(z => z.Product.ProductName))
                    .ForPath(x => x.Price, y => y.MapFrom(z => z.Product.Price))
                    .ForPath(x => x.Id, y => y.MapFrom(z => z.Id))
                    .ForPath(x => x.ManagerName, y => y.MapFrom(z => z.Manager.ManagerName))
                    .ForPath(x => x.ManagerTelephone, y => y.MapFrom(z => z.Manager.ManagerTelephone))
                    .ForPath(x => x.ManagerRank, y => y.MapFrom(z => z.Manager.ManagerRank));


                conf.CreateMap<PurchaseViewModel, PurchaseDto>()
                    .ForPath(x => x.Client.ClientName, o => o.MapFrom(z => z.ClientName))
                    .ForPath(x => x.Client.ClientTelephone, y => y.MapFrom(z => z.ClientTelephone))
                    .ForPath(x => x.Product.ProductName, y => y.MapFrom(z => z.ProductName))
                    .ForPath(x => x.Product.Price, y => y.MapFrom(z => z.Price))
                    .ForPath(x => x.Id, y => y.MapFrom(z => z.Id))
                    .ForPath(x => x.Manager.ManagerName, y => y.MapFrom(z => z.ManagerName))
                    .ForPath(x => x.Manager.ManagerTelephone, y => y.MapFrom(z => z.ManagerTelephone))
                    .ForPath(x => x.Manager.ManagerRank, y => y.MapFrom(z => z.ManagerRank));

                conf.CreateMap<CreatePurchaseViewModel, PurchaseViewModel>();
                conf.CreateMap<PurchaseViewModel, ModifyPurchaseViewModel>();
                conf.CreateMap<ModifyPurchaseViewModel, PurchaseViewModel>();
                //Client//
                conf.CreateMap<ClientDto, ClientViewModel>();
                conf.CreateMap<ClientViewModel, ClientDto>();
                   
                //Product//
                conf.CreateMap<ProductDto, ProductViewModel>();
                conf.CreateMap<ProductViewModel, ProductDto>();

                //Manager//
                conf.CreateMap<ManagerDto, ManagerViewModel>();
                conf.CreateMap<ManagerViewModel, ManagerDto>();

            });
        }
    }
}
