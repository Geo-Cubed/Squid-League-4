using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Pages
{
    public partial class Index
    {
        [Inject]
        public IMatchDataService MatchDataService { get; set; }

        public IEnumerable<UpcommingMatchViewModel> UpcommingMatches { get; set; }
            = new List<UpcommingMatchViewModel>();

        protected async override Task OnInitializedAsync()
        {
            this.UpcommingMatches = (await this.MatchDataService.GetUpcommingMatches()).OrderBy(x => x.MatchDate.Value);
        }
    }
}
