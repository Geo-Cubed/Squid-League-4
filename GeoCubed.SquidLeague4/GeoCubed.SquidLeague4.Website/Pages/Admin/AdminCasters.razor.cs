using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.Shared;
using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using Microsoft.AspNetCore.Components;

namespace GeoCubed.SquidLeague4.Website.Pages.Admin
{
    public partial class AdminCasters
    {
        [Inject]
        private ICasterDataService casterDataService { get; set; }

        protected IEnumerable<AdminCasterViewModel> allCasters { get; set; }
         = new List<AdminCasterViewModel>();

        protected AdminCasterViewModel Model { get; set; }

        protected int SelectedCasterId { get; set; } = 0;

        protected Modal AddModal { get; set; }

        protected Modal EditModal { get; set; }

        protected Modal DeleteModal { get; set; }

        protected string Message { get; set; }

        protected async override Task OnInitializedAsync()
        {
            this.Model = new AdminCasterViewModel();
            this.allCasters = await this.casterDataService.GetAllCastersForAdmin();
        }

        protected void OpenAddCaster()
        {
            this.Message = string.Empty;
            this.Model = new AdminCasterViewModel();
            this.AddModal.Open();
        }

        protected async Task AddCaster()
        {
            this.Message = string.Empty;
            var response = await this.casterDataService.CreateCaster(this.Model);
            if (response.Success)
            {
                this.AddModal.Close();
                this.Model = new AdminCasterViewModel();
                this.allCasters = await this.casterDataService.GetAllCastersForAdmin();
            }
            else
            {
                this.Message = response.ValidationErrors;
            }
        }

        protected void OpenEditCaster()
        {
            this.Message = string.Empty;
            var casterToEdit = this.allCasters.FirstOrDefault(x => x.Id == this.SelectedCasterId);
            this.Model = new AdminCasterViewModel()
            {
                Id = casterToEdit.Id,
                CasterName = casterToEdit.CasterName,
                Discord = casterToEdit.Discord,
                Twitter = casterToEdit.Twitter,
                Twitch = casterToEdit.Twitch,
                YouTube = casterToEdit.YouTube,
                ProfilePicturePath = casterToEdit.ProfilePicturePath,
                IsActive = casterToEdit.IsActive
            };

            this.EditModal.Open();
        }

        protected async Task EditCaster()
        {
            this.Message = string.Empty;
            var response = await this.casterDataService.UpdateCaster(this.Model);
            if (response.Success)
            {
                this.EditModal.Close();
                this.Model = new AdminCasterViewModel();
                this.allCasters = await this.casterDataService.GetAllCastersForAdmin();
            }
            else
            {
                this.Message = response.ValidationErrors;
            }
        }

        protected void OpenDeleteCaster()
        {
            this.Message = string.Empty;
            this.Model = new AdminCasterViewModel() { CasterName = this.allCasters.FirstOrDefault(c => c.Id == this.SelectedCasterId).CasterName };
            this.DeleteModal.Open();
        }

        protected async Task DeleteCaster()
        {
            this.Message = string.Empty;
            var response = await this.casterDataService.DeleteCaster(this.SelectedCasterId);
            if (response.Success)
            {
                this.DeleteModal.Close();
                this.allCasters = await this.casterDataService.GetAllCastersForAdmin();
            }
            else
            {
                this.Message = response.Message;
            }
        }
    }
}
