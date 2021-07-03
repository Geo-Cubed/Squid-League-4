using GeoCubed.SquidLeague4.Application.Features.Results.Queries.GetFullSetInfo;
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
    }
}
