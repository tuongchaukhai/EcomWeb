using EcomWeb.Dtos.User;
using EcomWeb.Models;
using System.Diagnostics.Eventing.Reader;
using System.Linq.Expressions;

namespace EcomWeb.Repository
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        new Task<IEnumerable<User>> GetAll();

        Task<User> SearchByEmail(string email);

        Task<IEnumerable<User>> GetByRole(string role);
    }
}
