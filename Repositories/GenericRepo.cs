using Microsoft.EntityFrameworkCore;
using ShopsAPI.Controllers.Data;
using System.Collections.Generic;
using System.Linq;

namespace ShopsAPI.Repositories
{
    public abstract class GenericRepo<T> where T : class
    {
        internal DataContext _context;
        internal DbSet<T> _dbSet;

        public GenericRepo(DataContext dataContext)
        {
            _context = dataContext;
            _dbSet = _context.Set<T>();
        }

        public virtual List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Create(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
