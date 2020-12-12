using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CubedApi.BLL.Teams;
using CubedApi.CustomExceptions;
using CubedApi.Models.DatabaseTables;
using CubedApi.Models.ModelLinkers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CubedApi.Api.Controllers
{
    [Route("_apis/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        // GET: api/<TeamController> => get all active teams
        [HttpGet]
        public ActionResult<IEnumerable<Team>> Get()
        {
            try
            {
                return (List<Team>)TeamCommands.GetAllTeams();
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
        public ActionResult<Team> Get(int id)
        {
            try
            {
                return TeamCommands.GetTeamById(id);
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
        public ActionResult<Team> GetTeamByPlayerId(int id)
        {
            try
            {
                return TeamCommands.GetTeamByPlayerId(id);
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
        public ActionResult<IEnumerable<TeamPlayers>> GetTeamProfiles()
        {
            try
            {
                return (List<TeamPlayers>)TeamCommands.GetTeamProfiles();
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
