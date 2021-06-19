using GeoCubed.SquidLeague4.Application.Features.LowerBracket.Queries.GetAllLowerBracket;
using GeoCubed.SquidLeague4.Application.Features.UpperBracket.Queries.GetAllUpperBracket;
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
    }
}
