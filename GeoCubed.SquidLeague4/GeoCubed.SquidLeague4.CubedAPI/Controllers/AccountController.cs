using GeoCubed.SquidLeague4.Application.Interfaces.Authentication;
using GeoCubed.SquidLeague4.Application.Models.Authentication;
using GeoCubed.SquidLeague4.Domain.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.CubedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;

        public AccountController(IAuthenticationService authenticationService)
        {
            this._authenticationService = authenticationService;
        }

        [Authorize]
        [HttpGet("checkvalid", Name = "CheckValidToken")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public ActionResult<bool> CheckValidToken()
        {
            return Ok(true);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet("isadmin", Name = "CheckUserAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public ActionResult<bool> CheckUserAdmin()
        {
            return Ok(true);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet("getroles", Name = "GetUserRoles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<List<string>>> GetRolesAsync(string username)
        {
            return Ok(await this._authenticationService.GetRoles(username));
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet("getusers", Name = "GetAllUsers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<List<UserDto>>> GetAllUsers()
        {
            return Ok(await this._authenticationService.GetUsers());
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpGet("getallroles", Name = "GetAllRoles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<List<string>>> GetAllRoles()
        {
            return Ok(await this._authenticationService.GetAllRoles());
        }

        [HttpPost("authenticate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request)
        {
            return Ok(await this._authenticationService.AuthenticateAsync(request));
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<RegistrationResponse>> RegisterAsync(RegistrationRequest request)
        {
            return Ok(await this._authenticationService.RegisterAsync(request));
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("deleteaccount")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<DeleteResponse>> DeleteAsync([FromBody] DeleteRequest request)
        {
            return Ok(await this._authenticationService.DeleteAsync(request));
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPut("addroles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<RoleResponse>> AddRolesAsync([FromBody] RoleRequest request)
        {
            return Ok(await this._authenticationService.AddRole(request));
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPut("removeroles")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<RoleResponse>> RemoveRolesAsync([FromBody] RoleRequest request)
        {
            return Ok(await this._authenticationService.RemoveRole(request));
        }
    }
}
