using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TBC.Capital.Dal.Models
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            CreateDate = DateTime.Now;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            return userIdentity;
        }
        public DateTime CreateDate { get; private set; }
        public string FullName { get; set; }
        public string ProfileImage { get; set; }
    }
}
