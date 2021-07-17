using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.ViewModels.Matches;
using GeoCubed.SquidLeague4.Website.ViewModels.Results;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
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

        [Inject]
        private NavigationManager navigationManager { get; set; }

        [Parameter]
        public int id { get; set; }

        protected List<MatchInfoViewModel> matches { get; set; }
            = new List<MatchInfoViewModel>();

        protected MatchInfoViewModel selectedMatch { get; set; }

        protected List<SetInformationViewModel> setInformation { get; set; }
            = new List<SetInformationViewModel>();

        protected override async Task OnInitializedAsync()
        {
            this.matches = await this.matchDataService.GetMatchInfo();
            this.selectedMatch = this.matches.FirstOrDefault(x => x.MatchId == id);
            if (this.selectedMatch != null)
            {
                await this.GetMatchGames();
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            this.selectedMatch = this.matches.FirstOrDefault(x => x.MatchId == id);
            if (this.selectedMatch != null)
            {
                await this.GetMatchGames();
            }
        }

        protected async Task OnMatchSelectAsync(ChangeEventArgs e)
        {
            if (!int.TryParse(e.Value.ToString(), out int matchId))
            {
                this.selectedMatch = null;
                return;
            }

            if (matchId <= 0)
            {
                this.selectedMatch = null;
                return;
            }

            this.navigationManager.NavigateTo($"matches/{matchId}");
        }

        protected async Task GetMatchGames()
        {
            if (selectedMatch == null)
            {
                return;
            }

            try
            {
                this.setInformation = await this.gameDataService.GetSetInformation(this.selectedMatch.MatchId);
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

        protected string GetHeadderText()
        {
            if (this.selectedMatch == null)
            {
                return string.Empty;
            }

            var baseStr = (string.IsNullOrEmpty(this.selectedMatch.Caster)) ? string.Empty : string.Format("Casted by: {0}", this.selectedMatch.Caster);
            baseStr = (string.IsNullOrEmpty(this.selectedMatch.CoCaster)) ? baseStr : string.Format("{0} and {1}", baseStr, this.selectedMatch.CoCaster);
            return (this.selectedMatch.MatchDate.HasValue && this.selectedMatch.MatchDate.Value > new DateTime(2021, 1, 1)) ? string.Format("{0} {1} BST", baseStr, this.selectedMatch.MatchDate.Value) : baseStr;
        }
    }
}
