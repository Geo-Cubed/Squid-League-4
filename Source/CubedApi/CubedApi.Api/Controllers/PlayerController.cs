using System;
using System.Collections.Generic;
using CubedApi.Api.Commands.Players;
using CubedApi.Api.Common.CustomExceptions;
using CubedApi.Api.Data;
using CubedApi.Api.Models.DTOs;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CubedApi.Api.Controllers
{
    [Route("_apis/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly SquidLeagueContext _context;
        private readonly PlayerCommands _playerCommands;

        public PlayerController(SquidLeagueContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("Cannot have a null context.");
            }

            this._context = context;
            this._playerCommands = new PlayerCommands(this._context);
        }

        // GET: api/<PlayerController> => get all players
        [HttpGet]
        public ActionResult<List<PlayerDto>> Get()
        {
            try
            {
                return this._playerCommands.GetAllPlayers();
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
        public ActionResult<PlayerDto> Get(int id)
        {
            try
            {
                return this._playerCommands.GetPlayerById(id);
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
        public ActionResult<List<PlayerDto>> GetTeamPlayers(int id)
        {
            try
            {
                return this._playerCommands.GetPlayersByTeamId(id);
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
