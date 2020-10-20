using Microsoft.AspNetCore.Mvc;
using Reservations.Services.Reservations.Business;
using Reservations.Services.Reservations.Commands;
using System.Threading.Tasks;

namespace Reservations.Services.Reservations.Controllers
{
    [Route("api/reservations/v1/reservations")]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationBusiness _reservationBusiness;

        public ReservationController(IReservationBusiness reservationBusiness)
        {
            _reservationBusiness = reservationBusiness;
        }

        /// <summary>
        /// Create reservation
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateReservationAsync()
        {
            return Ok();
        }

        /// <summary>
        /// Check available rooms by current location
        /// </summary>
        /// <returns></returns>
        [HttpPost, Route("available-rooms")]
        public async Task<IActionResult> CheckAvailableRoomsAsync([FromBody] CheckAvailableRoomsCommand command)
        {
            return Ok(await _reservationBusiness.CheckAvailabilityAsync(command));
        }
    }
}
