namespace TBC.Capital.Dal.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TBC.Capital.Dal.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TBC.Capital.Dal.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "TBC.Capital.Dal.Models.ApplicationDbContext";
        }

        protected override void Seed(TBC.Capital.Dal.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Language.AddOrUpdate(
                 p => p.Code,
                 new Language { Code = "ka", Name = "ქართული" },
                 new Language { Code = "en", Name = "English" }
               );
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.UserName == "test@test.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "test@test.com" };

                manager.Create(user, "testtest");
                manager.AddToRole(user.Id, "Admin");
            }
        }
    }
}
