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
    public partial class AdminPlayers
    {
        [Inject]
        private IPlayerDataService playerDataService { get; set; }

        protected IEnumerable<AdminPlayerViewModel> allPlayers { get; set; }
            = new List<AdminPlayerViewModel>();

        protected int selectedPlayerId { get; set; } = 0;

        protected AdminPlayerViewModel model { get; set; }

        protected Modal addModal { get; set; }

        protected Modal editModal { get; set; }

        protected Modal deleteModal { get; set; }

        protected override Task OnInitializedAsync()
        {
            this.model = new AdminPlayerViewModel();
            this.allPlayers = await this.playerDataService.GetAllPlayers();
        }

        protected void OpenAddPlayer() 
        { 
        }

        protected async Task AddPlayer()
        {

        }        
        
        protected void OpenEditPlayer() 
        { 
        }

        protected async Task EditPlayer()
        {

        }        
        
        protected void OpenDeletePlayer() 
        { 
        }

        protected async Task DeletePlayer()
        {

        }


    }
}
