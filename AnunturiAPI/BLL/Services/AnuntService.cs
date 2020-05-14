using BLL.DTO;
using DAL;
using Domain;
using Domain.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AnuntService
    {
        private readonly IGenericRepository<Anunt> _anuntRepo;
        private readonly AnuntDal _anuntDal;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AnuntService(ApplicationDbContext dbContext)
        {
            var roleStore = new RoleStore<IdentityRole>(dbContext);

            _roleManager = new RoleManager<IdentityRole>(roleStore);
            _anuntDal = new AnuntDal(dbContext);
            _anuntRepo = new GenericRepository<Anunt>(dbContext);
        }

        public int AddAnunt(AnuntDto dto)
        {
            var role = _roleManager.Roles.Where(x => x.Name == dto.Role).FirstOrDefault();

            var anunt = new Anunt()
            {
                Content = dto.Content,
                Subject = dto.Subject,
                Role = role,
                Created = DateTime.Now
            };

            _anuntRepo.Insert(anunt);

            _anuntRepo.Save();

            return anunt.Id;
        }

        public IList<AnuntInfoDto> GetAnunturiForRole(string role)
        {
            var result = new List<AnuntInfoDto>();
            var anunturi = _anuntDal.GetAnunturiForRole(role);

            foreach(var anunt in anunturi)
            {
                result.Add(new AnuntInfoDto()
                {
                    Id = anunt.Id,
                    Subject = anunt.Subject
                });
            }

            return result;
        }

        public AnuntDto GetAnuntWithRole(int anuntId)
        {
            var anunt = _anuntDal.GetAnuntWithRole(anuntId);

            var result = new AnuntDto()
            {
                Content = anunt.Content,
                Id = anunt.Id,
                Subject = anunt.Subject,
                Role =  anunt.Role.Name
            };

            return result;
        }

    }
}
