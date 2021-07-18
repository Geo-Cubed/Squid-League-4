using GeoCubed.SquidLeague4.Identity.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.CubedAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                    // Make sure the roles are created.
                    await Identity.RootUser.CreateRootRoles.AddBaseRoles(roleManager);

#if DEBUG
    // Create a root user and assign admin so I can create my user account before deleting.
    //await Identity.RootUser.CreateRootUser.SeedAsync(userManager);
    //await Identity.RootUser.CreateRootRoles.AddUserToRolesync(userManager, "slRoot", "Admin");
    //await Identity.RootUser.CreateRootRoles.AddUserToRolesync(userManager, "GeoCubed", "Admin");
#endif
                }
                catch (Exception ex)
                {
                    // TODO: Log this.
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
