using GeoCubed.SquidLeague4.Application.Features.Modes.Queries.GetAllModes;
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
    public class ModeController : ControllerBase
    {
        private readonly IMediator _mediator;

        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator)]
        [HttpGet("all", Name = "GetAllModes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<List<ModeVm>>> GetAllModes()
        {
            var modes = await this._mediator.Send(new GetAllModesQuery());
            return Ok(modes);
        }
    }
}
