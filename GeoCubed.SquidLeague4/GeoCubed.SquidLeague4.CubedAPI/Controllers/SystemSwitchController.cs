using GeoCubed.SquidLeague4.Application.Features.Switches.Commands.CreateSwitch;
using GeoCubed.SquidLeague4.Application.Features.Switches.Commands.DeleteSwitch;
using GeoCubed.SquidLeague4.Application.Features.Switches.Commands.UpdateSwitch;
using GeoCubed.SquidLeague4.Application.Features.Switches.Queries.GetAllSwitchesForAdmin;
using GeoCubed.SquidLeague4.Domain.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.CubedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemSwitchController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SystemSwitchController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator)]
        [HttpGet("all", Name = "GetAllSystemSwitchesAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<List<SystemSwitchAdminVm>>> GetAllSystemSwitchesForAdmin()
        {
            var switches = await this._mediator.Send(new GetAllSwitchesForAdminQuery());
            return Ok(switches);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost(Name = "AddSwitch")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<CreateSwitchCommandResponse>> AddSwitch([FromBody] CreateSwitchCommand createSwitchCommand)
        {
            var response = await this._mediator.Send(createSwitchCommand);
            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator)]
        [HttpPut(Name = "UpdateSwitch")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> UpdateSwitch([FromBody] UpdateSwitchCommand updateSwitchCommand)
        {
            var response = await this._mediator.Send(updateSwitchCommand);
            if (response.Success)
            {
                return NoContent();
            }

            return BadRequest(response);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("{id}", Name = "DeleteSwitch")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> DeleteSwitch(int id)
        {
            var request = new DeleteSwitchCommand() { Id = id };
            var response = await this._mediator.Send(request);
            if (response.Success)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
