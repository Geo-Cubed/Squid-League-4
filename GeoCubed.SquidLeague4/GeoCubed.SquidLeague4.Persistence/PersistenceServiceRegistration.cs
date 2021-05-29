using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GeoCubed.SquidLeague4.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SquidLeagueDbContext>(options =>
                options.UseMySQL(configuration.GetConnectionString("SquidLeague4ConnectionString")));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<IHelpfulPersonRepository, HelpfulPersonRepository>();
            services.AddScoped<ICasterRepository, CasterRepository>();
            services.AddScoped<IWeaponRepository, WeaponRepository>();
            services.AddScoped<IMatchRepository, MatchRepository>();
            services.AddScoped<ISwissMatchRepository, SwissMatchRepository>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IGameSettingRepository, GameSettingRepository>();
            services.AddScoped<ISystemSwitchRepository, SystemSwitchRepository>();
            services.AddScoped<IMapRepository, MapRepository>();
            services.AddScoped<IModeRepository, ModeRepository>();

            return services;
        }
    }
}
