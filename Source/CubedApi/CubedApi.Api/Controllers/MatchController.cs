using CubedApi.BLL.Matches;
using CubedApi.CustomExceptions;
using CubedApi.Models.DatabaseTables;
using CubedApi.Models.ModelLinkers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CubedApi.Api.Controllers
{
    [Route("_apis/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
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
                return (List<Match>)MatchCommands.GetAllSwissMatches();
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
        public ActionResult<SingleMatchInformation> Get(int id)
        {
            try
            {
                return MatchCommands.GetMatchInformationById(id);
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
