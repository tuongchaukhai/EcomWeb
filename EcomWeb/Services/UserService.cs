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

        public User Create(User user)
        {
            if (_userRepository.SearchByEmail(user.Email) == null)
                return null;

            _userRepository.Create(user);

            return user;
        }

        public void Delete(User user)
        {
            _userRepository.Delete(user);
        }

        public IQueryable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User GetById(int id)
        {
            return _userRepository.GetById(id);
        }

        public IQueryable<User> GetByRole(string roleName)
        {
            return _userRepository.GetByRole(roleName);
        }

        public User Update(User user)
        {   
            if (_userRepository.SearchByEmail(user.Email) == null)
                return null;

            _userRepository.Update(user);

            return user;
        }
    }
}
