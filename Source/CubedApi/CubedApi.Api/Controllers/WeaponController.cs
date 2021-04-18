using CubedApi.Api.Commands.Players;
using CubedApi.Api.Common.CustomExceptions;
using CubedApi.Api.Common.Utilities.Interfaces;
using CubedApi.Api.Data;
using CubedApi.Api.Models.DTOs;
using CubedApi.Api.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CubedApi.Api.Controllers
{
    [Route("_apis/[controller]")]
    [ApiController]
    public class WeaponController : ControllerBase
    {
        private readonly SquidLeagueContext _context;
        private readonly PlayerCommands _commands;
        public WeaponController(SquidLeagueContext context, IMapping mapper)
        {
            if (mapper == null)
            {
                throw new ArgumentException("Mapper cannot be null.");
            }

            this._context = context ?? throw new ArgumentException("Context cannot be null.");
            this._commands = new PlayerCommands(this._context, mapper);
        }

        // GET api/<WeaponController>/5
        [HttpGet("{id}")]
        public ActionResult<List<WeaponDto>> Get(int id)
        {
            try
            {
                return this._commands.GetCommonWeaponsForPlayer(id);
            }
            catch (NoDataException)
            {
                return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }

        // POST api/<WeaponController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
    }
}
