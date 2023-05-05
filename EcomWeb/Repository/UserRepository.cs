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

        public override async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.Include(b => b.Role).ToListAsync();
        }

        public async Task<User> SearchByEmail(string email)
        {
            return await _context.Users.Where(b=>b.Email == email).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetByRole(string role)
        {
            return await _context.Users
                .Include(b => b.Role)
                .Where(b => b.Role.RoleName == role).ToListAsync();
        }
    }
}
