using AutoMapper;
using Clean_Architecture.Model.Dto.AccountClient;
using Clean_Architecture.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Service.AccountClient
{
    public class AccountClientService : IAccountClientService
    {
        private readonly IGenericRepository<Clean_Architecture.Model.Entities.AccountClient> _repository;
        private readonly IMapper _mapper;
        public AccountClientService(IGenericRepository<Clean_Architecture.Model.Entities.AccountClient> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        //public IEnumerable<AccountClientDto> Search(string key)
        //{
        //    return _mapper.Map<List<AccountClientDto>>(_repository.GetAll().OrderByDescending(x => x.Id).Where(x => x.CategoryName.Contains(key)));
        //}
        public bool Add(AccountClientDto accountClientDto)
        {
            return _repository.Add(_mapper.Map<Clean_Architecture.Model.Entities.AccountClient>(accountClientDto));
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }

        public IEnumerable<AccountClientDto> GetAll()
        {
            return _mapper.Map<List<AccountClientDto>>(_repository.GetAll().OrderByDescending(x => x.Id));
        }

        public AccountClientDto GetById(int id)
        {
            return _mapper.Map<AccountClientDto>(_repository.GetbyId(id));
        }

        public bool Update(AccountClientDto accountClientDto)
        {
            return _repository.Update(_mapper.Map<Clean_Architecture.Model.Entities.AccountClient>(accountClientDto));
        }
    }
}
