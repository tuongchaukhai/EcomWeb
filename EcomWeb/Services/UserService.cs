using EcomWeb.Dtos.User;
using EcomWeb.Models;
using EcomWeb.Repository;

namespace EcomWeb.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Create(User user)
        {
            if (_userRepository.SearchByEmail(user.Email).Result != null) //exist
                return null;

            user.CreatedDate = DateTime.Now; //Will add trigger in the future

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            await _userRepository.Create(user);

            return user;
        }

        public async Task<bool> Delete(User user)
        {
            if (GetById(user.UserId) == null)
                return false;

            await _userRepository.Delete(user);
            return true;
        }

        public async Task<UsersPage> GetAll(int page = 1, int pageSize = 10)
        {
            return await _userRepository.GetAll(page, pageSize);
        }

        public async Task<User> GetById(int id)
        {
            return await _userRepository.GetById(id);
        }

        public async Task<IEnumerable<User>> GetByRole(string roleName)
        {
            return await _userRepository.GetByRole(roleName);
        }

        public async Task<User> Update(User user)
        { 
            await _userRepository.Update(user);
            return user;
        }
    }
}
