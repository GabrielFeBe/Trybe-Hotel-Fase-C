using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class UserRepository : IUserRepository
    {
        protected readonly ITrybeHotelContext _context;
        public UserRepository(ITrybeHotelContext context)
        {
            _context = context;
        }
        public UserDto GetUserById(int userId)
        {
            throw new NotImplementedException();
        }

        public UserDto Login(LoginDto login)
        {
            var firstOrDefault = _context.Users.FirstOrDefault(u => u.Email == login.Email) ?? throw new Exception("Incorrect e-mail or password");
            if (firstOrDefault.Password == login.Password)
            {
                return new UserDto
                {
                    UserId = firstOrDefault.UserId,
                    Name = firstOrDefault.Name,
                    Email = firstOrDefault.Email,
                    UserType = firstOrDefault.UserType
                };
            }
            else
            {
                throw new Exception("Incorrect e-mail or password");
            }
        }
        public UserDto Add(UserDtoInsert user)
        {
            var userEntity = new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                UserType = "client"
            };
            var firstOrDefault = _context.Users.FirstOrDefault(u => u.Email == user.Email);
            if (firstOrDefault == null)
            {
                var userAdded = _context.Users.Add(userEntity);
                _context.SaveChanges();
                return new UserDto
                {
                    UserId = userAdded.Entity.UserId,
                    Name = userAdded.Entity.Name,
                    Email = userAdded.Entity.Email,
                    UserType = userAdded.Entity.UserType
                };
            }
            else
            {

                throw new Exception("User email already exists");
            }
        }

        public UserDto GetUserByEmail(string userEmail)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserDto> GetUsers()
        {
            return _context.Users.Select(user => new UserDto
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                UserType = user.UserType
            }).ToList();
        }

    }
}