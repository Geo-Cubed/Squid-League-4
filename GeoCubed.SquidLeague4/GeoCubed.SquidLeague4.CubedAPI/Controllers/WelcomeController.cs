using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.CubedAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WelcomeController : ControllerBase
    {
        [HttpGet("ping")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<string>> Welcome()
        {
            return Ok("Ahoy hoy");
        }

    }
}
