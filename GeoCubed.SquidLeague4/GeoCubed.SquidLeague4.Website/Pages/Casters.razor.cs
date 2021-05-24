using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Pages
{
    partial class Casters
    {
        [Inject]
        public ICasterDataService CasterDataService { get; set; }

        public IEnumerable<CasterDetailViewModel> AllCasters { get; set; }
            = new List<CasterDetailViewModel>();

        protected async override Task OnInitializedAsync()
        {
            this.AllCasters = await this.CasterDataService.GetAllCasters();
        }
    }
}
