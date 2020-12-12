using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CubedApi.Api.Controllers
{
    [Route("_apis/[controller]")]
    [ApiController]
    public class WelcomeController : ControllerBase
    {
        // GET: api/<WelcomeController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Welcome to the CubedApi for squid league 4", "For a full list of api commands see https://github.com/Geo-Cubed/Squid-League-4" };
        }

        // POST api/<WelcomeController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
    }
}
