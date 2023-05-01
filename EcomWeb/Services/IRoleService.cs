using EcomWeb.Models;

namespace EcomWeb.Services
{
    public interface IRoleService
    {
        IQueryable<Role> GetAll();
        Role GetById(int id);
        Role Create(Role role);
        Role Update(Role role);
        void Delete(Role role);
    }
}
