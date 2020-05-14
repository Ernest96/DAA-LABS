using BLL.DTO;
using DAL;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService
    {
        private readonly UserDal _userDal;

        public UserService(ApplicationDbContext context)
        {
            _userDal = new UserDal(context);
        }

        public IList<UserDto> GetUsers()
        {
            var result = new List<UserDto>();

            var users = _userDal.GetUsers();

            foreach (var u in users)
            {
                result.Add(new UserDto()
                {
                    UserId = u.UserId,
                    Email = u.Email,
                    Role = u.RoleName,
                    Username = u.Username
                });
            }

            return result;
        }

        public UserDto GetUser(string userId)
        {
            var user = _userDal.GetUserInfo(userId);

            var result = new UserDto()
            {
                Email = user.Email,
                Role = user.RoleName,
                UserId = user.UserId,
                Username = user.Username
            };

            return result;
        }
    }
}
