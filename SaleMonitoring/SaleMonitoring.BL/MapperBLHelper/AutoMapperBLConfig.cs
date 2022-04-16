using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SaleMonitoring.BL.Models;
using SaleMonitoring.DomainModel.DataModel;

namespace SaleMonitoring.BL.MapperBLHelper
{
    public static class AutoMapperBLConfig
    {
        public static MapperConfiguration Configure()
        {
            return new MapperConfiguration(conf =>
            {
                conf.CreateMap<ClientEntity, ClientDto>();
                conf.CreateMap<ClientDto, ClientEntity>();

                conf.CreateMap<ProductEntity, ProductDto>();
                conf.CreateMap<ProductDto, ProductEntity>();

                conf.CreateMap<PurchaseEntity, PurchaseDto>();
                //    .ForPath(x => x.Client.ClientName, o => o.MapFrom(z => z.Client.ClientName))
                //    .ForPath(x => x.Client.Telephone, y => y.MapFrom(z => z.Client.Telephone))
                //    .ForPath(x => x.Client.Id, y => y.MapFrom(z => z.Client.Id))
                //    .ForPath(x => x.Product.ProductName, y => y.MapFrom(z => z.Product.ProductName))
                //    .ForPath(x => x.Product.Price, y => y.MapFrom(z => z.Product.Price))
                //    .ForPath(x => x.Product.Id, y => y.MapFrom(z => z.Product.Id))
                //    .ForPath(x => x.Id, y => y.MapFrom(z => z.Id))
                //    .ForPath(x => x.Manager.ManagerName, y => y.MapFrom(z => z.Manager.ManagerName))
                //    .ForPath(x => x.Manager.ManagerTelephone, y => y.MapFrom(z => z.Manager.ManagerTelephone))
                //    .ForPath(x => x.Manager.ManagerRank, y => y.MapFrom(z => z.Manager.ManagerRank))
                //    .ForPath(x => x.Manager.Id, y => y.MapFrom(z => z.Manager.Id))
                //    .ForPath(x => x.Manager.Date, y => y.MapFrom(z => z.Manager.Date))
                //    .ForPath(x => x.Client.Date, y => y.MapFrom(z => z.Client.Date))
                //    .ForPath(x => x.Product.Date, y => y.MapFrom(z => z.Product.Date));
                conf.CreateMap<PurchaseDto, PurchaseEntity>();
                    //.ForPath(x => x.Client.ClientName, o => o.MapFrom(z => z.Client.ClientName))
                    //.ForPath(x => x.Client.Telephone, y => y.MapFrom(z => z.Client.Telephone))
                    //.ForPath(x => x.Product.ProductName, y => y.MapFrom(z => z.Product.ProductName))
                    //.ForPath(x => x.Product.Price, y => y.MapFrom(z => z.Product.Price))
                    //.ForPath(x => x.Id, y => y.MapFrom(z => z.Id));

                conf.CreateMap<ManagerEntity, ManagerDto>();
                conf.CreateMap<ManagerDto, ManagerEntity>();

            });
        }
    }
}
