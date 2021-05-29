using Blazored.LocalStorage;
using GeoCubed.SquidLeague4.Website.Auth;
using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.Services;
using GeoCubed.SquidLeague4.Website.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            builder.Services.AddBlazoredLocalStorage();

            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

            builder.Services.AddHttpClient<IClient, Client>(client => client.BaseAddress = new Uri("https://localhost/geocubed/"));

            builder.Services.AddScoped<IPlayerDataService, PlayerDataService>();
            builder.Services.AddScoped<ITeamDataService, TeamDataService>();
            builder.Services.AddScoped<ICasterDataService, CasterDataService>();
            builder.Services.AddScoped<IHelpfulPersonDataService, HelpfulPersonDataService>();
            builder.Services.AddScoped<IMatchDataService, MatchDataService>();
            builder.Services.AddScoped<ISwissDataService, SwissDataService>();
            builder.Services.AddScoped<IGameDataService, GameDataService>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<ISystemSwitchDataService, SystemSwitchDataService>();

            await builder.Build().RunAsync();
        }
    }
}
