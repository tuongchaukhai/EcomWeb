using EcomWeb.Dtos.User;
using EcomWeb.Models;
using System.Linq.Expressions;

namespace EcomWeb.Repository
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> GetAll();
        T GetById(int id);
        IQueryable<T> GetByCondition(Expression<Func<T, bool>> condition);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
