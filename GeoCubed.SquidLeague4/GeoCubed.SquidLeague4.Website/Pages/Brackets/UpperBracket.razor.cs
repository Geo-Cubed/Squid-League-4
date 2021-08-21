using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.ViewModels.KnockoutMatches;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Pages.Brackets
{
    public partial class UpperBracket
    {
        [Inject]
        private IBracketKnockoutDataService knockoutDataService { get; set; }

        [Inject]
        private NavigationManager navigation { get; set; }

        protected List<KnockoutInfoViewModel> matches { get; set; }
            = new List<KnockoutInfoViewModel>();

        protected List<KnockoutInfoViewModel> ur1 { get; set; }
            = new List<KnockoutInfoViewModel>();

        protected List<KnockoutInfoViewModel> uqf { get; set; }
            = new List<KnockoutInfoViewModel>();

        protected List<KnockoutInfoViewModel> usf { get; set; }
            = new List<KnockoutInfoViewModel>();

        protected List<KnockoutInfoViewModel> ugf { get; set; }
            = new List<KnockoutInfoViewModel>();

        protected override async Task OnInitializedAsync()
        {
            this.matches = await this.knockoutDataService.GetUpperMatches();
            this.ur1 = this.matches.Where(x => x.Stage == "UR1").ToList();
            this.uqf = this.matches.Where(x => x.Stage == "UQF").ToList();
            this.usf = this.matches.Where(x => x.Stage == "USF").ToList();
            this.ugf = this.matches.Where(x => x.Stage == "UGF").ToList();
        }

        protected void NavigateTo(KnockoutInfoViewModel match)
        {
            this.navigation.NavigateTo($"matches/{match.MatchId}");
        }
    }
}
