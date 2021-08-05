using GeoCubed.SquidLeague4.Website.Common.Helpers;
using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.Models.Enums;
using GeoCubed.SquidLeague4.Website.Shared;
using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Pages.Admin
{
    public partial class AdminStatistics
    {
        [Inject]
        private IStatsDataService statsDataService { get; set; }

        protected List<AdminStatsViewModel> allStats { get; set; }
            = new List<AdminStatsViewModel>();

        protected int selectedStatsId { get; set; }

        protected Modal addModal { get; set; }

        protected Modal editModal { get; set; }

        protected Modal deleteModal { get; set; }

        protected string message { get; set; }

        protected AdminStatsViewModel model { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.model = new AdminStatsViewModel();
            this.selectedStatsId = 0;
            this.allStats = await this.statsDataService.GetAllStatsForAdmin();
        }

        protected void OpenAddStats()
        {
            this.message = string.Empty;
            this.model = new AdminStatsViewModel() 
            { 
                Id = -1, 
                Alias = string.Empty, 
                Sql = string.Empty,
                Modifier = StatsModifiers.None.GetDescription()
            };

            this.addModal.Open();
        }

        protected async Task AddStats()
        {
            this.message = string.Empty;
            var response = await this.statsDataService.CreateStats(this.model);
            if (response.Success)
            {
                this.addModal.Close();
                this.model = new AdminStatsViewModel();
                this.allStats = await this.statsDataService.GetAllStatsForAdmin();
            }
        }

        protected void OpenEditStats()
        {
            this.message = string.Empty;
            var statsToEdit = this.allStats.FirstOrDefault(x => x.Id == this.selectedStatsId);
            this.model = new AdminStatsViewModel()
            {
                Id = statsToEdit.Id,
                Alias = statsToEdit.Alias,
                Sql = statsToEdit.Sql,
                Modifier = statsToEdit.Modifier
            };

            this.editModal.Open();
        }

        protected async Task EditStats()
        {
            this.message = string.Empty;
            var response = await this.statsDataService.UpdateStats(this.model);
            if (response.Success)
            {
                this.editModal.Close();
                this.model = new AdminStatsViewModel();
                this.allStats = await this.statsDataService.GetAllStatsForAdmin();
            }

        }

        protected void OpenDeleteStats()
        {
            this.message = string.Empty;
            this.model = new AdminStatsViewModel()
            {
                Alias = this.allStats.FirstOrDefault(x => x.Id == this.selectedStatsId)?.Alias
            };

            this.deleteModal.Open();
        }

        protected async Task DeleteStats()
        {
            this.message = string.Empty;
            var response = await this.statsDataService.DeleteStats(this.selectedStatsId);
            if (response.Success)
            {
                this.deleteModal.Close();
                this.allStats = await this.statsDataService.GetAllStatsForAdmin();
            }
        }
    }
}
