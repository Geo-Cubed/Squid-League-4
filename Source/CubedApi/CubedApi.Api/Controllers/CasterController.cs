using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using CubedApi.Api.Data;
using CubedApi.Api.Commands.Casters;
using CubedApi.Api.Common.CustomExceptions;
using CubedApi.Api.Models.DTOs;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CubedApi.Api.Controllers
{
    [Route("_apis/[controller]")]
    [ApiController]
    public class CasterController : ControllerBase
    {
        private readonly SquidLeagueContext _context;
        private readonly CasterCommands _casterCommands;

        public CasterController(SquidLeagueContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("Context cannot be null.");
            }

            this._context = context;
            this._casterCommands = new CasterCommands(this._context);
        }

        // GET: _apis/<CastersController>
        [HttpGet]
        public ActionResult<List<CasterProfileDto>> Get()
        {
            try
            {
                return this._casterCommands.GetAllCasters();
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

        // GET _apis/<CastersController>/5
        [HttpGet("{id}")]
        public ActionResult<CasterProfileDto> Get(int id)
        {
            try
            {
                return this._casterCommands.GetCasterById(id);
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

        // GET _apis/<CasterController>/match/5
        [HttpGet("match/{id}")]
        public ActionResult<List<CasterProfileDto>> GetByMatchId(int id)
        {
            try
            {
                return this._casterCommands.GetCastersByMatchId(id);
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

        // POST api/<CastersController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
    }
}
