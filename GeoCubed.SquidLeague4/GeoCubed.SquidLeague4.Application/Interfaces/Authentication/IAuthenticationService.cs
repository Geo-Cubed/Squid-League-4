using GeoCubed.SquidLeague4.Application.Models.Authentication;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Interfaces.Authentication
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);

        Task<RegistrationResponse> RegisterAsync(RegistrationRequest request);

        Task<DeleteResponse> DeleteAsync(DeleteRequest request);

        Task<List<UserDto>> GetUsers();

        Task<List<string>> GetAllRoles();

        Task<List<string>> GetRoles(string username);

        Task<RoleResponse> AddRole(RoleRequest request);

        Task<RoleResponse> RemoveRole(RoleRequest request);
    }
}
