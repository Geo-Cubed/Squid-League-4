using Blazored.LocalStorage;
using GeoCubed.SquidLeague4.Website.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Pages.Admin
{
    public partial class AdminRoot
    {
        [Inject]
        private NavigationManager navigationManager { get; set; }

        [Inject]
        private IAuthenticationService authenticationService { get; set; }

        [Inject]
        private ILocalStorageService localStorageService { get; set; }

        protected string userName { get; set; }

        protected bool isAdmin { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var isValidToken = await this.authenticationService.ValidateAuthenticated();
            if (!isValidToken)
            {
                await this.authenticationService.Logout();
                this.navigationManager.NavigateTo("login");
            }

            this.userName = await this.localStorageService.GetItemAsync<string>("user");
            this.isAdmin = await this.authenticationService.CheckIfAdmin();
        }

        protected void NavigateTo(string url)
        {
            this.navigationManager.NavigateTo(url);
        }
    }
}
