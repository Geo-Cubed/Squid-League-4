using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.Services.Base;
using GeoCubed.SquidLeague4.Website.Shared;
using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Pages.Admin
{
    public partial class AdminSystemSwitches
    {
        [Inject]
        private ISystemSwitchDataService switchDataService { get; set; }

        protected IEnumerable<AdminSwitchViewModel> allSwitches { get; set; }
            = new List<AdminSwitchViewModel>();

        protected AdminSwitchViewModel model { get; set; }

        protected int selectedSwitchId { get; set; }

        protected Modal addModal { get; set; }

        protected Modal editModal { get; set; }

        protected Modal deleteModal { get; set; }

        protected string message { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.model = new AdminSwitchViewModel();
            this.allSwitches = await this.switchDataService.GetAllSwitchesForAdmin();
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

        protected void OpenAddSwitch()
        {
            this.message = string.Empty;
            this.model = new AdminSwitchViewModel();
            this.addModal.Open();
        }

        protected async Task AddSwitch()
        {
            this.message = string.Empty;
            var response = await this.switchDataService.CreateSwitch(this.model);
            if (response.Success)
            {
                this.addModal.Close();
                this.model = new AdminSwitchViewModel();
                this.allSwitches = await this.switchDataService.GetAllSwitchesForAdmin();
            }
            else
            {
                this.SetMessage(response);
            }
        }

        protected void OpenEditSwitch()
        {
            this.message = string.Empty;
            var switchToEdit = this.allSwitches.FirstOrDefault(x => x.Id == this.selectedSwitchId);
            this.model = new AdminSwitchViewModel()
            {
                Id = switchToEdit.Id,
                Name = switchToEdit.Name,
                Value = switchToEdit.Value
            };

            this.editModal.Open();
        }

        protected async Task EditSwitch()
        {
            this.message = string.Empty;
            var response = await this.switchDataService.UpdateSwitch(this.model);
            if (response.Success)
            {
                this.editModal.Close();
                this.model = new AdminSwitchViewModel();
                this.allSwitches = await this.switchDataService.GetAllSwitchesForAdmin();
            }
            else
            {
                this.SetMessage(response);
            }
        }

        protected void OpenDeleteSwitch()
        {
            this.message = string.Empty;
            this.model = new AdminSwitchViewModel() { Name = this.allSwitches.FirstOrDefault(c => c.Id == this.selectedSwitchId).Name };
            this.deleteModal.Open();
        }

        protected async Task DeleteSwitch()
        {
            this.message = string.Empty;
            var response = await this.switchDataService.DeleteSwitch(this.selectedSwitchId);
            if (response.Success)
            {
                this.deleteModal.Close();
                this.allSwitches = await this.switchDataService.GetAllSwitchesForAdmin();
            }
            else
            {
                this.SetMessage(response);
            }
        }
    }
}
