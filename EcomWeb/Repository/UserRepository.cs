using EcomWeb.Controllers;
using EcomWeb.Dtos.Product;
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

        public async Task<UsersPage> GetAll(int page = 1, int pageSize = 10)
        {
            var users = await _context.Users.Include(b => b.Role).ToListAsync();

            int totalRecords = users.Count();

            users = users.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            return new UsersPage { Users = users, TotalRecords = totalRecords };
        }
    }
}
