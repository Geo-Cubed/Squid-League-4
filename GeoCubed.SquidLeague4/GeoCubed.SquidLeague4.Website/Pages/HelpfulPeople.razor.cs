using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Pages
{
    partial class HelpfulPeople
    {
        [Inject]
        public IHelpfulPersonDataService HelpfulPersonDataService { get; set; }

        public IEnumerable<HelpfulPersonDetailViewModel> AllHelpfulPeople { get; set; } 
            = new List<HelpfulPersonDetailViewModel>();

        protected async override Task OnInitializedAsync()
        {
            this.AllHelpfulPeople = await this.HelpfulPersonDataService.GetAllHelpfulPersons();
        }
    }
}
