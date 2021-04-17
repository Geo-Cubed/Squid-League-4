using CubedApi.Api.Commands.Matches;
using CubedApi.Api.Common.CustomExceptions;
using CubedApi.Api.Data;
using CubedApi.Api.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CubedApi.Api.Controllers
{
    [Route("_apis/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly SquidLeagueContext _context;
        private readonly MatchCommands _matchCommands;

        public MatchController(SquidLeagueContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("Context cannot be null.");
            }

            this._context = context;
            this._matchCommands = new MatchCommands(this._context);
        }

        // GET: _apis/<MatchController>
        [HttpGet]
        public ActionResult<List<Match>> GetAllMatches()
        {
            throw new NotImplementedException();
        }

        // GET: _apis/<MatchController>/swiss
        [HttpGet("swiss")]
        public ActionResult<List<Match>> GetSwissMatches()
        {
            try
            {
                return this._matchCommands.GetAllSwissMatches();
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

        // GET api/<MatchController>/5
        [HttpGet("{id}")]
        public ActionResult<Tuple<Match, List<Game>>> Get(int id)
        {
            try
            {
                return this._matchCommands.GetMatchInformationById(id);
            }
            catch (DataIsNullException)
            {
                return NotFound();
            }
            catch
            {
                return BadRequest();
            }
        }

        // POST api/<MatchController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
    }
}
