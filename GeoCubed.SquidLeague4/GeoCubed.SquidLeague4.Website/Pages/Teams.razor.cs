using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.ViewModels.Teams;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Pages
{
    partial class Teams
    {
        [Inject]
        public ITeamDataService TeamDataService { get; set; }

        [Inject]
        public IMatchDataService MatchDataService { get; set; }

        public IEnumerable<BasicTeamViewModel> ActiveTeams { get; set; }
            = new List<BasicTeamViewModel>();

        public IEnumerable<TeamMatchViewModel> TeamMatches { get; set; }
            = new List<TeamMatchViewModel>();

        public int SelectedTeamId { get; set; }

        public TeamDetailViewModel SelectedTeam { get; set; }

        protected async override Task OnInitializedAsync()
        {
            this.SelectedTeam = null;
            this.SelectedTeamId = 0;
            this.ActiveTeams = (await this.TeamDataService.GetActiveTeams()).OrderBy(x => x.TeamName);
        }

        protected async Task OnTeamSelectAsync(ChangeEventArgs e)
        {
            // Flush team games so that it doesn't display another teams games while loading.
            this.TeamMatches = new List<TeamMatchViewModel>();
            if (!int.TryParse(e.Value.ToString(), out int teamId))
            {
                this.SelectedTeam = null;
                return;
            }

            this.SelectedTeamId = teamId;
            this.SelectedTeam = await this.TeamDataService.GetAllTeamsWithPlayers(this.SelectedTeamId);
            // Load team games
            this.TeamMatches = await this.MatchDataService.GetTemMatches(this.SelectedTeamId);
        }
    }
}
