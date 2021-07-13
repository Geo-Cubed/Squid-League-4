using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.ViewModels;
using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using GeoCubed.SquidLeague4.Website.ViewModels.GameSettings;
using GeoCubed.SquidLeague4.Website.ViewModels.Matches;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Pages.Admin
{
    public partial class AdminMatchResults
    {
        [Inject]
        private IMatchDataService matchDataService { get; set; }

        [Inject]
        private IGameSettingDataService gameSettingDataService { get; set; }

        [Inject]
        private IPlayerDataService playerDataService { get; set; }

        [Inject]
        private IWeaponDataService weaponDataService { get; set; }

        [Inject]
        private IGameDataService gameDataService { get; set; }

        protected BasicMatchInfo selectedMatch { get; set; }

        protected List<AdminResultsModel> setInformation { get; set; }
            = new List<AdminResultsModel>();

        protected IEnumerable<BasicMatchInfo> matches { get; set; }
            = new List<BasicMatchInfo>();

        protected IEnumerable<BasicWeaponInfo> allWeapons { get; set; }
            = new List<BasicWeaponInfo>();

        protected IEnumerable<AdminGameFullInfoViewModel> games { get; set; }
            = new List<AdminGameFullInfoViewModel>();

        protected IEnumerable<AdminPlayerViewModel> homeTeamPlayers { get; set; }
            = new List<AdminPlayerViewModel>();

        protected IEnumerable<AdminPlayerViewModel> awayTeamPlayers { get; set; }
            = new List<AdminPlayerViewModel>();

        protected override async Task OnInitializedAsync()
        {
            this.matches = await this.matchDataService.GetBasicMatchInfo();
            this.allWeapons = (await this.weaponDataService.GetBasicWeaponInfo()).OrderBy(x => x.Name);
        }

        protected async Task OnMatchSelectAsync(ChangeEventArgs e)
        {
            if (!int.TryParse(e.Value.ToString(), out int MatchId))
            {
                return;
            }

            this.selectedMatch = this.matches.FirstOrDefault(x => x.Id == MatchId);
            try
            {
                this.homeTeamPlayers = await this.playerDataService.GetPlayersByTeamId(this.selectedMatch.HomeTeamId);
                this.awayTeamPlayers = await this.playerDataService.GetPlayersByTeamId(this.selectedMatch.AwayTeamId);
                this.setInformation = await this.gameDataService.GetResultsInfo(this.selectedMatch.Id);
            }
            catch
            {
                this.homeTeamPlayers = new List<AdminPlayerViewModel>();
                this.awayTeamPlayers = new List<AdminPlayerViewModel>();
                this.setInformation = new List<AdminResultsModel>();
            }
        }

        protected string GetMatchText(string HomeTeam, string AwayTeam)
        {
            return string.Format("{0} vs. {1}", HomeTeam, AwayTeam);
        }

        protected async Task SaveGameInformationAsync(AdminResultsModel gameInfo)
        {
            // Call the save method.
            // If method was success - Reload match.
            // Else display message.
        }

        protected async Task DeleteGameInformationAsync(int gameId)
        {
            // Call the delete method.
            // If metho was success - Reload match.
            // Else display message.
        }
    }
}
