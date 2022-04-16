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
    public class ClientService : IClientService
    {
        private static IMapper _mapper;
        private static IRepository _repository;

        public ClientService(IRepository repository)
        {
            _mapper = new Mapper(AutoMapperBLConfig.Configure());
            _repository = repository;
        }

        public IEnumerable<ClientDto> GetClientsDto()
        {
            var clientEntities = _repository.Get<ClientEntity>();
            var client =
                _mapper.Map<IEnumerable<ClientDto>>(clientEntities);
            return client;
        }

        public IEnumerable<ClientDto> GetClientsDto(int page, Expression<Func<ClientEntity, bool>> predicate = null)
        {
            List<ClientEntity> clientEntities;
            if (predicate != null)
            {
                clientEntities = _repository.Get<ClientEntity>(3, page, predicate).ToList();
            }
            else
            {
                clientEntities = _repository.Get<ClientEntity>(3, page).ToList();
            }

            var clients =
                _mapper.Map<IEnumerable<ClientDto>>(clientEntities);
            return clients;
        }

        public void AddClient(ClientDto clientDto)
        {
            var client = _repository.Get<ClientEntity>()
                .FirstOrDefault(
                    c => c.ClientName == clientDto.ClientName 
                         && c.ClientTelephone == clientDto.ClientTelephone
                         );
            if (client != null)
            {

            }
            else
                _repository.Add<ClientEntity>(_mapper.Map<ClientEntity>(clientDto));
        }

        public void ModifyClient(ClientDto clientDto)
        {
            var clientEntity = _repository.Get<ClientEntity>().ToList()
                .Find(client => client.Id == clientDto.Id);
            clientEntity.ClientName = clientDto.ClientName;
            clientEntity.ClientTelephone = clientDto.ClientTelephone;
            clientEntity.Id = clientDto.Id;
            _repository.Save();
        }

        public void RemoveClient(ClientDto clientDto)
        {
            var clientEntity = _repository.Get<ClientEntity>()
                .ToList()
                .Find(client => client.Id == clientDto.Id);
            _repository.Remove(clientEntity);
        }

        public IEnumerable<ClientDto> GetFilteredClientDto(TextFieldFilter filter, string fieldString, int page)
        {
            switch (filter)
            {
                case TextFieldFilter.ClientName:
                {
                    return GetClientsDto(page, c => c.ClientName == fieldString).ToList();

                }
                case TextFieldFilter.Telephone:
                {
                    return GetClientsDto(page, c => c.ClientTelephone == fieldString).ToList();
                }
            }

            return GetClientsDto(page);
        }
    }
}
