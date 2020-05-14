using BLL.DTO;
using DAL;
using Domain;
using Domain.Domain;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace BLL.Services
{
    public class UserRoleService
    {
        private readonly IGenericRepository<IdentityRole> _roleRepo;

        public UserRoleService(ApplicationDbContext dbContext)
        {
            _roleRepo = new GenericRepository<IdentityRole>(dbContext);
        }

        public IList<RoleDto> GetAllRoles()
        {
            var roles = _roleRepo.GetAll();
            IList<RoleDto> roleDtos = new List<RoleDto>();

            foreach(var role in roles)
            {
                roleDtos.Add(new RoleDto()
                {
                    Id = role.Id,
                    Name = role.Name
                });
            }

            return roleDtos;
        }
    }
}
