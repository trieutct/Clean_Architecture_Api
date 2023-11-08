using Clean_Architecture.Model.Dto.AccountClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Service.AccountClient
{
    public interface IAccountClientService
    {
        IEnumerable<AccountClientDto> GetAll();
       // IEnumerable<AccountClientDto> Search(string key);
        AccountClientDto GetById(int id);
        bool Add(AccountClientDto accountClientDto);
        bool Update(AccountClientDto accountClientDto);
        bool Delete(int id);
    }
}
