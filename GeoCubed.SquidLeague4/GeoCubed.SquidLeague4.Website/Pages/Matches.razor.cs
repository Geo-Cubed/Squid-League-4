using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.ViewModels.Results;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Pages
{
    public partial class Matches
    {
        [Inject]
        private IGameDataService gameDataService { get; set; }

        [Inject]
        private IMatchDataService matchDataService { get; set; }

        protected List<SetInformationViewModel> setInformation { get; set; }
            = new List<SetInformationViewModel>();

        protected override Task OnInitializedAsync()
        {
            /* TODO:
             * Use nswag.
             * Get list of matches.
             * 
             *
             */
        }

        protected async Task OnMatchSelectAsync(ChangeEventArgs e)
        {
            if (!int.TryParse(e.Value.ToString(), out int matchId))
            {
                this.selectedMatch = null;
                return;
            }

            try
            {
                this.setInformation = await this.gameDataService.GetSetInformation(matchId);
            }
            catch
            {
                this.setInformation = new List<SetInformationViewModel>();
            }
        }

        protected string GenerateWinnerText()
        {
            return string.Empty;
        }
    }
}
