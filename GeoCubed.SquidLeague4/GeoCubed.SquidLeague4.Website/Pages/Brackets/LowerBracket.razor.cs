using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.ViewModels.KnockoutMatches;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Pages.Brackets
{
    public partial class LowerBracket
    {
        [Inject]
        private IBracketKnockoutDataService knockoutDataService { get; set; }

        [Inject]
        private NavigationManager navigation { get; set; }

        protected List<KnockoutInfoViewModel> matches { get; set; }
            = new List<KnockoutInfoViewModel>();

        protected List<KnockoutInfoViewModel> lr1 { get; set; }
            = new List<KnockoutInfoViewModel>();

        protected List<KnockoutInfoViewModel> lqf { get; set; }
            = new List<KnockoutInfoViewModel>();

        protected List<KnockoutInfoViewModel> lsf { get; set; }
            = new List<KnockoutInfoViewModel>();

        protected List<KnockoutInfoViewModel> lgf { get; set; }
            = new List<KnockoutInfoViewModel>();

        protected override async Task OnInitializedAsync()
        {
            this.matches = await this.knockoutDataService.GetLowerMatches();
            this.lr1 = this.matches.Where(x => x.Stage == "LR1").ToList();
            this.lqf = this.matches.Where(x => x.Stage == "LQF").ToList();
            this.lsf = this.matches.Where(x => x.Stage == "LSF").ToList();
            this.lgf = this.matches.Where(x => x.Stage == "LGF").ToList();
        }

        protected void NavigateTo(KnockoutInfoViewModel match)
        {
            this.navigation.NavigateTo($"matches/{match.MatchId}");
        }
    }
}
