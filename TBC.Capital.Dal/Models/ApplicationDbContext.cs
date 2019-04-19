using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using TBC.Capital.Dal.Models.Content;


namespace TBC.Capital.Dal.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Language> Language { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }


        public ApplicationDbContext() : base("DefaultConnection", throwIfV1Schema: false) { }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
