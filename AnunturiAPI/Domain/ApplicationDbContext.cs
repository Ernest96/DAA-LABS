using Domain.Domain;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace Domain
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Anunt> Anunturi { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public ApplicationDbContext()
            : base("Data Source=DESKTOP-GAN2GTI\\SQLEXPRESS;uid=sa;pwd=ernest123;Initial Catalog=Anunturi", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
