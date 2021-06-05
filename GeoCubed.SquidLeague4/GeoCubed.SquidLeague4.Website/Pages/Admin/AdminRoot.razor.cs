using Microsoft.AspNetCore.Components;

namespace GeoCubed.SquidLeague4.Website.Pages.Admin
{
    public partial class AdminRoot
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        protected void NavigateTo(string url)
        {
            this.NavigationManager.NavigateTo(url);
        }
    }
}
