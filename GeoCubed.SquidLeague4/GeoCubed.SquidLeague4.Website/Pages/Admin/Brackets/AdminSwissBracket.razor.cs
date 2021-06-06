using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.Services.Base;
using GeoCubed.SquidLeague4.Website.Shared;
using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Pages.Admin.Brackets
{
    public partial class AdminSwissBracket
    {
        [Inject]
        private ISystemSwitchDataService switchDataService { get; set; }

        protected IEnumerable<AdminSwitchViewModel> allSwitches { get; set; }
            = new List<AdminSwitchViewModel>();

        [Inject]
        private IMatchDataService matchDataService { get; set; }

        protected IEnumerable<AdminMatchViewModel> allMatches { get; set; }
            = new List<AdminMatchViewModel>();

        protected IEnumerable<AdminBracketSwissViewModel> allBracketSwiss { get; set; }
            = new List<AdminBracketSwissViewModel>();

        protected int selectedSwissId { get; set; }

        protected AdminBracketSwissViewModel model { get; set; }

        protected Modal addModal { get; set; }

        protected Modal editModal { get; set; }

        protected Modal deleteModal { get; set; }

        protected string message { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.allMatches = await this.matchDataService.GetAllMatchesForAdmin();
            this.allSwitches = await this.switchDataService.GetAllSwitchesForAdmin();
        }

        protected string GetMatchVsText(int swissId)
        {
            var match = this.allMatches.FirstOrDefault(m => m.Id == this.allBracketSwiss.FirstOrDefault(s => s.Id == swissId).MatchId);
            if (match == null)
            {
                return string.Empty;
            }

            return string.Format("{0} vs {1}", "a", "b");
        }

        protected string GetDeleteText()
        {
            return string.Empty;
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

        protected void OpenAddSwiss()
        {
            this.message = string.Empty;
            this.model = new AdminBracketSwissViewModel()
            {
                
            };

            this.addModal.Open();
        }

        protected async Task AddSwiss()
        {
            this.message = string.Empty;
            //var response = await this.matchDataService.CreateMatch(this.model);
            if (/*response.Success*/false)
            {
                this.addModal.Close();
                this.model = new AdminBracketSwissViewModel();
                //this.allBracketSwiss = await this.matchDataService.GetAllMatchesForAdmin();
            }
            else
            {
                this.SetMessage(null);
            }
        }

        protected void OpenEditSwiss()
        {
            this.message = string.Empty;
            var swissToEdit = this.allBracketSwiss.FirstOrDefault(x => x.Id == this.selectedSwissId);
            this.model = new AdminBracketSwissViewModel()
            {
                Id = swissToEdit.Id,
                MatchId = swissToEdit.MatchId,
                MatchWeek = swissToEdit.MatchWeek
            };

            this.editModal.Open();
        }

        protected async Task EditSwiss()
        {
            this.message = string.Empty;
            //var response = await this.matchDataService.UpdateMatch(this.model);
            if (/*response.Success*/false)
            {
                this.editModal.Close();
                this.model = new AdminBracketSwissViewModel();
                //this.allBracketSwiss = await this.matchDataService.GetAllMatchesForAdmin();
            }
            else
            {
                this.SetMessage(null);
            }
        }

        protected void OpenDeleteSwiss()
        {
            this.message = string.Empty;
            var currentSwiss = this.allBracketSwiss.FirstOrDefault(c => c.Id == this.selectedSwissId);
            this.model = new AdminBracketSwissViewModel()
            {
                MatchId = currentSwiss.MatchId,
                MatchWeek = currentSwiss.MatchWeek
            };

            this.deleteModal.Open();
        }

        protected async Task DeleteSwiss()
        {
            this.message = string.Empty;
            //var response = await this.matchDataService.DeleteMatch(this.selectedSwissId);
            if (/*response.Success*/false)
            {
                this.deleteModal.Close();
                //this.allBracketSwiss = await this.matchDataService.GetAllMatchesForAdmin();
            }
            else
            {
                this.SetMessage(null);
            }
        }
    }
}
