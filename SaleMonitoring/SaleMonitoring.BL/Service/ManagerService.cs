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
    public class ManagerService : IManagerService
    {
        private readonly IMapper _mapper;
        private readonly IRepository _repository;

        public ManagerService(IRepository repository)
        {
            _mapper = new Mapper(AutoMapperBLConfig.Configure());
            _repository = repository;
        }
        public IEnumerable<ManagerDto> GetManagersDto(int page, Expression<Func<ManagerEntity, bool>> predicate = null)
        {
            List<ManagerEntity> managerEntities;
            if (predicate != null)
            {
                managerEntities = _repository.Get<ManagerEntity>(3, page, predicate).ToList();
            }
            else
            {
                managerEntities = _repository.Get<ManagerEntity>(3, page).ToList();
            }

            var managers =
                _mapper.Map<IEnumerable<ManagerDto>>(managerEntities);
            return managers;
        }
        public IEnumerable<ManagerDto> GetManagersDto()
        {
            var managerEntities = _repository.Get<ManagerEntity>();
            var manager
                = _mapper.Map<IEnumerable<ManagerDto>>(managerEntities);
            return manager;
        }

        public void AddManager(ManagerDto managerDto)
        {
            var manager = _repository.Get<ManagerEntity>()
                .FirstOrDefault(
                    m => m.ManagerName == managerDto.ManagerName
                    && m.ManagerTelephone == managerDto.ManagerTelephone
                    && m.ManagerRank == managerDto.ManagerRank
                );
            if (manager != null)
            {

            }
            else
                _repository.Add<ManagerEntity>(_mapper.Map<ManagerEntity>(managerDto));
        }

        public void ModifyManager(ManagerDto managerDto)
        {
            var managerEntity = _repository.Get<ManagerEntity>().ToList()
                .Find(manager => manager.Id == managerDto.Id);
            managerEntity.ManagerName = managerDto.ManagerName;
            managerEntity.ManagerTelephone = managerDto.ManagerTelephone;
            managerEntity.ManagerRank = managerDto.ManagerRank;
            managerEntity.Date = managerDto.Date;
            _repository.Save();
        }

        public void RemoveManager(ManagerDto managerDto)
        {
            var managerEntity = _repository.Get<ManagerEntity>()
                .ToList()
                .Find(manager => manager.Id == managerDto.Id);
            _repository.Remove(managerEntity);
        }

        public IEnumerable<ManagerDto> GetFilteredManagerDto(TextFieldFilter filter, string fieldString, int page)
        {
            switch (filter)
            {
                case TextFieldFilter.ManagerName:
                {
                    return GetManagersDto(page, m => m.ManagerName == fieldString).ToList();

                }

                case TextFieldFilter.Telephone:
                {
                    return GetManagersDto(page, m => m.ManagerTelephone == fieldString).ToList();
                }

                case TextFieldFilter.ManagerRank:
                {
                    return GetManagersDto(page, m => m.ManagerRank == fieldString).ToList();
                }
            }

            return GetManagersDto(page);
        }
    }
}
