using EcomWeb.Models;
using EcomWeb.Repository;

namespace EcomWeb.Services
{
    public class RoleService : IRoleService
    {
        private IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public Role Create(Role role)
        {
            if (_roleRepository.GetByRoleName(role.RoleName) != null)
                return null;

            _roleRepository.Create(role);

            return role;
        }

        public void Delete(Role role)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Role> GetAll()
        {
            return _roleRepository.GetAll();
        }

        public Role GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Role Update(Role role)
        {
            throw new NotImplementedException();
        }
    }
}
