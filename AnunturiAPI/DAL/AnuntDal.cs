using Domain;
using Domain.Abstraction;
using Domain.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL
{
    public class AnuntDal : GenericRepository<Anunt>
    {
        public AnuntDal(ApplicationDbContext context) : base(context)
        {

        }

        public IList<AnuntInfo> GetAnunturiForRole(string role)
        {
            var date = DateTime.Now.AddHours(-24);

            var result = _context.Anunturi.AsNoTracking()
                .Where(x => x.Role.Name == role && x.Created > date)
                .OrderByDescending(x => x.Created)
                .Select(x => new AnuntInfo()
                {
                    Id = x.Id,
                    Subject = x.Subject
                })
                .ToList();

            return result;
        }

        public Anunt GetAnuntWithRole(int anuntId)
        {
            var result = _context.Anunturi.AsNoTracking()
                .Where(x => x.Id == anuntId)
                .Include(x => x.Role)
                .FirstOrDefault();

            return result;
        }
    }
}
