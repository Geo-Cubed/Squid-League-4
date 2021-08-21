using GeoCubed.SquidLeague4.Application.Features.Bracket.Commands.CreateKnockoutMatch;
using GeoCubed.SquidLeague4.Application.Features.Bracket.Commands.DeleteKnockoutMatch;
using GeoCubed.SquidLeague4.Application.Features.Bracket.Queries.GetAllLowerBracket;
using GeoCubed.SquidLeague4.Application.Features.Bracket.Queries.GetAllUpperBracket;
using GeoCubed.SquidLeague4.Application.Features.Bracket.Queries.GetKnockoutMatchInfo;
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
    public class KnockoutController : ControllerBase
    {
        private readonly IMediator _mediator;

        public KnockoutController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet("uppermatches", Name = "GetUpperMatches")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<KnockoutMatchInfo>>> GetUpperMatches()
        {
            var upper = await this._mediator.Send(new GetKnockoutMatchInfoQuery(true));
            return Ok(upper);
        }
        
        [HttpGet("lowermatches", Name = "GetLowerMatches")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<KnockoutMatchInfo>>> GetLowerMatches()
        {
            var lower = await this._mediator.Send(new GetKnockoutMatchInfoQuery(false));
            return Ok(lower);
        }

        [HttpGet("allupper", Name = "GetAllUpperBracket")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<List<UpperBracketVm>>> GetAllUpperBracket()
        {
            var upper = await this._mediator.Send(new GetAllUpperBracketQuery());
            return Ok(upper);
        }

        [HttpGet("alllower", Name = "GetAllLowerBracket")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<List<LowerBracketVm>>> GetAllLowerBracket()
        {
            var lower = await this._mediator.Send(new GetAllLowerBracketQuery());
            return Ok(lower);
        }

        [HttpPost(Name = "AddKnockoutMatch")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<CreateKnockoutMatchCommandResponse>> AddKnockoutMatch([FromBody] CreateKnockoutMatchCommand createKnockoutMatchCommand)
        {
            var response = await this._mediator.Send(createKnockoutMatchCommand);
            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpDelete("{id}", Name = "DeleteKnockoutMatch")]
        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> DeleteKnockoutMatch(int id)
        {
            var request = new DeleteKnockoutMatchCommand(id);
            var response = await this._mediator.Send(request);
            if (response.Success)
            {
                return NoContent();
            }

            return BadRequest(response);
        }
    }
}
