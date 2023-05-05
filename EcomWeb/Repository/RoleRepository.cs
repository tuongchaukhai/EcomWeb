using EcomWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace EcomWeb.Repository
{
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(MyDbContext context) : base(context) { }

        public async Task<Role> GetByRoleName(string roleName)
        {
            return await _context.Roles.Where(b => b.RoleName == roleName).FirstOrDefaultAsync();
        }
    }
}
