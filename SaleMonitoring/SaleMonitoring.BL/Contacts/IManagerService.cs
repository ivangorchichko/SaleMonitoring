using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SaleMonitoring.BL.Enums;
using SaleMonitoring.BL.Models;
using SaleMonitoring.DomainModel.DataModel;

namespace SaleMonitoring.BL.Contacts
{
    public interface IManagerService
    {
        IEnumerable<ManagerDto> GetManagersDto();

        IEnumerable<ManagerDto> GetManagersDto(int page, Expression<Func<ManagerEntity, bool>> predicate = null);

        void AddManager(ManagerDto managerDto);

        void ModifyManager(ManagerDto managerDto);

        void RemoveManager(ManagerDto managerDto);

        IEnumerable<ManagerDto> GetFilteredManagerDto(TextFieldFilter filter, string fieldString, int page);
    }
}
