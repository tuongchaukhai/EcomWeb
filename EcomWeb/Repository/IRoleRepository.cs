using EcomWeb.Models;

namespace EcomWeb.Repository
{
    public interface IRoleRepository : IRepositoryBase<Role>
    {
        Task<Role> GetByRoleName(string roleName);
    }
}
