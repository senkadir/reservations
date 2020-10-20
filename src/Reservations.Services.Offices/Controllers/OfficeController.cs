using Microsoft.AspNetCore.Mvc;
using Reservations.Services.Offices.Business;
using Reservations.Services.Offices.Commands;
using System.Threading.Tasks;

namespace Reservations.Services.Offices.Controllers
{
    [Route("api/offices/v1/offices")]
    public class OfficeController : ControllerBase
    {
        private readonly IOfficeBusiness _officeBusiness;

        public OfficeController(IOfficeBusiness officeBusiness)
        {
            _officeBusiness = officeBusiness;
        }

        /// <summary>
        /// Creates new office
        /// </summary>
        /// <param name="createOfficeCommand"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateOfficeAsync([FromBody] CreateOfficeCommand createOfficeCommand)
        {
            await _officeBusiness.CreateAsync(createOfficeCommand);

            return Ok();
        }

        /// <summary>
        /// Get current offices
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetOfficesAsync()
        {
            return Ok(await _officeBusiness.GetAsync());
        }

        [HttpPost, Route("availables")]
        public async Task<IActionResult> CheckAvailableOffices([FromBody] CheckAvailableOfficesCommand command)
        {
            return Ok(await _officeBusiness.AvailableOfficesAsync(command));
        }
    }
}