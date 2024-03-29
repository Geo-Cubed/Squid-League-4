﻿using GeoCubed.SquidLeague4.Website.Common.Helpers;
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

        [Inject]
        private NavigationManager navigationManager { get; set; }

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
                // This is a really bad fix but with the time limits it'll do.
                if (team == "BYE")
                {
                    continue;
                }

                standings.Add(new SwissStandingsViewModel()
                {
                    TeamName = team,
                    Wins = matches.Where(x => (x.HomeTeam == team && x.Winner == "home") || (x.AwayTeam == team && x.Winner == "away")).Count(),
                    Losses = matches.Where(x => (x.HomeTeam == team && x.Winner == "away") || (x.AwayTeam == team && x.Winner == "home")).Count(),
                    Points = this.CalculatePoints(team, matches)
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

        private int CalculatePoints(string team, IEnumerable<MatchDetailsDto> matches)
        {
            var homePoints = matches.Where(x => x.HomeTeam == team).Sum(x => x.HomeTeamScore);
            var awayPoints = matches.Where(x => x.AwayTeam == team).Sum(x => x.AwayTeamScore);
            var homeNonByePoints = matches.Where(x => x.HomeTeam == team && x.AwayTeam != "BYE" && x.Winner == "home").Count();
            var awayNonByePoints = matches.Where(x => x.AwayTeam == team && x.HomeTeam != "BYE" && x.Winner == "away").Count();

            return homePoints + awayPoints + homeNonByePoints + awayNonByePoints;
        }

        protected void NavigateTo(SwissMatchDetailsViewModel match)
        {
            if (!MatchHelper.IsMatchBye(match))
            {
                this.navigationManager.NavigateTo($"matches/{match.MatchId}");
            }
        }
    }
}
