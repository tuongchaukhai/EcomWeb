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

        public async Task<Role> Create(Role role)
        {
            if (_roleRepository.GetByRoleName(role.RoleName).Result != null)
                return null;

            await _roleRepository.Create(role);

            return role;
        }

        public async void Delete(Role role)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Role>> GetAll()
        {
            return await _roleRepository.GetAll();
        }

        public async Task<Role> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Role> Update(Role role)
        {
            throw new NotImplementedException();
        }
    }
}
