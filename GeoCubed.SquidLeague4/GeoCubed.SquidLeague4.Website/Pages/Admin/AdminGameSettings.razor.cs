using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.Shared;
using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Pages.Admin
{
    public partial class AdminGameSettings
    {
        [Inject]
        private IGameSettingDataService gameSettingDataService { get; set; }

        [Inject]
        private ISystemSwitchDataService systemSwitchDataService { get; set; }

        [Inject]
        private IMapDataService mapDataService { get; set; }

        [Inject]
        private IModeDataService modeDataService { get; set; }

        protected IEnumerable<AdminGameSettingViewModel> allSettings { get; set; }
            = new List<AdminGameSettingViewModel>();

        protected IEnumerable<AdminSwitchViewModel> allSwitches { get; set; }
            = new List<AdminSwitchViewModel>();

        protected IEnumerable<AdminMapViewModel> allMaps { get; set; }
            = new List<AdminMapViewModel>();

        protected IEnumerable<AdminModeViewModel> allModes { get; set; }
            = new List<AdminModeViewModel>();

        protected AdminGameSettingViewModel model { get; set; }

        protected int selectedSettingId { get; set; }

        protected Modal addModal { get; set; }

        protected Modal editModal { get; set; }

        protected Modal deleteModal { get; set; }

        protected string message { get; set; }

        protected async override Task OnInitializedAsync()
        {
            this.selectedSettingId = 0;
            this.allModes = await this.modeDataService.GetAllModesForAdmin();
            this.allMaps = await this.mapDataService.GetAllMapsForAdmin();
            this.allSwitches = await this.systemSwitchDataService.GetAllSwitchesForAdmin();
            this.allSettings = await this.gameSettingDataService.GetGameSettingsForAdmin();
        }

        protected void OpenAddSetting()
        {
            this.message = string.Empty;
            this.model = new AdminGameSettingViewModel();
            this.addModal.Open();
        }

        protected async Task AddSetting()
        {
            this.message = string.Empty;
            var response = await this.gameSettingDataService.CreateSetting(this.model);
            if (response.Success)
            {
                this.addModal.Close();
                this.model = new AdminGameSettingViewModel();
                this.allSettings = await this.gameSettingDataService.GetGameSettingsForAdmin();
            }
            else
            {
                this.message = response.ValidationErrors;
            }
        }

        protected void OpenEditSetting()
        {
            this.message = string.Empty;
            var settingToEdit = this.allSettings.FirstOrDefault(x => x.Id == this.selectedSettingId);
            this.model = new AdminGameSettingViewModel()
            {
                Id = settingToEdit.Id,
                BracketStage = settingToEdit.BracketStage,
                GameMapId = settingToEdit.GameMapId,
                GameModeId = settingToEdit.GameModeId,
                SortOrder = settingToEdit.SortOrder
            };

            this.editModal.Open();
        }

        protected async Task EditSetting()
        {
            this.message = string.Empty;
            var response = await this.gameSettingDataService.UpdateSetting(this.model);
            if (response.Success)
            {
                this.editModal.Close();
                this.model = new AdminGameSettingViewModel();
                this.allSettings = await this.gameSettingDataService.GetGameSettingsForAdmin();
            }
            else
            {
                this.message = response.ValidationErrors;
            }
        }

        protected void OpenDeleteSetting()
        {
            this.message = string.Empty;
            var currentSetting = this.allSettings.FirstOrDefault(c => c.Id == this.selectedSettingId);
            this.model = new AdminGameSettingViewModel() 
            { 
                BracketStage = currentSetting.BracketStage,
                GameMapId = currentSetting.GameMapId,
                GameModeId = currentSetting.GameModeId
            };
            this.deleteModal.Open();
        }

        protected async Task DeleteSetting()
        {
            this.message = string.Empty;
            var response = await this.gameSettingDataService.DeleteSetting(this.selectedSettingId);
            if (response.Success)
            {
                this.deleteModal.Close();
                this.allSettings = await this.gameSettingDataService.GetGameSettingsForAdmin();
            }
            else
            {
                this.message = response.Message;
            }
        }
    }
}
