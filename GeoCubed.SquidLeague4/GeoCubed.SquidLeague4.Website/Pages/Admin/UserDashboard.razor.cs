using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.Shared;
using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Pages.Admin
{
    public partial class UserDashboard
    {
        [Inject]
        private IAuthenticationService authenticationService { get; set; }

        protected IEnumerable<AdminUserViewModel> allUsers
            = new List<AdminUserViewModel>();

        protected IEnumerable<string> allRoles
            = new List<string>();

        protected string selectedUserName { get; set; }

        protected Modal addUser { get; set; }

        protected Modal editUserRoles { get; set; }

        protected Modal deleteUser { get; set; }

        protected AdminUserViewModel model { get; set; }

        protected AdminUserCreationViewModel creationModel { get; set; }

        protected string roleModel { get; set; }

        protected string message { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.model = new AdminUserViewModel();
            this.creationModel = new AdminUserCreationViewModel();
            this.roleModel = string.Empty;
            this.allUsers = await this.authenticationService.GetAllUsers();
            this.allRoles = await this.authenticationService.GetAllRoles();
        }

        protected void OpenAddUser()
        {
            this.message = string.Empty;
            this.creationModel = new AdminUserCreationViewModel();
            this.addUser.Open();
        }

        protected async Task AddTeam()
        {
            this.message = string.Empty;
            var response = await this.authenticationService.Register(this.creationModel.Username, this.creationModel.Password);
            if (response)
            {
                this.addUser.Close();
                this.creationModel = new AdminUserCreationViewModel();
                this.allUsers = await this.authenticationService.GetAllUsers();
            }
            else
            {
                this.message = "There was an issue registering the user.";
            }
        }

        protected void OpenEditUserRoles()
        {
            this.message = string.Empty;
            this.roleModel = this.allRoles.OrderBy(x => x).FirstOrDefault();
            var userToEdit = this.allUsers.FirstOrDefault(x => x.UserName == this.selectedUserName);
            this.model = new AdminUserViewModel()
            {
                UserName = userToEdit.UserName,
                Roles = userToEdit.Roles
            };

            this.editUserRoles.Open();
        }

        protected async Task EditUserRoles(bool isAdding)
        {
            this.message = string.Empty;
            var request = new AdminRoleRequestViewModel() { RoleName = this.roleModel, Username = this.model.UserName };
            bool success;
            try
            {
                if (isAdding)
                {
                    success = await this.authenticationService.AssignRole(request);
                }
                else
                {
                    success = await this.authenticationService.RemoveRole(request);
                }
            }
            catch
            {
                success = false;
            }

            if (success)
            {
                this.editUserRoles.Close();
                this.model = new AdminUserViewModel();
                this.allUsers = await this.authenticationService.GetAllUsers();
            }
            else
            {
                this.message = " There was an issue editing the roles.";
            }
        }

        protected async Task DeleteUser()
        {
            this.message = string.Empty;
            var response = await this.authenticationService.DeleteAccount(this.selectedUserName);
            if (response)
            {
                this.deleteUser.Close();
                this.allUsers = await this.authenticationService.GetAllUsers();
            }
            else
            {
                this.message = "There was an issue deleting the account.";
            }
        }

        protected void OpenAddRole()
        {
            this.message = string.Empty;
            this.roleModel = string.Empty;
            this.addUser.Open();
        }
    }
}
