using GeoCubed.SquidLeague4.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GeoCubed.SquidLeague4.Persistence
{
    public class SquidLeagueDbContext : DbContext
    {
        public SquidLeagueDbContext(DbContextOptions<SquidLeagueDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Audit> Audits { get; set; }
        public virtual DbSet<BracketKnockout> BracketKnockouts { get; set; }
        public virtual DbSet<BracketSwiss> BracketSwisses { get; set; }
        public virtual DbSet<CasterProfile> CasterProfiles { get; set; }
        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<GameMap> GameMaps { get; set; }
        public virtual DbSet<GameMode> GameModes { get; set; }
        public virtual DbSet<GameSetting> GameSettings { get; set; }
        public virtual DbSet<HelpfulPerson> HelpfulPeople { get; set; }
        public virtual DbSet<Match> Matches { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<SystemSwitch> SystemSwitches { get; set; }
        public virtual DbSet<Team> Teams { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Weapon> Weapons { get; set; }
        public virtual DbSet<WeaponPlayed> WeaponPlayeds { get; set; }
        public virtual DbSet<WeaponSpecial> WeaponSpecials { get; set; }
        public virtual DbSet<WeaponSub> WeaponSubs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("Name=ConnectionStrings:squidLeagueDb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SquidLeagueDbContext).Assembly);
        }
    }
}
