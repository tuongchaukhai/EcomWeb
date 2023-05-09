using EcomWeb.Dtos.User;
using EcomWeb.Models;
using System.Linq.Expressions;

namespace EcomWeb.Services
{
    public interface IUserService
    {
        Task<UsersPage> GetAll(int page = 1, int pageSize = 10);
        Task<User> GetById(int id);
        Task<IEnumerable<User>> GetByRole(string roleName);
        Task<User> Create(User user);
        Task<User> Update(User user);
        Task<bool> Delete(User user);
    }
}
