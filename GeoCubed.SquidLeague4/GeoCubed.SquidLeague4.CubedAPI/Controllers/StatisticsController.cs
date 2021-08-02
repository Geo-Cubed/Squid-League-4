using GeoCubed.SquidLeague4.Application.Features.Stats.Commands.CreateStats;
using GeoCubed.SquidLeague4.Application.Features.Stats.Commands.DeleteStats;
using GeoCubed.SquidLeague4.Application.Features.Stats.Commands.UpdateStats;
using GeoCubed.SquidLeague4.Application.Features.Stats.Queries.GetAllStats;
using GeoCubed.SquidLeague4.Application.Features.Stats.Queries.GetAllStatsForAdmin;
using GeoCubed.SquidLeague4.Application.Features.Stats.Queries.GetStatsData;
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
    public class StatisticsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StatisticsController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet("all", Name = "GetAllStats")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<StatsInfoVm>>> GetAllStats()
        {
            var stats = await this._mediator.Send(new GetAllStatsQuery());
            return Ok(stats);
        }
        
        [Authorize(Roles = Roles.Admin)]
        [HttpGet("alladmin", Name = "GetAllStatsForAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<List<AdminStatsVm>>> GetAllStatsForAdmin()
        {
            var stats = await this._mediator.Send(new GetAllStatsForAdminQuery());
            return Ok(stats);
        }

        [HttpGet("stats", Name = "GetStatsDataById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<StatsDataVm>>> GetStatsDataById(int statsId)
        {
            var response = await this._mediator.Send(new GetStatsDataQuery(statsId, "idk"));
            if (response != null)
            {
                return Ok(response);
            }

            return BadRequest();
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPost(Name = "AddStats")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> AddStats([FromBody] CreateStatsCommand createStatsCommand)
        {
            var response = await this._mediator.Send(createStatsCommand);
            if (response.Success)
            {
                return NoContent();
            }

            return BadRequest(response);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpPut(Name = "UpdateStats")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> UpdateStats([FromBody] UpdateStatsCommand updateStatsCommand)
        {
            var response = await this._mediator.Send(updateStatsCommand);
            if (response.Success)
            {
                return NoContent();
            }

            return BadRequest(response);
        }

        [Authorize(Roles = Roles.Admin)]
        [HttpDelete("{id}", Name = "DeleteStats")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> DeleteStats(int id)
        {
            var response = await this._mediator.Send(new DeleteStatsCommand(id));
            if (response.Success)
            {
                return NoContent();
            }

            return BadRequest();
        }
    }
}
