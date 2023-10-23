using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace Template.WebAPI.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Consumes(MediaTypeNames.Application.Json)] // "application/json"
    [Route("[controller]")]
    public class MainController : ControllerBase
    {
        

        public MainController()
        {
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }
    }
}