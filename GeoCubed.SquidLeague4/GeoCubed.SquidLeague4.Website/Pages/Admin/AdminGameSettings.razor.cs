using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.Services.Base;
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

        protected int selectedSettingId { get; set; } = 0;

        protected Modal addModal { get; set; }

        protected Modal editModal { get; set; }

        protected Modal deleteModal { get; set; }

        protected string message { get; set; }

        protected async override Task OnInitializedAsync()
        {
            this.model = new AdminGameSettingViewModel();
            this.allModes = await this.modeDataService.GetAllModesForAdmin();
            this.allMaps = await this.mapDataService.GetAllMapsForAdmin();
            this.allSwitches = await this.systemSwitchDataService.GetAllSwitchesForAdmin();
            this.allSettings = await this.gameSettingDataService.GetGameSettingsForAdmin();
        }

        private void SetMessage(ApiResponse<int> response)
        {
            if (string.IsNullOrEmpty(response.ValidationErrors))
            {
                this.message = response.Message;
            }
            else
            {
                this.message = response.ValidationErrors;
            }
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
                this.SetMessage(response);
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
                this.SetMessage(response);
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
                this.SetMessage(response);
            }
        }

        protected int GetSortOrders()
        {
            if (this.selectedSettingId <= 0)
            {
                return 0;
            }

            if(int.TryParse(this.allSwitches.FirstOrDefault(s => s.Name.EndsWith(this.model.BracketStage + "_BO")).Value, out int bestOf))
            {
                return bestOf;
            }

            return 0;
        }

        protected string GetDeleteMessage()
        {
            if (this.selectedSettingId <= 0)
            {
                return string.Empty;
            }

            return string.Format(
                "Are you sure you want to delete stage '{0}' on {1} {2}?",
                this.model.BracketStage,
                this.allModes.FirstOrDefault(m => m.Id == this.allSettings.FirstOrDefault(s => s.Id == this.selectedSettingId).GameModeId)?.ModeName,
                this.allMaps.FirstOrDefault(m => m.Id == this.allSettings.FirstOrDefault(s => s.Id == this.selectedSettingId).GameMapId)?.MapName);
        }
    }
}
