using GeoCubed.SquidLeague4.Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Identity.RootUser
{
    public static class CreateRootRoles
    {
        public static async Task AddBaseRoles(RoleManager<IdentityRole> roleManager)
        {
            var baseRoles = new List<string>() { "Basic", "Moderator", "Admin" };
            foreach (var role in baseRoles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }

        public static async Task AddUserToRolesync(UserManager<ApplicationUser> userManager, string username, string role)
        {
            var user = await userManager.FindByNameAsync(username);
            if (user == null)
            {
                return;
            }

            var currentRoles = await userManager.GetRolesAsync(user);
            if (!currentRoles.Contains(role))
            {
                try
                {
                    await userManager.AddToRoleAsync(user, role);
                }
                catch
                {
                    // TODO: Log this.
                }
            }
        }
    }
}
