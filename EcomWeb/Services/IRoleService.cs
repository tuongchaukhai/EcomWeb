using EcomWeb.Models;

namespace EcomWeb.Services
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetAll();
        Task<Role> GetById(int id);
        Task<Role> Create(Role role);
        Task<Role> Update(Role role);
        void Delete(Role role);
    }
}
