using EcomWeb.Dtos.User;
using EcomWeb.Models;
using System.Linq.Expressions;

namespace EcomWeb.Repository
{
    public interface IRepositoryBase<T>
    {
        Task <IEnumerable<T>> GetAll();
        Task <T> GetById(int id);
        Task <IEnumerable<T>> GetByCondition(Expression<Func<T, bool>> condition);
        Task Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
