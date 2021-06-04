using GeoCubed.SquidLeague4.Application.Features.Teams.Commands.CreateTeam;
using GeoCubed.SquidLeague4.Application.Features.Teams.Commands.DeleteTeam;
using GeoCubed.SquidLeague4.Application.Features.Teams.Commands.UpdateTeam;
using GeoCubed.SquidLeague4.Application.Features.Teams.Queries.GetTeamById;
using GeoCubed.SquidLeague4.Application.Features.Teams.Queries.GetTeamList;
using GeoCubed.SquidLeague4.Application.Features.Teams.Queries.GetTeamWithPlayersList;
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
    public class TeamController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TeamController(IMediator mediator)
        {
            this._mediator = mediator ?? throw new ArgumentException("Cannot have a null mediator for the team controller.");
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator)]
        [HttpGet("all", Name = "GetAllTeams")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<TeamAdminVm>>> GetAllTeams()
        {
            var teams = await this._mediator.Send(new GetTeamListQuery());
            return teams;
        }

        [HttpGet("teambyid", Name = "GetTeamById")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TeamVm>> GetTeamById(int id)
        {
            try
            {
                var request = new GetTeamByIdQuery() { Id = id };
                var team = await this._mediator.Send(request);
                return team;
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("allteamsandplayers", Name = "GetAllTeamsAndPlayers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<TeamWithPlayersVm>>> GetAllTeamsAndPlayers()
        {
            var teams = await this._mediator.Send(new GetTeamWithPlayersListQuery());
            return teams;
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator)]
        [HttpPost(Name = "AddTeam")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<CreateTeamCommandResponse>> AddTeam([FromBody] CreateTeamCommand createTeamCommand)
        {
            var response = await this._mediator.Send(createTeamCommand);
            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator)]
        [HttpPut(Name = "UpdateTeam")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateTeam([FromBody] UpdateTeamCommand updateTeamCommand)
        {
            var response = await this._mediator.Send(updateTeamCommand);
            if (response.Success)
            {
                return NoContent();
            }

            return BadRequest(response);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator)]
        [HttpDelete("{id}", Name = "DeleteTeam")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteTeam(int id)
        {
            var request = new DeleteTeamCommand() { Id = id };
            var response = await this._mediator.Send(request);
            if (response.Success)
            {
                return NoContent();
            }

            return NotFound(response);
        }
    }
}
