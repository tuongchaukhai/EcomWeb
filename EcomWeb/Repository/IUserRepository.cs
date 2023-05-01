using EcomWeb.Dtos.User;
using EcomWeb.Models;
using System.Diagnostics.Eventing.Reader;
using System.Linq.Expressions;

namespace EcomWeb.Repository
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        User SearchByEmail(string email);

        IQueryable<User> GetByRole(string role);
    }
}
