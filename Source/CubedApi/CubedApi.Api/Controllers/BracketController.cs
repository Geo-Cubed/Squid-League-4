using System;
using System.Collections.Generic;
using CubedApi.Api.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CubedApi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BracketController : ControllerBase
    {
        private readonly SquidLeagueContext _context;

        public BracketController(SquidLeagueContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("Context cannot be null.");
            }

            this._context = context;
        }

        // GET: api/<BracketController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<BracketController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BracketController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
    }
}
