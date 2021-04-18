using System;
using System.Collections.Generic;
using CubedApi.Api.Commands.Teams;
using CubedApi.Api.Common.CustomExceptions;
using CubedApi.Api.Common.Utilities.Interfaces;
using CubedApi.Api.Data;
using CubedApi.Api.Models.DTOs;
using CubedApi.Api.Models.Linkers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CubedApi.Api.Controllers
{
    [Route("_apis/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly SquidLeagueContext _context;
        private readonly TeamCommands _teamCommands;

        public TeamController(SquidLeagueContext context, IMapping mapper)
        {
            if (mapper == null)
            {
                throw new ArgumentException("Mapper cannot be null.");
            }

            this._context = context ?? throw new ArgumentException("Context cannot be null.");
            this._teamCommands = new TeamCommands(this._context, mapper);
        }

        // GET: api/<TeamController> => get all active teams
        [HttpGet]
        public ActionResult<List<TeamDto>> Get()
        {
            try
            {
                return this._teamCommands.GetAllTeams();
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

        // GET api/<TeamController>/5 => Get team with id of 5 (must be active) 
        [HttpGet("{id}")]
        public ActionResult<TeamDto> Get(int id)
        {
            try
            {
                return this._teamCommands.GetTeamById(id);
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

        // GET _apis/<TeamController>/player/5 => Get the team of the player with id 5 (must be active)
        [HttpGet("player/{id}")]
        public ActionResult<TeamDto> GetTeamByPlayerId(int id)
        {
            try
            {
                return this._teamCommands.GetTeamByPlayerId(id);
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

        [HttpGet("profile")]
        public ActionResult<List<TeamProfile>> GetTeamProfiles()
        {
            try
            {
                return this._teamCommands.GetTeamProfiles();
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

        // POST api/<TeamController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
    }
}
