using EcomWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace EcomWeb.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected MyDbContext _context { get; set; }
        public RepositoryBase(MyDbContext context)
        {
            _context = context;
        }

        public virtual IQueryable<T> GetAll()
        {
            return _context.Set<T>();
        }

        public virtual T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public virtual IQueryable<T> GetByCondition(System.Linq.Expressions.Expression<Func<T, bool>> condition)
        {
            return _context.Set<T>().Where(condition);
        }

        public virtual void Create(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            var entry = _context.Entry(entity);
            entry.State = EntityState.Detached;

            _context.Set<T>().Update(entity);
            _context.SaveChanges();
        }
    }
}
