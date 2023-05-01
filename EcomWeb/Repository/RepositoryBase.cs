using EcomWeb.Models;

namespace EcomWeb.Repository
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected MyDbContext _context { get; set; }
        public RepositoryBase(MyDbContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll()
        {
            return _context.Set<T>().
        }

        public IQueryable<T> GetByCondition(System.Linq.Expressions.Expression<Func<T, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
