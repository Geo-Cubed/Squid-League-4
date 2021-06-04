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
    public partial class AdminHelpfulPeople
    {
        [Inject]
        private IHelpfulPersonDataService helpfulPersonDataService { get; set; }

        protected IEnumerable<AdminHelpfulPeopleViewModel> allPeople { get; set; }
            = new List<AdminHelpfulPeopleViewModel>();

        protected AdminHelpfulPeopleViewModel model { get; set; }

        protected int selectedPersonId { get; set; }

        protected Modal addModal { get; set; }

        protected Modal editModal { get; set; }

        protected Modal deleteModal { get; set; }

        protected string message { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.model = new AdminHelpfulPeopleViewModel();
            this.allPeople = await this.helpfulPersonDataService.GetHelpfulPeopleForAdmin();
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

        protected void OpenAddPerson()
        {
            this.message = string.Empty;
            this.model = new AdminHelpfulPeopleViewModel();
            this.addModal.Open();
        }

        protected async Task AddPerson()
        {
            this.message = string.Empty;
            var response = await this.helpfulPersonDataService.CreateHelpfulPerson(this.model);
            if (response.Success)
            {
                this.addModal.Close();
                this.model = new AdminHelpfulPeopleViewModel();
                this.allPeople = await this.helpfulPersonDataService.GetHelpfulPeopleForAdmin();
            }
            else
            {
                this.SetMessage(response);
            }
        }

        protected void OpenEditPerson()
        {
            this.message = string.Empty;
            var personToEdit = this.allPeople.FirstOrDefault(x => x.Id == this.selectedPersonId);
            this.model = new AdminHelpfulPeopleViewModel()
            {
                Id = personToEdit.Id,
                UserName = personToEdit.UserName,
                Description = personToEdit.Description,
                TwitterLink = personToEdit.TwitterLink,
                ProfilePictureLink = personToEdit.ProfilePictureLink,
                Role = personToEdit.Role
            };

            this.editModal.Open();
        }

        protected async Task EditPerson()
        {
            this.message = string.Empty;
            var response = await this.helpfulPersonDataService.UpdateHelpfulPerson(this.model);
            if (response.Success)
            {
                this.editModal.Close();
                this.model = new AdminHelpfulPeopleViewModel();
                this.allPeople = await this.helpfulPersonDataService.GetHelpfulPeopleForAdmin();
            }
            else
            {
                this.SetMessage(response);
            }
        }

        protected void OpenDeletePerson()
        {
            this.message = string.Empty;
            this.model = new AdminHelpfulPeopleViewModel() { UserName = this.allPeople.FirstOrDefault(c => c.Id == this.selectedPersonId).UserName };
            this.deleteModal.Open();
        }

        protected async Task DeletePerson()
        {
            this.message = string.Empty;
            var response = await this.helpfulPersonDataService.DeleteHelpfulPerson(this.selectedPersonId);
            if (response.Success)
            {
                this.deleteModal.Close();
                this.allPeople = await this.helpfulPersonDataService.GetHelpfulPeopleForAdmin();
            }
            else
            {
                this.SetMessage(response);
            }
        }
    }
}
