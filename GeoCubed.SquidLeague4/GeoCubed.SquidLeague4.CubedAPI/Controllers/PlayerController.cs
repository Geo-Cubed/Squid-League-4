using GeoCubed.SquidLeague4.Application.Features.Players.Commands.CreatePlayer;
using GeoCubed.SquidLeague4.Application.Features.Players.Commands.DeletePlayer;
using GeoCubed.SquidLeague4.Application.Features.Players.Commands.UpdatePlayer;
using GeoCubed.SquidLeague4.Application.Features.Players.Queries.GetPlayerDetail;
using GeoCubed.SquidLeague4.Application.Features.Players.Queries.GetPlayerList;
using GeoCubed.SquidLeague4.Domain.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.CubedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PlayerController(IMediator mediator)
        {
            this._mediator = mediator ?? throw new ArgumentException("Cannot have a null mediator for the player controller.");
        }

        [HttpGet("all", Name = "GetAllPlayers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<PlayerDetailVM>>> GetAllPlayers()
        {
            // TODO: Only get active players.
            var players = await this._mediator.Send(new GetPlayerListQuery());
            return Ok(players);
        }

        [HttpGet("playerbyid", Name = "GetPlayerById")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PlayerDetailVM>> GetPlayerById(int id)
        {
            var getPlayerQuery = new GetPlayerDetailQuery() { Id = id };
            try
            {
                var player = await this._mediator.Send(getPlayerQuery);
                return Ok(player);
            }
            catch
            {
                return NotFound();
            }
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator)]
        [HttpPost(Name = "AddPlayer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<CreatePlayerCommandResponse>> AddPlayer([FromBody] CreatePlayerCommand createPlayerCommand)
        {
            var response = await this._mediator.Send(createPlayerCommand);
            return Ok(response);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator)]
        [HttpPut(Name = "UpdatePlayer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdatePlayer([FromBody] UpdatePlayerCommand updatePlayerCommand)
        {
            var response = await this._mediator.Send(updatePlayerCommand);
            if (response.Success)
            {
                return NoContent();
            }

            return NotFound(response);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator)]
        [HttpDelete("{id}", Name = "DeletePlayer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeletePlayer(int id)
        {
            var command = new DeletePlayerCommand() { Id = id };
            var response = await this._mediator.Send(command);
            if (response.Success)
            {
                return NoContent();
            }

            return NotFound(response);
        }
    }
}
