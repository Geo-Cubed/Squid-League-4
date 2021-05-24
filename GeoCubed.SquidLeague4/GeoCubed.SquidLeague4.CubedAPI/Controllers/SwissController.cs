using GeoCubed.SquidLeague4.Application.Features.Swiss.Queries.GetSwissMatchesList;
using MediatR;
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
    }
}
