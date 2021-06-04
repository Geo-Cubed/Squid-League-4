using GeoCubed.SquidLeague4.Website.Common.Helpers;
using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.Models.Enums;
using GeoCubed.SquidLeague4.Website.Shared;
using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Pages.Admin
{
    public partial class AdminPlayers
    {
        [Inject]
        private IPlayerDataService playerDataService { get; set; }

        [Inject]
        private ITeamDataService teamDataService { get; set; }

        protected IEnumerable<AdminPlayerViewModel> allPlayers { get; set; }
            = new List<AdminPlayerViewModel>();

        protected IEnumerable<AdminTeamViewModel> allTeams { get; set; }
            = new List<AdminTeamViewModel>();

        protected int selectedPlayerId { get; set; } = 0;

        protected AdminPlayerViewModel model { get; set; }

        protected Modal addModal { get; set; }

        protected Modal editModal { get; set; }

        protected Modal deleteModal { get; set; }

        protected string message { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.model = new AdminPlayerViewModel();
            this.allTeams = await this.teamDataService.GetAllTeamsForAdmin();
            this.allPlayers = await this.playerDataService.GetAllPlayers();
        }

        protected void OpenAddPlayer() 
        {
            this.message = string.Empty;
            this.model = new AdminPlayerViewModel() 
            {
                SzRank = Ranks.unknown.GetDescription(),
                TcRank = Ranks.unknown.GetDescription(),
                RmRank = Ranks.unknown.GetDescription(),
                CbRank = Ranks.unknown.GetDescription(),
                TeamId = -1 
            };

            this.addModal.Open();
        }

        protected async Task AddPlayer()
        {
            this.message = string.Empty;
            var response = await this.playerDataService.CreatePlayer(this.model);
            if (response.Success)
            {
                this.addModal.Close();
                this.model = new AdminPlayerViewModel();
                this.allPlayers = await this.playerDataService.GetAllPlayers();
            }
            else
            {
                this.message = response.ValidationErrors;
            }
        }        
        
        protected void OpenEditPlayer() 
        {
            this.message = string.Empty;
            var playerToEdit = this.allPlayers.FirstOrDefault(x => x.Id == this.selectedPlayerId);
            this.model = new AdminPlayerViewModel()
            {
                Id = playerToEdit.Id,
                InGameName = playerToEdit.InGameName,
                SzRank = playerToEdit.SzRank,
                TcRank = playerToEdit.TcRank,
                RmRank = playerToEdit.RmRank,
                CbRank = playerToEdit.CbRank,
                TeamId = playerToEdit.TeamId ?? -1,
                IsActive = playerToEdit.IsActive
            };

            this.editModal.Open();
        }

        protected async Task EditPlayer()
        {
            this.message = string.Empty;
            var response = await this.playerDataService.UpdatePlayer(this.model);
            if (response.Success)
            {
                this.editModal.Close();
                this.model = new AdminPlayerViewModel();
                this.allPlayers = await this.playerDataService.GetAllPlayers();
            }
            else
            {
                this.message = response.ValidationErrors;
            }
        }        
        
        protected void OpenDeletePlayer() 
        {
            this.message = string.Empty;
            this.model = new AdminPlayerViewModel() 
            { 
                InGameName = this.allPlayers.FirstOrDefault(c => c.Id == this.selectedPlayerId).InGameName 
            };

            this.deleteModal.Open();
        }

        protected async Task DeletePlayer()
        {
            this.message = string.Empty;
            var response = await this.playerDataService.DeletePlayer(this.selectedPlayerId);
            if (response.Success)
            {
                this.deleteModal.Close();
                this.allPlayers = await this.playerDataService.GetAllPlayers();
            }
            else
            {
                this.message = response.Message;
            }
        }
    }
}
