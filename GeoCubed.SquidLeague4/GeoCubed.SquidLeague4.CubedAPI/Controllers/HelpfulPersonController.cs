using GeoCubed.SquidLeague4.Application.Features.HelpfulPeople.Commands.CreateHelpfulPerson;
using GeoCubed.SquidLeague4.Application.Features.HelpfulPeople.Commands.DeleteHelpfulPerson;
using GeoCubed.SquidLeague4.Application.Features.HelpfulPeople.Commands.UpdateHelpfulPerson;
using GeoCubed.SquidLeague4.Application.Features.HelpfulPeople.Queries.GetHelpfulPersonById;
using GeoCubed.SquidLeague4.Application.Features.HelpfulPeople.Queries.GetHelpfulPersonList;
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
    public class HelpfulPersonController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HelpfulPersonController(IMediator mediator)
        {
            this._mediator = mediator ??
                throw new ArgumentException("Cannot have a null mediator for the helpful person controller.");
        }

        [HttpGet("all", Name = "GetAllHelpfulPeople")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<HelpfulPersonVm>>> GetAllHelpfulPeople()
        {
            var people = await this._mediator.Send(new GetHelpfulPersonListQuery());
            return Ok(people);
        }

        [HttpGet("byid", Name = "HelpfulPersonById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<HelpfulPersonVm>> GetHelpfulPersonById(int id)
        {
            var personQuery = new GetHelpfulPersonByIdQuery() { Id = id };
            try
            {
                var person = await this._mediator.Send(personQuery);
                return Ok(person);
            }
            catch
            {
                return NotFound();
            }
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator)]
        [HttpPost(Name = "AddHelpfulPerson")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<CreateHelpfulPersonCommandResponse>> CreateHelpfulPerson([FromBody] CreateHelpfulPersonCommand createHelpfulPersonCommand)
        {
            var response = await this._mediator.Send(createHelpfulPersonCommand);
            return Ok(response);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator)]
        [HttpPut(Name = "UpdateHelpfulPerson")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> UpdateHelpfulPerson([FromBody] UpdateHelpfulPersonCommand updateHelpfulPersonCommand)
        {
            var response = await this._mediator.Send(updateHelpfulPersonCommand);
            if (response.Success)
            {
                return NoContent();
            }

            return NotFound(response);
        }

        [Authorize(Roles = Roles.Admin + "," + Roles.Moderator)]
        [HttpDelete("{id}", Name = "DeleteHelpfulPerson")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteHelpfulPerson(int id)
        {
            var command = new DeleteHelpfulPersonCommand() { Id = id };
            var response = await this._mediator.Send(command);
            if (response.Success)
            {
                return NoContent();
            }

            return NotFound(response);
        }
    }
}
