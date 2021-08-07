using GeoCubed.SquidLeague4.Website.Common.Helpers;
using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.Models.Enums;
using GeoCubed.SquidLeague4.Website.ViewModels.Stats;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Pages
{
    partial class Statistics
    {
        [Inject]
        private IStatsDataService StatisticDataService { get; set; }

        protected List<StatsOptionsViewModel> StatsOptions { get; set; }
            = new List<StatsOptionsViewModel>();

        protected int SelectedStatsId { get; set; }

        protected StatsOptionsViewModel SelectedStats { get; set; }

        protected StatsModifiersViewModel statsModifiersVm;

        protected string dataValues { get; set; }
            = string.Empty;

        protected string dataLabels { get; set; }
            = string.Empty;

        protected string tableTitle { get; set; }
            = string.Empty;

        protected string StrongDataType { get; set; }
            = string.Empty;

        protected async override Task OnInitializedAsync()
        {
            // Do Stats stuff.
            this.StatsOptions = await this.StatisticDataService.GetAllStats();
            this.statsModifiersVm = await this.StatisticDataService.GetStatsModifiers();
            this.SelectedStatsId = 0;
            this.SelectedStats = null;
        }

        protected async Task OnStatsSelectAsync(ChangeEventArgs e)
        {
            // Load the correct stats page.
            if (!int.TryParse(e.Value.ToString(), out int statsId))
            {
                this.SelectedStats = null;
            }

            this.SelectedStatsId = statsId;
            this.SelectedStats = this.StatsOptions.FirstOrDefault(x => x.Id == statsId);

            // Refresh the stats.
            this.OnModifierSelectAsync(new ChangeEventArgs() { Value = -1 });
        }

        protected async Task OnModifierSelectAsync(ChangeEventArgs e)
        {
            this.dataLabels = string.Empty;
            this.dataValues = string.Empty;
            if (!int.TryParse(e.Value.ToString(), out int modifierId))
            { 
                modifierId = -1;
            }

            var data = await this.StatisticDataService.GetStatsData(this.SelectedStatsId, modifierId);
            this.StrongDataType = data.FirstOrDefault().ValueStrongType;
            switch (this.StrongDataType)
            {
                case "double":
                   data = data
                        .OrderByDescending(x => double.TryParse(x.Value, out double val) ? val : double.MinValue)
                        .ToList();
                    break;
                case "int":
                    data = data
                        .OrderByDescending(x => int.TryParse(x.Value, out int val) ? val : int.MinValue)
                        .ToList();
                    break;
                case "string":
                    data = data
                        .OrderByDescending(x => x.Value)
                        .ToList();
                    break;
                default:
                    break;
            }

            this.ConvertDataToGraph(data.Take(10).ToList());
            this.GenerateGraphTitle(modifierId);
        }

        protected void ConvertDataToGraph(List<StatsDataViewModel> data)
        {
            this.dataValues = string.Join(',', data.Select(x => x.Value));
            this.dataLabels = string.Join(',', data.Select(x => 
                {
                    if (x.Key.Length <= 12)
                    {
                        return x.Key;
                    }

                    return x.Key.Substring(0, 12) + "...";
                }));
        }

        protected void GenerateGraphTitle(int modifierId)
        {
            var baseTitle = "Top 10 " + this.SelectedStats.Alias;
            var modifier = this.SelectedStats.Modifier;

            if (string.IsNullOrEmpty(modifier) || modifier == "none")
            {
                this.tableTitle = baseTitle;
            }
            else
            {
                var addon = string.Empty;
                EnumExtentions.TryGetValueFromDescription(modifier, out StatsModifiers modifierObj);
                switch (modifierObj)
                {
                    case StatsModifiers.Mode:
                        if (this.statsModifiersVm.Modes.Any(x => x.Key == modifierId))
                        {
                            addon = this.statsModifiersVm.Modes.FirstOrDefault(x => x.Key == modifierId).Value;
                        }
                        else
                        {
                            addon = "All Modes";
                        }

                        break;
                    case StatsModifiers.Weapon:
                        if (this.statsModifiersVm.Weapons.Any(x => x.Key == modifierId))
                        {
                            addon = this.statsModifiersVm.Weapons.FirstOrDefault(x => x.Key == modifierId).Value;
                        }
                        else
                        {
                            addon = "All Weapons";
                        }

                        break;
                    default:
                        break;
                }

                this.tableTitle = baseTitle.Replace($"by {char.ToUpper(modifier[0]) + modifier.Substring(1)}", $"in {addon}");
            }
        }
    }
}
