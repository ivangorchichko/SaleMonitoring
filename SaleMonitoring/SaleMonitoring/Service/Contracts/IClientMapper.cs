using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaleMonitoring.BL.Models;
using SaleMonitoring.Models.Client;

namespace SaleMonitoring.Service.Contracts
{
    public interface IClientMapper
    {
        ClientViewModel GetClientViewModel(int? id);

        IEnumerable<ClientViewModel> GetClientViewModel();

        ClientDto GetClientDto(ClientViewModel clientViewModel);
    }
}
