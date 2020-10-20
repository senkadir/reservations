using Microsoft.AspNetCore.Mvc;
using Reservations.Services.Rooms.Business;
using Reservations.Services.Rooms.Commands;
using System.Threading.Tasks;

namespace Reservations.Services.Rooms.Controllers
{
    [Route("api/rooms/v1/rooms")]
    public class RoomController : ControllerBase
    {
        private readonly IRoomBusiness _roomBusiness;

        public RoomController(IRoomBusiness roomBusiness)
        {
            _roomBusiness = roomBusiness;
        }

        /// <summary>
        /// Create new room
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateRoomAsync([FromBody] CreateRoomCommand command)
        {
            await _roomBusiness.CreateAsync(command);

            return Ok();
        }

        /// <summary>
        /// Get available rooms by offices
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost, Route("by-offices")]
        public async Task<IActionResult> GetRoomsByOffice([FromBody] GetAvailableRoomsByOfficesCommand command)
        {
            return Ok(await _roomBusiness.AvailableRoomsByOfficesAsync(command));
        }
    }
}
