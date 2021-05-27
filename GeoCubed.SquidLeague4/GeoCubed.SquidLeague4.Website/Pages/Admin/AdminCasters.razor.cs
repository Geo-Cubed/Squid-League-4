using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace GeoCubed.SquidLeague4.Website.Pages.Admin
{
    public partial class AdminCasters
    {
        [Inject]
        private ICasterDataService casterDataService { get; set; }

        protected IEnumerable<AdminCasterViewModel> allCasters { get; set; }
         = new List<AdminCasterViewModel>();

        protected override Task OnInitializedAsync()
        {
            this.allCasters = this.casterDataService.GetAllCasters();
        }
    }
}
