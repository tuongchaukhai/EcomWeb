using EcomWeb.Dtos.User;
using EcomWeb.Models;
using System.Linq.Expressions;

namespace EcomWeb.Repository
{
    public class UserRepository : IUserRepository
    {
        private MyDbContext _context;
        public UserRepository(MyDbContext context)
        {
            _context = context;
        }

        public void Create(UserAddDto userDto)
        {
            var newUser = new User
            {
                Email = userDto.Email,
                Password = userDto.Password,
                FullName = userDto.FullName,
                Active = userDto.Active,
                RoleId = userDto.RoleId,
                CreatedDate = DateTime.Now
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();

        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<User> GetByCondition(Expression<Func<User, bool>> condition)
        {
            throw new NotImplementedException();
        }

        public void Update(UserUpdateDto user)
        {
            throw new NotImplementedException();
        }
    }
}
