using EcomWeb.Dtos.User;
using EcomWeb.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EcomWeb.Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(MyDbContext context) : base(context)
        {
        }

        public User SearchByEmail(string email)
        {
            return GetByCondition(b => b.Email == email).FirstOrDefault();
        }

        public IQueryable<User> GetByRole(string role)
        {
            return _context.Users
                .Include(b => b.Role)
                .Where(b => b.Role.RoleName == role);
        }
    }
}
