﻿using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using GeoCubed.SquidLeague4.Website.ViewModels.Matches;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Pages.Admin
{
    public partial class AdminMatchResults
    {
        [Inject]
        private IMatchDataService matchDataService { get; set; }

        protected IEnumerable<BasicMatchInfo> matches { get; set; }
            = new List<BasicMatchInfo>();

        protected IEnumerable<AdminGameFullInfoViewModel> games { get; set; }
            = new List<AdminGameFullInfoViewModel>();

        protected override async Task OnInitializedAsync()
        {
            this.matches = await this.matchDataService.GetBasicMatchInfo();
            // TODO: Get map list.
        }

        protected string GetMatchText(string HomeTeam, string AwayTeam)
        {
            return string.Format("{0} vs. {1}", HomeTeam, AwayTeam);
        }
    }
}