using GeoCubed.SquidLeague4.Application.Features.Games.Commands.CreateGame;
using GeoCubed.SquidLeague4.Application.Features.Games.Commands.DeleteGame;
using GeoCubed.SquidLeague4.Application.Features.Games.Commands.UpdateGame;
using GeoCubed.SquidLeague4.Application.Features.Games.Queries.GetAllGames;
using GeoCubed.SquidLeague4.Application.Features.Games.Queries.GetGamesByMatchId;
using GeoCubed.SquidLeague4.Application.Features.Games.Queries.GetGamesByTeamId;
using GeoCubed.SquidLeague4.Domain.Authorization;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.CubedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GameController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator)]
        [HttpGet("all", Name = "GetAllGames")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<GameVm>>> GetAllGames()
        {
            var allGames = await this._mediator.Send(new GetAllGamesQuery());
            return Ok(allGames);
        }

        [HttpGet("bymatchid", Name = "GamesByMatchId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<MatchGameVm>>> GetGamesByMatchId(int matchId)
        {
            var games = await this._mediator.Send(new GetGamesByMatchIdQuery() { MatchId = matchId });
            return Ok(games);
        }

        [HttpGet("byteamid", Name = "GamesByTeamId")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<TeamGameVm>>> GetGamesByTeamId(int teamId)
        {
            var games = await this._mediator.Send(new GetGamesByTeamIdQuery() { TeamId = teamId });
            return Ok(games);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator)]
        [HttpPost(Name = "AddGame")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<CreateGameCommandResponse>> AddGame([FromBody] CreateGameCommand createGameCommand)
        {
            var response = await this._mediator.Send(createGameCommand);
            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [Authorize(Roles.Admin + "," + Roles.Moderator)]
        [HttpPut(Name = "UpdateGame")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> UpdateGame([FromBody] UpdateGameCommand updateGameCommand)
        {
            var response = await this._mediator.Send(updateGameCommand);
            if (response.Success)
            {
                return NoContent();
            }

            return NotFound(response);
        }

        [Authorize(Roles.Admin + "," + Roles.Moderator)]
        [HttpDelete("{id}", Name = "DeleteGame")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> DeleteGame(int id)
        {
            var response = await this._mediator.Send(new DeleteGameCommand() { Id = id });
            if (response.Success)
            {
                return NoContent();
            }

            return NotFound(response);
        }
    }
}
