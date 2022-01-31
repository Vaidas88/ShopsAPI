using Microsoft.EntityFrameworkCore;
using ShopsAPI.Controllers.Data;
using ShopsAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopsAPI.Repositories
{
    public abstract class GenericRepo<T> where T : Entity
    {
        internal DataContext _context;
        internal DbSet<T> _dbSet;

        public GenericRepo(DataContext dataContext)
        {
            _context = dataContext;
            _dbSet = _context.Set<T>();
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<T> FindByNameAsync(string name)
        {
            return await _dbSet.Where(entity => entity.Name == name).SingleOrDefaultAsync();
        }

        public virtual async Task CreateAsync(T entity)
        {
            _dbSet.Add(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
