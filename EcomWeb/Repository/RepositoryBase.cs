using EcomWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace EcomWeb.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly MyDbContext _context;

        public RepositoryBase(MyDbContext context)
        {
            _context = context;
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetByCondition(System.Linq.Expressions.Expression<Func<T, bool>> condition)
        {
            return await _context.Set<T>().Where(condition).ToListAsync();
        }

        public virtual async Task Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async Task Update(T entity)
        {
            //var entry = _context.Entry(entity);
            //entry.State = EntityState.Detached;

            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
