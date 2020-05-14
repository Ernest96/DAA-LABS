using Domain;
using Domain.Abstraction;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserDal : GenericRepository<IdentityUser>
    {
        public UserDal(ApplicationDbContext dbContext) : base(dbContext)
        {

        }

        public IList<UserInfo> GetUsers()
        {
            var usersWithRoles = (from user in _context.Users
                                  select new
                                  {
                                      UserId = user.Id,
                                      Username = user.UserName,
                                      Email = user.Email,
                                      RoleName = (from userRole in user.Roles
                                                   join role in _context.Roles on userRole.RoleId
                                                   equals role.Id
                                                   select role.Name).FirstOrDefault()
                                  }).ToList().Select(p => new UserInfo()

                                  {
                                      UserId = p.UserId,
                                      Username = p.Username,
                                      Email = p.Email,
                                      RoleName = p.RoleName
                                  }).ToList();

            return usersWithRoles;
        }

        public UserInfo GetUserInfo(string userId)
        {
            var userWithRole= (from user in _context.Users
                               where user.Id == userId
                               select new
                                  {
                                      UserId = user.Id,
                                      Username = user.UserName,
                                      Email = user.Email,
                                      RoleName = (from userRole in user.Roles
                                                  join role in _context.Roles on userRole.RoleId
                                                  equals role.Id
                                                  select role.Name).FirstOrDefault()
                                  }).ToList().Select(p => new UserInfo()
                                  {
                                      UserId = p.UserId,
                                      Username = p.Username,
                                      Email = p.Email,
                                      RoleName = p.RoleName
                                  }).FirstOrDefault();

            return userWithRole;
        }
    }
}
