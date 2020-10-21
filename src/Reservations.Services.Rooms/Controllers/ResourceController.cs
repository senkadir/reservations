using Microsoft.AspNetCore.Mvc;
using Reservations.Services.Rooms.Business;
using Reservations.Services.Rooms.Commands;
using System.Threading.Tasks;

namespace Reservations.Services.Rooms.Controllers
{
    [Route("api/rooms/v1/resources")]
    public class ResourceController : ControllerBase
    {
        private readonly IResourceBusiness _resourceBusiness;

        public ResourceController(IResourceBusiness resourceBusiness)
        {
            _resourceBusiness = resourceBusiness;
        }

        /// <summary>
        /// Create new resource
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateResource([FromBody] CreateResourceCommand command)
        {
            await _resourceBusiness.CreateResourceAsync(command);

            return Ok();
        }
    }
}
