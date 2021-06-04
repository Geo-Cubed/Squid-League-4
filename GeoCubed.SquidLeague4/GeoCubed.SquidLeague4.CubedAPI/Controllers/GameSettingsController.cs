using GeoCubed.SquidLeague4.Application.Features.GameSettings.Commands.CreateGameSetting;
using GeoCubed.SquidLeague4.Application.Features.GameSettings.Commands.DeleteGameSetting;
using GeoCubed.SquidLeague4.Application.Features.GameSettings.Commands.UpdateGameSetting;
using GeoCubed.SquidLeague4.Application.Features.GameSettings.Queries.GetGameSettingsForAdmin;
using GeoCubed.SquidLeague4.Application.Features.GameSettings.Queries.GetMapLists;
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
    public class GameSettingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GameSettingsController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet("maplist", Name = "GetMapLists")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<MapListVm>>> GetMapLists()
        {
            var mapLists = await this._mediator.Send(new GetMapListsQuery());
            return Ok(mapLists);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator)]
        [HttpGet("all", Name = "GetGameSettingsForAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<List<GameSettingAdminVm>>> GetGameSettingsForAdmin()
        {
            var gameSettings = await this._mediator.Send(new GetGameSettingsForAdminQuery());
            return Ok(gameSettings);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator)]
        [HttpPost(Name = "AddGameSetting")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<CreateGameSettingCommandResponse>> AddGameSetting([FromBody] CreateGameSettingCommand createGameSettingCommand)
        {
            var response = await this._mediator.Send(createGameSettingCommand);
            if (response.Success)
            {
                Ok(response);
            }

            return BadRequest(response);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator)]
        [HttpPut(Name = "UpdateGameSetting")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> UpdateGameSetting([FromBody] UpdateGameSettingCommand updateGameSettingCommand)
        {
            var response = await this._mediator.Send(updateGameSettingCommand);
            if (response.Success)
            {
                return NoContent();
            }

            return BadRequest(response);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator)]
        [HttpDelete("{id}", Name = "DeleteGameSetting")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> DeleteGameSetting(int id)
        {
            var request = new DeleteGameSettingCommand() { Id = id };
            var response = await this._mediator.Send(request);
            if (response.Success)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}