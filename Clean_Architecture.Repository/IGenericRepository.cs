using Clean_Architecture.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Repository
{
    public interface IGenericRepository<T> where T : class,new ()
    {
        IEnumerable<T> GetAll();
        T GetbyId(int id);
        bool Add(T entity);
        bool Update(T entity);
        bool Delete(int id);
    }
}
