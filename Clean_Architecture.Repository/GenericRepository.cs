using Clean_Architecture.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clean_Architecture.Repository
{
    public class GenericRepository<T>:IGenericRepository<T> where T : class,new ()
    {
        private readonly ShopDbContext _context;
        DbSet<T> _dbSet;
        public GenericRepository(ShopDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }
        public T GetbyId(int id)
        {
            return _dbSet.Find(id);
        }
        public bool Add(T entity)
        {
            if (!_dbSet.Any(e => e == entity))
            {
                _dbSet.Add(entity);
                _context.SaveChanges();
                return true;
            }
            return false;
        }
        public bool Update(T entity)
        {
            if (!_dbSet.Any(e => e == entity))
            {
                return false;
            }
            _context.Entry(entity).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return true;
        }
        public bool Delete(int id)
        {
            var category = _dbSet.Find(id);
            if (category == null)
            {
                return false;
            }
            _dbSet.Remove(category);
            _context.SaveChanges();
            return true;
        }
    }
}
