using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using SaleMonitoring.BL.Contacts;
using SaleMonitoring.BL.Models;
using SaleMonitoring.MapperWebConfig;
using SaleMonitoring.Models.Client;
using SaleMonitoring.Models.Manager;
using SaleMonitoring.Service.Contracts;

namespace SaleMonitoring.Service
{
    public class ManagerMapper : IManagerMapper
    {
        private readonly IMapper _mapper;
        private readonly IManagerService _managerService;

        public ManagerMapper(IManagerService managerService)
        {
            _managerService = managerService;
            _mapper = new Mapper(AutoMapperWebConfig.Configure());
        }

        public ManagerViewModel GetManagerViewModel(int? id)
        {
            return _mapper.Map<IEnumerable<ManagerViewModel>>(_managerService.GetManagersDto())
                .ToList().Find(x => x.Id == id);
        }
        public IEnumerable<ManagerViewModel> GetManagerViewModel()
        {
            return _mapper.Map<IEnumerable<ManagerViewModel>>(_managerService.GetManagersDto())
                .OrderBy(d => d.Date);
        }


        public ManagerDto GetManagerDto(ManagerViewModel managerViewModel)
        {
            return _mapper.Map<ManagerDto>(managerViewModel);
        }
    }
}