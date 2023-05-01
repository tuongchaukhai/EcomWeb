using EcomWeb.Models;

namespace EcomWeb.Repository
{
    public interface IRoleRepository : IRepositoryBase<Role>
    {
        Role GetByRoleName(string roleName);
    }
}
