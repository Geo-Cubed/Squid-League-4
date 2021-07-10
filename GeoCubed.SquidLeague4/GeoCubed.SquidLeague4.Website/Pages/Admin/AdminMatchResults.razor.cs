﻿using GeoCubed.SquidLeague4.Website.Interfaces;
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

        protected BasicMatchInfo selectedMatch { get; set; }

        protected IEnumerable<BasicMatchInfo> matches { get; set; }
            = new List<BasicMatchInfo>();

        protected IEnumerable<AdminGameFullInfoViewModel> games { get; set; }
            = new List<AdminGameFullInfoViewModel>();

        protected IEnumerable<MatchMapListViewModel> maps { get; set; }
            = new List<MatchMapListViewModel>();

        protected IEnumerable<AdminPlayerViewModel> homeTeamPlayers { get; set; }
            = new List<AdminPlayerViewModel>();

        protected IEnumerable<AdminPlayerViewModel> awayTeamPlayers { get; set; }
            = new List<AdminPlayerViewModel>();

        protected override async Task OnInitializedAsync()
        {
            this.matches = await this.matchDataService.GetBasicMatchInfo();
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
                this.maps = await this.gameSettingDataService.GetMapListByMatchId(MatchId);
                this.homeTeamPlayers = await this.playerDataService.GetPlayersByTeamId(this.selectedMatch.HomeTeamId);
                this.awayTeamPlayers = await this.playerDataService.GetPlayersByTeamId(this.selectedMatch.AwayTeamId);
            }
            catch
            {
                this.maps = new List<MatchMapListViewModel>();
                this.homeTeamPlayers = new List<AdminPlayerViewModel>();
                this.awayTeamPlayers = new List<AdminPlayerViewModel>();
            }
        }

        protected string GetMatchText(string HomeTeam, string AwayTeam)
        {
            return string.Format("{0} vs. {1}", HomeTeam, AwayTeam);
        }
    }
}
