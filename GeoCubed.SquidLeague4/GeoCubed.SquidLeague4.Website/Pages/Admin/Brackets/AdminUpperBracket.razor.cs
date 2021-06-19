using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.Services.Base;
using GeoCubed.SquidLeague4.Website.Shared;
using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using GeoCubed.SquidLeague4.Website.ViewModels.Matches;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Pages.Admin.Brackets
{
    public partial class AdminUpperBracket
    {
        [Inject]
        private IBracketKnockoutDataService bracketDataService { get; set; }

        protected IEnumerable<AdminUpperBracketViewModel> allUpperBracket { get; set; }
            = new List<AdminUpperBracketViewModel>();

        [Inject]
        private ISystemSwitchDataService switchDataService { get; set; }

        protected IEnumerable<string> upperStages { get; set; }
            = new List<string>();

        [Inject]
        private IMatchDataService matchDataService { get; set; }

        protected IEnumerable<BasicMatchInfo> allMatches { get; set; }
            = new List<BasicMatchInfo>();

        protected int selectedUpperBracketId { get; set; } = 0;

        protected AdminUpperBracketViewModel model { get; set; }

        protected Modal addModal { get; set; }

        protected Modal deleteModal { get; set; }

        protected string message { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.model = new AdminUpperBracketViewModel();
            this.allMatches = await this.matchDataService.GetBasicMatchInfo();
            this.upperStages = await this.switchDataService.GetUpperKnockoutStages();
            this.allUpperBracket = await this.bracketDataService.GetUpperBracketMatches();
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
            if (this.selectedUpperBracketId <= 0)
            {
                return string.Empty;
            }

            var upper = this.allUpperBracket.FirstOrDefault(s => s.Id == this.selectedUpperBracketId);
            if (upper == null)
            {
                return string.Empty;
            }

            var vsText = this.GetMatchVsText(this.model.MatchId);
            return string.Format(
                "Are you sure you want to delete {0} on stage {1}",
                vsText,
                upper.Stage);
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

        protected void OpenAddUpper()
        {
            this.message = string.Empty;
            this.model = new AdminUpperBracketViewModel()
            {

            };

            this.addModal.Open();
        }

        protected async Task AddUpper()
        {
            this.message = string.Empty;
            var response = new ApiResponse<int>(); //await this.bracketDataService.CreateSwissMatch(this.model);
            if (response.Success)
            {
                this.addModal.Close();
                this.model = new AdminUpperBracketViewModel();
                this.allUpperBracket = await this.bracketDataService.GetUpperBracketMatches();
            }
            else
            {
                this.SetMessage(response);
            }
        }

        protected void OpenDeleteUpper()
        {
            this.message = string.Empty;
            var currentUpper = this.allUpperBracket.FirstOrDefault(c => c.Id == this.selectedUpperBracketId);
            this.model = new AdminUpperBracketViewModel()
            {
                MatchId = currentUpper.MatchId,
                Stage = currentUpper.Stage
            };

            this.deleteModal.Open();
        }

        protected async Task DeleteUpper()
        {
            this.message = string.Empty;
            var response = new ApiResponse<int>(); //await this.bracketDataService.DeleteSwissMatch(this.selectedUpperBracketId);
            if (response.Success)
            {
                this.deleteModal.Close();
                this.allUpperBracket = await this.bracketDataService.GetUpperBracketMatches();
            }
            else
            {
                this.SetMessage(response);
            }
        }
    }
}
