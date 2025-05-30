using Microsoft.AspNetCore.Mvc;

namespace CountryCatalogAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class ApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetStatus()
        {
            return Ok("server is running");
        }

        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("pong");
        }
    }
}