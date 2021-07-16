using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.ViewModels.Matches;
using GeoCubed.SquidLeague4.Website.ViewModels.Results;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Pages
{
    public partial class Matches
    {
        [Inject]
        private IGameDataService gameDataService { get; set; }

        [Inject]
        private IMatchDataService matchDataService { get; set; }

        protected List<MatchInfoViewModel> matches { get; set; }

        protected MatchInfoViewModel selectedMatch { get; set; }

        protected List<SetInformationViewModel> setInformation { get; set; }
            = new List<SetInformationViewModel>();

        protected override async Task OnInitializedAsync()
        {
            /* TODO:
             * Use nswag.
             * Get list of matches.
             * Try make the ui moblie friendly.
             *
             */
        }

        protected async Task OnMatchSelectAsync(ChangeEventArgs e)
        {
            if (!int.TryParse(e.Value.ToString(), out int matchId))
            {
                this.selectedMatch = null;
                return;
            }

            this.selectedMatch = this.matches.FirstOrDefault(x => x.MatchId == matchId);
            try
            {
                this.setInformation = await this.gameDataService.GetSetInformation(matchId);
            }
            catch
            {
                this.setInformation = new List<SetInformationViewModel>();
            }
        }

        protected string GenerateWinnerText()
        {
            if (this.selectedMatch == null || this.selectedMatch.Winner == "none")
            {
                return string.Empty;
            }

            if (selectedMatch.Winner == "home")
            {
                return string.Format(
                    "Winner: {0} {1} - {2}", 
                    this.selectedMatch.HomeTeamName, 
                    this.selectedMatch.HomeTeamScore, 
                    this.selectedMatch.AwayTeamScore);
            }

            return string.Format(
                "Winner: {0} {1} - {2}",
                this.selectedMatch.AwayTeamName,
                this.selectedMatch.AwayTeamScore,
                this.selectedMatch.HomeTeamScore);
        }

        protected string GetMatchText(string homeTeam, string awayTeam)
        {
            return string.Format("{0} vs. {1}", homeTeam, awayTeam);
        }
    }
}
