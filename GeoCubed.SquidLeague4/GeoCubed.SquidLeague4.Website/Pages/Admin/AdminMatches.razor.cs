using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.Shared;
using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Pages.Admin
{
    public partial class AdminMatches
    {
        [Inject]
        private IMatchDataService matchDataService { get; set; }

        [Inject]
        private ITeamDataService teamDataService { get; set; }

        [Inject]
        private ICasterDataService casterDataService { get; set; }

        protected IEnumerable<AdminMatchViewModel> allMatches { get; set; }
            = new List<AdminMatchViewModel>();

        protected IEnumerable<AdminTeamViewModel> allTeams { get; set; }
            = new List<AdminTeamViewModel>();

        protected IEnumerable<AdminCasterViewModel> allCasters { get; set; }
            = new List<AdminCasterViewModel>();

        protected AdminMatchViewModel model { get; set; }

        protected int selectedMatchId { get; set; } = 0;

        protected Modal addModal { get; set; }

        protected Modal editModal { get; set; }

        protected Modal deleteModal { get; set; }

        protected string message { get; set; }

        protected async override Task OnInitializedAsync()
        {
            this.model = new AdminMatchViewModel();
            this.allMatches = await this.matchDataService.GetAllMatchesForAdmin();
            this.allTeams = await this.teamDataService.GetAllTeamsForAdmin();
            this.allCasters = await this.casterDataService.GetAllCastersForAdmin();
        }

        protected void OpenAddMatch()
        {
            this.message = string.Empty;
            this.model = new AdminMatchViewModel()
            {
                HomeTeamId = (this.allTeams.Count() > 0) ? this.allTeams.OrderBy(t => t.TeamName).First().Id : -1, 
                AwayTeamId = (this.allTeams.Count() > 1) ? this.allTeams.OrderBy(t => t.TeamName).ElementAt(1).Id : -1,
                HomeTeamScore = 0,
                AwayTeamScore = 0,
                CasterProfileId = -1, 
                SecondaryCasterProfileId = -1
            };

            this.addModal.Open();
        }

        protected async Task AddMatch()
        {
            this.message = string.Empty;
            var response = await this.matchDataService.CreateMatch(this.model);
            if (response.Success)
            {
                this.addModal.Close();
                this.model = new AdminMatchViewModel();
                this.allMatches = await this.matchDataService.GetAllMatchesForAdmin();
            }
            else
            {
                this.message = response.ValidationErrors;
            }
        }

        protected void OpenEditMatch()
        {
            this.message = string.Empty;
            var matchToEdit = this.allMatches.FirstOrDefault(x => x.Id == this.selectedMatchId);
            this.model = new AdminMatchViewModel()
            {
                Id = matchToEdit.Id,
                HomeTeamId = matchToEdit.HomeTeamId,
                AwayTeamId = matchToEdit.AwayTeamId,
                Winner = matchToEdit.Winner,
                HomeTeamScore = (matchToEdit.HomeTeamScore < 0) ? 0 : matchToEdit.HomeTeamScore,
                AwayTeamScore = (matchToEdit.AwayTeamScore < 0) ? 0 : matchToEdit.AwayTeamScore,
                CasterProfileId = matchToEdit.CasterProfileId ?? -1,
                MatchVodLink = matchToEdit.MatchVodLink,
                MatchDate = matchToEdit.MatchDate,
                SecondaryCasterProfileId = matchToEdit.SecondaryCasterProfileId ?? -1
            };

            this.editModal.Open();
        }

        protected async Task EditMatch()
        {
            this.message = string.Empty;
            var response = await this.matchDataService.UpdateMatch(this.model);
            if (response.Success)
            {
                this.editModal.Close();
                this.model = new AdminMatchViewModel();
                this.allMatches = await this.matchDataService.GetAllMatchesForAdmin();
            }
            else
            {
                this.message = response.ValidationErrors;
            }
        }

        protected void OpenDeleteMatch()
        {
            this.message = string.Empty;
            var currentMatch = this.allMatches.FirstOrDefault(c => c.Id == this.selectedMatchId);
            this.model = new AdminMatchViewModel()
            {
                HomeTeamId = currentMatch.HomeTeamId,
                AwayTeamId = currentMatch.AwayTeamId
            };

            this.deleteModal.Open();
        }

        protected async Task DeleteMatch()
        {
            this.message = string.Empty;
            var response = await this.matchDataService.DeleteMatch(this.selectedMatchId);
            if (response.Success)
            {
                this.deleteModal.Close();
                this.allMatches = await this.matchDataService.GetAllMatchesForAdmin();
            }
            else
            {
                this.message = response.Message;
            }
        }

        protected string DeleteMessage()
        {
            if (this.selectedMatchId <= 0)
            {
                return string.Empty;
            }

            var match = this.allMatches.FirstOrDefault(m => m.Id == this.selectedMatchId);
            if (match == null)
            {
                return "Are you sure you want to delete match";
            }

            return string.Format(
                "Are you sure you want to delete match {0}: {1} vs. {2}",
                this.selectedMatchId,
                this.allTeams.FirstOrDefault(t => t.Id == match.HomeTeamId)?.TeamName,
                this.allTeams.FirstOrDefault(t => t.Id == match.AwayTeamId)?.TeamName
            );
        }
    }
}
