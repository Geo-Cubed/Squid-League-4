using CubedApi.BLL.HelpfulPeople;
using CubedApi.CustomExceptions;
using CubedApi.Models.DatabaseTables;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CubedApi.Api.Controllers
{
    [Route("_apis/[controller]")]
    [ApiController]
    public class HelpfulPeopleController : ControllerBase
    {
        // GET: _apis/<HelpfulPeopleController>
        [HttpGet]
        public ActionResult<IEnumerable<HelpfulPeople>> Get()
        {
            try
            {
                return (List<HelpfulPeople>)HelpfulPeopleCommands.GetAllHelpfulPeople();
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
