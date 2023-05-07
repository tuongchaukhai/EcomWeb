﻿using EcomWeb.Models;
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

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _userRepository.GetAll();
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
            if (_userRepository.SearchByEmail(user.Email).Result == null)
                return null;

            await _userRepository.Update(user);
            return user;
        }
    }
}
