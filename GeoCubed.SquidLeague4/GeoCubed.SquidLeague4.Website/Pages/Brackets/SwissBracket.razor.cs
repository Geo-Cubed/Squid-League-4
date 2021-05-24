using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.ViewModels.SwissMatches;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Pages.Brackets
{
    partial class SwissBracket
    {
        [Inject]
        public ISwissDataService SwissDataService { get; set; }

        public IEnumerable<SwissMatchDetailsViewModel> SwissMatches { get; set; }
            = new List<SwissMatchDetailsViewModel>();

        public IEnumerable<SwissStandingsViewModel> SwissStandings { get; set; }
            = new List<SwissStandingsViewModel>();

        protected override async Task OnInitializedAsync()
        {
            this.SwissMatches = await this.SwissDataService.GetAllSwissMatches();
            await this.CreateStandings();
        }

        private Task CreateStandings()
        {
            var matches = this.SwissMatches.Select(s => s.Match);
            var teams = matches.Select(m => m.AwayTeam).ToList();
            teams.AddRange(matches.Select(m => m.HomeTeam));
            teams = teams.Distinct().ToList();

            var standings = new List<SwissStandingsViewModel>();
            foreach (var team in teams)
            {
                standings.Add(new SwissStandingsViewModel()
                {
                    TeamName = team,
                    Wins = matches.Where(x => (x.HomeTeam == team && x.HomeTeamScore == 5) || (x.AwayTeam == team && x.AwayTeamScore == 5)).Count(),
                    Losses = matches.Where(x => (x.HomeTeam == team && x.AwayTeamScore == 5) || (x.AwayTeam == team && x.HomeTeamScore == 5)).Count(),
                    Points = matches.Where(x => x.HomeTeam == team).Sum(x => x.HomeTeamScore) + matches.Where(x => x.AwayTeam == team).Sum(x => x.AwayTeamScore)
                });
            }

            var standingsOrdered = standings.OrderByDescending(x => x.Points).ThenByDescending(x => x.Wins).ThenBy(x => x.Losses);
            var counter = 1;
            foreach (var team in standingsOrdered)
            {
                team.Position = counter;
                ++counter;
            }

            this.SwissStandings = standingsOrdered;
            return Task.CompletedTask;
        }
    }
}
