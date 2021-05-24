using GeoCubed.SquidLeague4.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Identity.RootUser
{
    public static class CreateRootUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager)
        {
            var applicationUser = new ApplicationUser
            {
                UserName = "slRoot"
            };

            var user = await userManager.FindByNameAsync(applicationUser.UserName);
            if (user == null)
            {
                // Setting is for debugging purposes only.
                var result = await userManager.CreateAsync(applicationUser, "slTest1!");
            }
        }
    }
}
