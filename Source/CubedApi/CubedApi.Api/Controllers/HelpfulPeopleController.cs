using CubedApi.Api.Commands.HelpfulPeople;
using CubedApi.Api.Common.CustomExceptions;
using CubedApi.Api.Common.Utilities.Interfaces;
using CubedApi.Api.Data;
using CubedApi.Api.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CubedApi.Api.Controllers
{
    [Route("_apis/[controller]")]
    [ApiController]
    public class HelpfulPeopleController : ControllerBase
    {
        private readonly SquidLeagueContext _context;
        private readonly HelpfulPeopleCommands _helpfulPeopleCommands;

        public HelpfulPeopleController(SquidLeagueContext context, IMapping mapper)
        {
            if (mapper == null)
            {
                throw new ArgumentException("Mapper cannot be null.");
            }

            this._context = context ?? throw new ArgumentException("Context cannot be null.");
            this._helpfulPeopleCommands = new HelpfulPeopleCommands(this._context, mapper);
        }

        // GET: _apis/<HelpfulPeopleController>
        [HttpGet]
        public ActionResult<List<HelpfulPersonDto>> Get()
        {
            try
            {
                return this._helpfulPeopleCommands.GetAllHelpfulPeople();
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

        // POST api/<HelpfulPeopleController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
    }
}
