using EcomWeb.Dtos.User;
using EcomWeb.Models;
using System.Linq.Expressions;

namespace EcomWeb.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(int id);
        Task<IEnumerable<User>> GetByRole(string roleName);
        Task<User> Create(User user);
        Task<User> Update(User user);
        Task<bool> Delete(User user);
    }
}
