using AnunturiAPI.Constants;
using AnunturiAPI.Filters;
using BLL.DTO;
using BLL.Services;
using Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.ApplicationServices;
using System.Web.Http;

namespace AnunturiAPI.Controllers
{

    [APIKeyFilter]
    [RoutePrefix("api/Users")]
    public class UsersController : ApiController
    {
        private readonly UserRoleService _roleService;
        private readonly ApplicationUserManager _userManager;
        private readonly UserService _userService;

        public UsersController()
        {
            var dbContext = new ApplicationDbContext();
            var store = new UserStore<ApplicationUser>(dbContext);
            _userManager = new ApplicationUserManager(store);
            _roleService = new UserRoleService(dbContext);
            _userService = new UserService(dbContext);
        }

        [Route("GetRoles")]
        [Authorize(Roles = "ADMIN")]

        [IPFilter]
        public IEnumerable<RoleDto> GetRoles()
        {
           return _roleService.GetAllRoles();
        }

        [Route("GetUsers")]
        [IPFilter]
        public IEnumerable<UserDto> GetUsers()
        {
            return _userService.GetUsers();
        }

        [Route("GetUserInfo")]
        [Authorize]
        public UserDto GetUserInfo()
        {
            var userId = User.Identity.GetUserId();
            var userDto = _userService.GetUser(userId);

            return userDto;
        }

        [Route("Delete")]
        [Authorize(Roles = "ADMIN")]
        [IPFilter]
        [HttpPost]
        public async  Task<IHttpActionResult> Delete(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                if (await _userManager.IsInRoleAsync(user.Id, RoleConsts.ADMIN))
                {
                    return BadRequest("Can't delete admin");
                }

                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(string.Join(", ", result.Errors));
                }
            }

            return Ok();
        }
    }
}
