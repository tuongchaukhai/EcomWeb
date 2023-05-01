using EcomWeb.Dtos.User;
using EcomWeb.Models;
using System.Linq.Expressions;

namespace EcomWeb.Services
{
    public interface IUserService
    {
        IQueryable<User> GetAll();
        User GetById(int id);
        IQueryable<User> GetByRole(string roleName);
        User Create(User user);
        User Update(User user);
        void Delete(User user);
    }
}
