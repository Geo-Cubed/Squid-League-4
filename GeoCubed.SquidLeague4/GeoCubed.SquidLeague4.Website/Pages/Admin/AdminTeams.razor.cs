using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.Services.Base;
using GeoCubed.SquidLeague4.Website.Shared;
using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Pages.Admin
{
    public partial class AdminTeams
    {
        [Inject]
        private ITeamDataService teamDataService { get; set; }

        protected IEnumerable<AdminTeamViewModel> allTeams { get; set; }
            = new List<AdminTeamViewModel>();

        protected AdminTeamViewModel model { get; set; }

        protected int selectedTeamId { get; set; }

        protected string message { get; set; }

        protected Modal addModal { get; set; }

        protected Modal editModal { get; set; }

        protected Modal deleteModal { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.model = new AdminTeamViewModel();
            this.allTeams = await this.teamDataService.GetAllTeamsForAdmin();
        }

        private void SetMessage(ApiResponse<int> response)
        {
            if (string.IsNullOrEmpty(response.ValidationErrors))
            {
                this.message = response.Message;
            }
            else
            {
                this.message = response.ValidationErrors;
            }
        }

        protected void OpenAddTeam()
        {
            this.message = string.Empty;
            this.model = new AdminTeamViewModel();
            this.addModal.Open();
        }

        protected async Task AddTeam()
        {
            this.message = string.Empty;
            var response = await this.teamDataService.CreateTeam(this.model);
            if (response.Success)
            {
                this.addModal.Close();
                this.model = new AdminTeamViewModel();
                this.allTeams = await this.teamDataService.GetAllTeamsForAdmin();
            }
            else
            {
                this.SetMessage(response);
            }
        }

        protected void OpenEditTeam()
        {
            this.message = string.Empty;
            var teamToEdit = this.allTeams.FirstOrDefault(x => x.Id == this.selectedTeamId);
            this.model = new AdminTeamViewModel()
            {
                Id = teamToEdit.Id,
                TeamName = teamToEdit.TeamName,
                IsActive = teamToEdit.IsActive
            };

            this.editModal.Open();
        }

        protected async Task EditTeam()
        {
            this.message = string.Empty;
            var response = await this.teamDataService.UpdateTeam(this.model);
            if (response.Success)
            {
                this.editModal.Close();
                this.model = new AdminTeamViewModel();
                this.allTeams = await this.teamDataService.GetAllTeamsForAdmin();
            }
            else
            {
                this.SetMessage(response);
            }
        }

        protected void OpenDeleteTeam()
        {
            this.message = string.Empty;
            this.model = new AdminTeamViewModel() { TeamName = this.allTeams.FirstOrDefault(c => c.Id == this.selectedTeamId).TeamName };
            this.deleteModal.Open();
        }

        protected async Task DeleteTeam()
        {
            this.message = string.Empty;
            var response = await this.teamDataService.DeleteTeam(this.selectedTeamId);
            if (response.Success)
            {
                this.deleteModal.Close();
                this.allTeams = await this.teamDataService.GetAllTeamsForAdmin();
            }
            else
            {
                this.SetMessage(response);
            }
        }
    }
}
