using GeoCubed.SquidLeague4.Application.Features.Swiss.Commands.CreateSwissMatch;
using GeoCubed.SquidLeague4.Application.Features.Swiss.Commands.DeleteSwissMatch;
using GeoCubed.SquidLeague4.Application.Features.Swiss.Queries.GetSwissMatchesForAdmin;
using GeoCubed.SquidLeague4.Application.Features.Swiss.Queries.GetSwissMatchesList;
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
    public class SwissController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SwissController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet("all", Name = "GetSwissMatches")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<SwissMatchDetailVm>>> GetSwissMatches()
        {
            var matches = await this._mediator.Send(new GetSwissMatchesListQuery());
            return Ok(matches);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator)]
        [HttpGet("adminall", Name = "GetAllSwissMatchesForAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<List<BracketSwissAdminVm>>> GetAllSwissMatchesForAdmin()
        {
            var swiss = await this._mediator.Send(new GetSwissMatchesForAdminQuery());
            return Ok(swiss);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator)]
        [HttpPost(Name = "AddSwissMatch")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateSwissMatchCommandResponse>> AddSwissMatch([FromBody] CreateSwissMatchCommand createSwissMatchCommand)
        {
            var response = await this._mediator.Send(createSwissMatchCommand);
            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator)]
        [HttpDelete("{id}", Name = "DeleteSwissMatch")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> DeleteSwissMatch(int id)
        {
            var request = new DeleteSwissMatchCommand(id);
            var response = await this._mediator.Send(request);
            if (response.Success)
            {
                return NoContent();
            }

            return BadRequest(response);
        }
    }
}
