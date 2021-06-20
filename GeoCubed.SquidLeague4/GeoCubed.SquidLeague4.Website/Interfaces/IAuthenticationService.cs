using GeoCubed.SquidLeague4.Website.Services.Base;
using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> Authenticate(string username, string password);

        Task<bool> Register(string username, string password);

        Task<bool> DeleteAccount(string username);

        Task<bool> ValidateAuthenticated();

        Task<bool> CheckIfAdmin();

        Task<List<AdminUserViewModel>> GetAllUsers();

        Task<List<string>> GetAllRoles();

        Task<bool> AssignRole(AdminRoleRequestViewModel roleRequest);

        Task<bool> RemoveRole(AdminRoleRequestViewModel roleRequest);

        Task Logout();
    }
}
