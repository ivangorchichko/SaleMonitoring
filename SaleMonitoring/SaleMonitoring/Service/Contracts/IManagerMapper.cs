using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaleMonitoring.BL.Models;
using SaleMonitoring.Models.Manager;

namespace SaleMonitoring.Service.Contracts
{
    public interface IManagerMapper
    {
        ManagerViewModel GetManagerViewModel(int? id);

        IEnumerable<ManagerViewModel> GetManagerViewModel();

        ManagerDto GetManagerDto(ManagerViewModel managerViewModel);
    }
}
