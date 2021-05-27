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

        public IEnumerable<TeamDetailViewModel> ActiveTeams { get; set; }
            = new List<TeamDetailViewModel>();

        public IEnumerable<TeamMatchViewModel> TeamMatches { get; set; }
            = new List<TeamMatchViewModel>();

        public int SelectedTeamId { get; set; }

        public TeamDetailViewModel SelectedTeam
        {
            get
            {
                return this.ActiveTeams.FirstOrDefault(t => t.Id == this.SelectedTeamId);
            }
        }

        protected async override Task OnInitializedAsync()
        {
            this.SelectedTeamId = 0;
            this.ActiveTeams = await this.TeamDataService.GetAllTeamsWithPlayers();
        }

        protected async Task OnTeamSelectAsync(ChangeEventArgs e)
        {
            // Flush team games so that it doesn't display another teams games while loading.
            this.TeamMatches = new List<TeamMatchViewModel>();
            this.SelectedTeamId = int.Parse(e.Value.ToString());

            // Load team games
            this.TeamMatches = await this.MatchDataService.GetTemMatches(this.SelectedTeamId);
        }
    }
}
