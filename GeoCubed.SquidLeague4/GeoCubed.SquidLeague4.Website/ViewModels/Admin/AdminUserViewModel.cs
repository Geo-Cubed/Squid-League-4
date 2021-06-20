using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Website.ViewModels.Admin
{
    public class AdminUserViewModel
    {
        public AdminUserViewModel()
        {
            this.Roles = new List<string>();
        }

        public string UserName { get; set; }

        public List<string> Roles { get; set; }
    }
}
