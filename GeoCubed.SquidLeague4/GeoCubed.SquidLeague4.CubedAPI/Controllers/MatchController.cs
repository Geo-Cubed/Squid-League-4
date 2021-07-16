using GeoCubed.SquidLeague4.Application.Features.Matches.Commands.CreateMatch;
using GeoCubed.SquidLeague4.Application.Features.Matches.Commands.DeleteMatch;
using GeoCubed.SquidLeague4.Application.Features.Matches.Commands.UpdateMatch;
using GeoCubed.SquidLeague4.Application.Features.Matches.Queries.GetAllMatchesForAdmin;
using GeoCubed.SquidLeague4.Application.Features.Matches.Queries.GetBasicMatchInfo;
using GeoCubed.SquidLeague4.Application.Features.Matches.Queries.GetMatchById;
using GeoCubed.SquidLeague4.Application.Features.Matches.Queries.GetMatchInfo;
using GeoCubed.SquidLeague4.Application.Features.Matches.Queries.GetMatchList;
using GeoCubed.SquidLeague4.Application.Features.Matches.Queries.GetTeamPlayedMatches;
using GeoCubed.SquidLeague4.Application.Features.Matches.Queries.GetUpcommingMatchesList;
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
    public class MatchController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MatchController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet("all", Name = "GetAllMatches")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<MatchDetailVm>>> GetAllMatchess()
        {
            var matches = await this._mediator.Send(new GetMatchListQuery());
            return Ok(matches);
        }

        [HttpGet("matchbyid", Name = "GetMatchById")]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MatchDetailVm>> GetPlayerById(int id)
        {
            var getMatchQuery = new GetMatchByIdQuery() { Id = id };
            try
            {
                var match = await this._mediator.Send(getMatchQuery);
                return Ok(match);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet("upcomming", Name = "GetUpcommingMatches")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<UpcommingMatchDetailVm>>> GetUpcommingMatches()
        {
            var matches = await this._mediator.Send(new GetUpcommingMatchesListQuery());
            return Ok(matches);
        }

        [HttpGet("teamplayed", Name = "GetTeamPlayedMatches")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<TeamPlayedMatchVm>>> GetTeamPlayedMatches(int teamId)
        {
            var matches = await this._mediator.Send(new GetTeamPlayedMatchesQuery() { TeamId = teamId });
            return Ok(matches);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator)]
        [HttpGet("alladmin", Name = "GetAllMatchesForAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<List<MatchAdminVm>>> GetMatchesForAdmin()
        {
            var matches = await this._mediator.Send(new GetAllMatchesForAdminQuery());
            return Ok(matches);
        }

        [HttpGet("info", Name = "GetMatchInfo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<MatchInfoVm>>> GetMatchInfo()
        {
            var matches = await this._mediator.Send(new GetMatchInfoQuery());
            return Ok(matches);
        }

        
        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator)]
        [HttpGet("basicInfo", Name = "GetBasicMatchInfo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<List<BasicMatchInfoVm>>> GetBasicMatchInfo()
        {
            var matches = await this._mediator.Send(new GetBasicMatchInfoQuery());
            return Ok(matches);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator)]
        [HttpPost(Name = "AddMatch")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<CreateMatchCommandResponse>> AddMatch([FromBody] CreateMatchCommand createMatchCommand)
        {
            var response = await this._mediator.Send(createMatchCommand);
            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator)]
        [HttpPut(Name = "UpdateMatch")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdatePlayer([FromBody] UpdateMatchCommand updateMatchCommand)
        {
            var response = await this._mediator.Send(updateMatchCommand);
            if (response.Success)
            {
                return NoContent();
            }

            return BadRequest(response);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator)]
        [HttpDelete("{id}", Name = "DeleteMatch")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteMatch(int id)
        {
            var command = new DeleteMatchCommand() { Id = id };
            var response = await this._mediator.Send(command);
            if (response.Success)
            {
                return NoContent();
            }

            return NotFound(response);
        }
    }
}
