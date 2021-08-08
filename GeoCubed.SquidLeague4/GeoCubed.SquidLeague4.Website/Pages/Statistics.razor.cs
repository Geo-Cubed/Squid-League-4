using GeoCubed.SquidLeague4.Website.Common.Helpers;
using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.Models.Enums;
using GeoCubed.SquidLeague4.Website.ViewModels.Stats;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
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

        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        private ElementReference selectModeModifier;

        private ElementReference selectWeaponModifier;

        protected List<StatsOptionsViewModel> StatsOptions { get; set; }
            = new List<StatsOptionsViewModel>();

        protected int SelectedStatsId { get; set; }

        protected StatsOptionsViewModel SelectedStats { get; set; }

        protected StatsModifiersViewModel statsModifiersVm;

        protected class DataItem 
        {
            public string Key { get; set; }
            public double Value { get; set; }
        }

        protected List<DataItem> dataValues { get; set; }
            = new List<DataItem>();

        protected string tableTitle { get; set; }
            = string.Empty;

        protected string StrongDataType { get; set; }
            = string.Empty;

        protected string seriesTitle { get; set; }

        protected List<StatsDataViewModel> fullData { get; set; }
            = new List<StatsDataViewModel>();

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
            if (statsId == -1)
            {
                return;
            }

            // Refresh the stats.
            await this.OnModifierSelectAsync(new ChangeEventArgs() { Value = -1 });
            if (this.SelectedStats.Modifier == "mode")
            {
                await this.JSRuntime.InvokeVoidAsync("helpers.selectElement", this.selectModeModifier);
            }
            else if (this.SelectedStats.Modifier == "weapon")
            {
                await this.JSRuntime.InvokeVoidAsync("helpers.selectElement", this.selectWeaponModifier);
            }
        }

        protected async Task OnModifierSelectAsync(ChangeEventArgs e)
        {
            this.dataValues = new List<DataItem>();
            this.fullData = new List<StatsDataViewModel>();
            if (!int.TryParse(e.Value.ToString(), out int modifierId))
            { 
                modifierId = -1;
            }

            var data = await this.StatisticDataService.GetStatsData(this.SelectedStatsId, modifierId);
            if (data == null)
            {
                return;
            }

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

            this.fullData = data;
            this.ConvertDataToGraph(data.Take(10).ToList());
            this.GenerateGraphTitle(modifierId);
            this.GenrateSeriesName(this.tableTitle);
        }

        protected void ConvertDataToGraph(List<StatsDataViewModel> data)
        {
            this.dataValues = data.Select(x => new DataItem() 
            { 
                Key = x.Key, 
                Value = double.Parse(x.Value)
            }).ToList();
        }

        protected void GenrateSeriesName(string title)
        {
            this.seriesTitle = string.Empty;
            if (title.ToLower().Contains("pick rate"))
            {
                this.seriesTitle = "Pick Rate";
            }

            if (title.ToLower().Contains("win rate"))
            {
                this.seriesTitle = "Win Rate";
            }

        }

        protected string truncateString(object str)
        {
            var value = (str == null) ? string.Empty : str.ToString();
            if (value.Length <= 4)
            {
                return value;
            }

            return value.Substring(0, 4) + "...";
        }

        protected void GenerateGraphTitle(int modifierId)
        {
            var baseTitle = "Top ";
            
            baseTitle += this.SelectedStats.Alias;
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
                            addon = "in " + this.statsModifiersVm.Modes.FirstOrDefault(x => x.Key == modifierId).Value;
                        }
                        else
                        {
                            addon = "All Modes";
                        }

                        break;
                    case StatsModifiers.Weapon:
                        if (this.statsModifiersVm.Weapons.Any(x => x.Key == modifierId))
                        {
                            addon = "with " + this.statsModifiersVm.Weapons.FirstOrDefault(x => x.Key == modifierId).Value;
                        }
                        else
                        {
                            addon = "All Weapons";
                        }

                        break;
                    default:
                        break;
                }


                this.tableTitle = baseTitle.Replace($"by {char.ToUpper(modifier[0]) + modifier.Substring(1)}", addon);
            }
        }
    }
}
