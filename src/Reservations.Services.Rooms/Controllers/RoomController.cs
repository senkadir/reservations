using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reservations.Services.Rooms.Business;
using Reservations.Services.Rooms.Commands;
using System;
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
        [HttpPost, Route("by-offices"), AllowAnonymous]
        public async Task<IActionResult> GetRoomsByOfficeAsync([FromBody] GetAvailableRoomsByOfficeCommand command)
        {
            return Ok(await _roomBusiness.AvailableRoomsByOfficeAsync(command));
        }

        /// <summary>
        /// Add resource to room
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost, Route("resource")]
        public async Task<IActionResult> AddResourceToRoomAsync([FromBody] AddResourceToRoomCommand command)
        {
            await _roomBusiness.AddResourceAsync(command);

            return Ok();
        }

        /// <summary>
        /// Get which resources are in which rooms
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("resources")]
        public async Task<IActionResult> GetRoomsResourcesAsync()
        {
            return Ok(await _roomBusiness.GetRoomsResourcesAsync());
        }

        /// <summary>
        /// Get room is resources
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("{id:guid}/resources")]
        public async Task<IActionResult> GetRoomResourcesAsync([FromRoute] Guid id)
        {
            return Ok(await _roomBusiness.GetRoomResources(new GetRoomResourcesCommand
            {
                RoomId = id
            }));
        }
    }
}
