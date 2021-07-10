using GeoCubed.SquidLeague4.Application.Features.Weapons.Queries.GetBasicWeaponInfo;
using GeoCubed.SquidLeague4.Application.Features.Weapons.Queries.GetWeaponList;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.CubedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeaponController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WeaponController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet("all", Name = "getallweapons")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<WeaponVm>>> GetAllWeapons()
        {
            var weapons = await this._mediator.Send(new GetWeaponListQuery());
            return weapons;
        }

        [HttpGet("basic", Name = "GetBasicWeaponInfo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<BasicWeaponInfoVm>>> GetBasicWeaponInfo()
        {
            var weapons = await this._mediator.Send(new GetBasicWeaponInfoQuery());
            return Ok(weapons);
        }
    }
}
