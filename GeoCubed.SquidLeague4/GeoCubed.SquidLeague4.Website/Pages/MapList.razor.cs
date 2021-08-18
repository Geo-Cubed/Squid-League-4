using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.ViewModels.GameSettings;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Pages
{
    public partial class MapList
    {
        [Inject]
        private IGameSettingDataService gameSettingDataService { get; set; }

        protected IEnumerable<MapListViewModel> swissMapLists { get; set; }
            = new List<MapListViewModel>();

        protected IEnumerable<MapListViewModel> upperMapLists { get; set; }
            = new List<MapListViewModel>();

        protected IEnumerable<MapListViewModel> lowerMapLists { get; set; }
            = new List<MapListViewModel>();

        protected override async Task OnInitializedAsync()
        {
            var mapLists = (await this.gameSettingDataService.GetMapLists()).Where(x => !string.IsNullOrEmpty(x.BracketStage));
            // This is where I start regreting my way of storing bracket stages :).
            this.swissMapLists = mapLists.Where(x => x.BracketStage.All(char.IsDigit));
            this.upperMapLists = mapLists.Where(x => x.BracketStage.ToUpper().StartsWith('U'));
            this.lowerMapLists = mapLists.Where(x => x.BracketStage.ToUpper().StartsWith('L'));
        }

        protected string GetKnockoutStageName(string stage)
        {
            // TODO: for next squid league put the bracket stages in a seperate table so I can store alias and bo with the stage.
            switch (stage)
            {
                case "UR1":
                    return "Upper Round 1";
                case "UQF":
                    return "Upper Quarter Finals";
                case "USF":
                    return "Upper Semi Finals";
                case "UGF":
                    return "Upper Grand Finals";
                case "LR1":
                    return "Lower Round 1";
                case "LQF":
                    return "Lower Quarter Finals";
                case "LSF":
                    return "Lower Semi Finals";
                case "LGF":
                    return "Lower Grand Finals";
                default:
                    return string.Empty;
            }
        }

    }
}
