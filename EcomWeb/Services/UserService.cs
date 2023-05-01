using AutoMapper;
using EcomWeb.Dtos.User;
using EcomWeb.Models;
using EcomWeb.Repository;

namespace EcomWeb.Services
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public User Create(UserAddDto userDto)
        {
            if (_userRepository.SearchByEmail(userDto.Email) == null)
                return null;

            var user = _mapper.Map<User>(userDto);
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

        public User Update(UserUpdateDto userDto)
        {   
            if (_userRepository.SearchByEmail(userDto.Email) == null)
                return null;

            var user = _mapper.Map<User>(userDto);
            _userRepository.Update(user);

            return user;
        }
    }
}
