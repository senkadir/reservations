using Microsoft.AspNetCore.Mvc;

namespace Reservations.Services.Offices.Controllers
{
    [Route("api/rooms/v1/service")]
    public class ServiceController : ControllerBase
    {
        /// <summary>
        /// Service health check endpoint
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Ping()
        {
            return Ok();
        }
    }
}
