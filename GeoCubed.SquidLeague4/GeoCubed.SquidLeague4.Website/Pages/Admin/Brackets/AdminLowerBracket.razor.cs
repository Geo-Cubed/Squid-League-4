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
    public partial class AdminLowerBracket
    {
        [Inject]
        private IBracketKnockoutDataService bracketDataService { get; set; }

        protected IEnumerable<AdminKnockoutMatchViewModel> allUpperBracket { get; set; }
            = new List<AdminKnockoutMatchViewModel>();

        [Inject]
        private ISystemSwitchDataService switchDataService { get; set; }

        protected IEnumerable<string> lowerStages { get; set; }
            = new List<string>();

        [Inject]
        private IMatchDataService matchDataService { get; set; }

        protected IEnumerable<BasicMatchInfo> allMatches { get; set; }
            = new List<BasicMatchInfo>();

        protected int selectedLowerBracketId { get; set; } = 0;

        protected AdminKnockoutMatchViewModel model { get; set; }

        protected Modal addModal { get; set; }

        protected Modal deleteModal { get; set; }

        protected string message { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.model = new AdminKnockoutMatchViewModel();
            this.allMatches = await this.matchDataService.GetBasicMatchInfo();
            this.lowerStages = await this.switchDataService.GetLowerKnockoutStages();
            this.allUpperBracket = await this.bracketDataService.GetLowerBracketMatches();
        }

        protected string GetMatchVsText(int matchId)
        {
            var match = this.allMatches.FirstOrDefault(m => m.Id == matchId);
            if (match == null)
            {
                return string.Empty;
            }

            return string.Format("{0} vs {1}", match.HomeTeam, match.AwayTeam);
        }

        protected string GetDeleteText()
        {
            if (this.selectedLowerBracketId <= 0)
            {
                return string.Empty;
            }

            var lower = this.allUpperBracket.FirstOrDefault(s => s.Id == this.selectedLowerBracketId);
            if (lower == null)
            {
                return string.Empty;
            }

            var vsText = this.GetMatchVsText(this.model.MatchId);
            return string.Format(
                "Are you sure you want to delete {0} on stage {1}",
                vsText,
                lower.Stage);
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

        protected void OpenAddLower()
        {
            this.message = string.Empty;
            this.model = new AdminKnockoutMatchViewModel()
            {

            };

            this.addModal.Open();
        }

        protected async Task AddLower()
        {
            this.message = string.Empty;
            var response = await this.bracketDataService.CreateKnockoutMatch(this.model);
            if (response.Success)
            {
                this.addModal.Close();
                this.model = new AdminKnockoutMatchViewModel();
                this.allUpperBracket = await this.bracketDataService.GetLowerBracketMatches();
            }
            else
            {
                this.SetMessage(response);
            }
        }

        protected void OpenDeleteLower()
        {
            this.message = string.Empty;
            var currentUpper = this.allUpperBracket.FirstOrDefault(c => c.Id == this.selectedLowerBracketId);
            this.model = new AdminKnockoutMatchViewModel()
            {
                MatchId = currentUpper.MatchId,
                Stage = currentUpper.Stage
            };

            this.deleteModal.Open();
        }

        protected async Task DeleteLower()
        {
            this.message = string.Empty;
            var response = await this.bracketDataService.DeleteKnockoutMatch(this.selectedLowerBracketId);
            if (response.Success)
            {
                this.deleteModal.Close();
                this.allUpperBracket = await this.bracketDataService.GetLowerBracketMatches();
            }
            else
            {
                this.SetMessage(response);
            }
        }
    }
}
