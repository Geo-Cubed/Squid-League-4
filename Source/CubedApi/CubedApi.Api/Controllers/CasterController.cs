using CubedApi.Models.DatabaseTables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CubedApi.BLL.Casters;
using CubedApi.CustomExceptions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CubedApi.Api.Controllers
{
    [Route("_apis/[controller]")]
    [ApiController]
    public class CasterController : ControllerBase
    {
        // GET: _apis/<CastersController>
        [HttpGet]
        public ActionResult<IEnumerable<CasterProfile>> Get()
        {
            try
            {
                return (List<CasterProfile>)CasterCommands.GetAllCasters();
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
        public ActionResult<CasterProfile> Get(int id)
        {
            try
            {
                return CasterCommands.GetCasterById(id);
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
        public ActionResult<CasterProfile> GetByMatchId(int id)
        {
            try
            {
                return CasterCommands.GetCasterByMatchId(id);
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
