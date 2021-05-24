using GeoCubed.SquidLeague4.Identity.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GeoCubed.SquidLeague4.Identity
{
    public class SquidLeague4IdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public SquidLeague4IdentityDbContext(DbContextOptions<SquidLeague4IdentityDbContext> options)
            :base (options)
        {
        }
    }
}
