using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.Services.Base;
using GeoCubed.SquidLeague4.Website.Shared;
using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using GeoCubed.SquidLeague4.Website.ViewModels.Matches;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Pages.Admin.Brackets
{
    public partial class AdminSwissBracket
    {
        [Inject]
        private ISwissDataService swissDataService { get; set; }

        protected IEnumerable<AdminBracketSwissViewModel> allBracketSwiss { get; set; }
            = new List<AdminBracketSwissViewModel>();

        [Inject]
        private ISystemSwitchDataService switchDataService { get; set; }

        protected IEnumerable<AdminSwitchViewModel> allSwitches { get; set; }
            = new List<AdminSwitchViewModel>();

        [Inject]
        private IMatchDataService matchDataService { get; set; }

        protected IEnumerable<BasicMatchInfo> allMatches { get; set; }
            = new List<BasicMatchInfo>();

        protected int selectedSwissId { get; set; }

        protected AdminBracketSwissViewModel model { get; set; }

        protected Modal addModal { get; set; }

        protected Modal deleteModal { get; set; }

        protected string message { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.allMatches = await this.matchDataService.GetBasicMatchInfo();
            this.allSwitches = await this.switchDataService.GetAllSwitchesForAdmin();
            this.allBracketSwiss = await this.swissDataService.GetSwissMatchesForAdmin();
        }

        protected string GetMatchVsText(int swissId)
        {
            var match = this.allMatches.FirstOrDefault(m => m.Id == this.allBracketSwiss.FirstOrDefault(s => s.Id == swissId).MatchId);
            if (match == null)
            {
                return string.Empty;
            }

            return string.Format("{0} vs {1}", match.HomeTeam, match.AwayTeam);
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
            var response = await this.swissDataService.CreateSwissMatch(this.model);
            if (response.Success)
            {
                this.addModal.Close();
                this.model = new AdminBracketSwissViewModel();
                this.allBracketSwiss = await this.swissDataService.GetSwissMatchesForAdmin();
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
            var response = await this.swissDataService.DeleteSwissMatch(this.selectedSwissId);
            if (response.Success)
            {
                this.deleteModal.Close();
                this.allBracketSwiss = await this.swissDataService.GetSwissMatchesForAdmin();
            }
            else
            {
                this.SetMessage(null);
            }
        }
    }
}
