using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SaleMonitoring.BL.Enums;
using SaleMonitoring.BL.Models;
using SaleMonitoring.DomainModel.DataModel;

namespace SaleMonitoring.BL.Contacts
{
    public interface IClientService
    {
        IEnumerable<ClientDto> GetClientsDto();
        void AddClient(ClientDto clientDto);
        void ModifyClient(ClientDto clientDto);
        void RemoveClient(ClientDto clientDto);
        IEnumerable<ClientDto> GetFilteredClientDto(TextFieldFilter filter, string fieldString, int page);

        IEnumerable<ClientDto> GetClientsDto(int page, Expression<Func<ClientEntity, bool>> predicate = null);
    }
}
