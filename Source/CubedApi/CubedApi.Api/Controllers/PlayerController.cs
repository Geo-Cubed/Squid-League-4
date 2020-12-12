using System.Collections.Generic;
using CubedApi.BLL.Players;
using CubedApi.CustomExceptions;
using CubedApi.Models.DatabaseTables;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CubedApi.Api.Controllers
{
    [Route("_apis/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        // GET: api/<PlayerController> => get all players
        [HttpGet]
        public ActionResult<IEnumerable<Player>> Get()
        {
            try
            {
                return (List<Player>)PlayerCommands.GetAllPlayers();
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

        // GET api/<PlayerController>/5 => get player with id of 5
        [HttpGet("{id}")]
        public ActionResult<Player> Get(int id)
        {
            try
            {
                return PlayerCommands.GetPlayerById(id);
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

        // GET _apis/<PlayerController>/team/5 => get all players on team 5
        [HttpGet("team/{id}")]
        public ActionResult<IEnumerable<Player>> GetTeamPlayers(int id)
        {
            try
            {
                return (List<Player>)PlayerCommands.GetPlayersByTeamId(id);
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

        // POST api/<PlayerController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
    }
}
