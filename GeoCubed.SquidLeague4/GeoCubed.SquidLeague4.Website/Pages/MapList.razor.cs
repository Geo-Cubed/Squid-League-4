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

    }
}
