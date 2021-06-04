using GeoCubed.SquidLeague4.Application.Features.Casters.Commands.CreateCaster;
using GeoCubed.SquidLeague4.Application.Features.Casters.Commands.DeleteCaster;
using GeoCubed.SquidLeague4.Application.Features.Casters.Commands.UpdateCaster;
using GeoCubed.SquidLeague4.Application.Features.Casters.Queries.GetCasterById;
using GeoCubed.SquidLeague4.Application.Features.Casters.Queries.GetCasterForAdmin;
using GeoCubed.SquidLeague4.Application.Features.Casters.Queries.GetCasterList;
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
    public class CasterController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CasterController(IMediator mediator)
        {
            this._mediator = mediator ?? throw new ArgumentException("Cannot have a null mediator for the caster controller");
        }

        [HttpGet("all", Name = "GetAllCasters")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CasterVm>>> GetAllCasters()
        {
            var request = new GetCasterListQuery();
            var casters = await this._mediator.Send(request);
            return Ok(casters);
        }

        [Authorize(Roles = Roles.Moderator + "," + Roles.Admin)]
        [HttpGet("adminall", Name = "GetAllCastersForAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CasterAdminVm>>> GetAllCastersForAdmin()
        {
            var casters = await this._mediator.Send(new GetCasterForAdminQuery());
            return Ok(casters);
        }

        [HttpGet("casterbyid", Name = "GetCasterById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CasterVm>> GetCasterById(int id)
        {
            try
            {
                var request = new GetCasterByIdQuery() { Id = id };
                var caster = await this._mediator.Send(request);
                return Ok(caster);
            }
            catch
            {
                return NotFound();
            }
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator)]
        [HttpPost(Name = "AddCaster")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<CreateCasterCommandResponse>> AddCaster([FromBody] CreateCasterCommand createCasterCommand)
        {
            var response = await this._mediator.Send(createCasterCommand);
            if (response.Success)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator)]
        [HttpPut(Name = "UpdateCaster")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> UpdateCaster([FromBody] UpdateCasterCommand updateCasterCommand)
        {
            var response = await this._mediator.Send(updateCasterCommand);
            if (response.Success)
            {
                return NoContent();
            }

            return BadRequest(response);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator)]
        [HttpDelete("{id}", Name = "DeleteCaster")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> DeleteCaster(int id)
        {
            var request = new DeleteCasterCommand() { Id = id };
            var response = await this._mediator.Send(request);
            if (response.Success)
            {
                return NoContent();
            }

            return NotFound(response);
        }
    }
}
