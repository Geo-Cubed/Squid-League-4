using GeoCubed.SquidLeague4.Application.Features.Results.Commands.DeleteGameInfo;
using GeoCubed.SquidLeague4.Application.Features.Results.Commands.SaveGameInfo;
using GeoCubed.SquidLeague4.Application.Features.Results.Queries.GetFullSetInfo;
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
    public class ResultsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ResultsController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator)]
        [HttpGet("fullSetInfo", Name = "FullSetInfo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<List<FullSetInfo>>> GetFullSetInfo(int matchId)
        {
            var games = await this._mediator.Send(new GetFullSetInfoQuery(matchId));
            return Ok(games);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator)]
        [HttpPut(Name = "SaveGameInfo")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> SaveGameInfo([FromBody] SaveGameInfoCommand gameInfo)
        {
            var response = await this._mediator.Send(gameInfo);
            if (response.Success)
            {
                return NoContent();
            }

            return BadRequest(response);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator)]
        [HttpDelete("{gameId}", Name = "DeleteGameInfo")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> DeleteGameInfo(int gameId)
        {
            var response = await this._mediator.Send(new DeleteGameInfoCommand(gameId));
            if (response.Success)
            {
                return NoContent();
            }

            return BadRequest(response);
        }
    }
}
