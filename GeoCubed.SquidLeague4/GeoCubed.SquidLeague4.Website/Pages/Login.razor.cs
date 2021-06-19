using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Pages
{
    public partial class Login
    {
        [Inject]
        private IAuthenticationService AuthenticationService { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        public LoginViewModel LoginViewModel { get; set; }

        public string Message { get; set; }

        protected override void OnInitialized()
        {
            this.LoginViewModel = new LoginViewModel();
        }

        protected async Task LoginAsync() 
        {
            var isAuthorized = await this.AuthenticationService.Authenticate(this.LoginViewModel.Username, this.LoginViewModel.Password);
            if (isAuthorized)
            {
                NavigationManager.NavigateTo("admin/root");
            }

            this.Message = "Username or Password are incorrect";
        }
    }
}
