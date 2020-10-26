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
        /// Create new reservation
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateReservationAsync([FromBody] CreateReservationCommand command)
        {
            await _reservationBusiness.CreateReservationAsync(command);

            return Ok();
        }

        /// <summary>
        /// Check available rooms by current location
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost, Route("available-rooms")]
        public async Task<IActionResult> CheckAvailableRoomsAsync([FromBody] CheckAvailableRoomsCommand command)
        {
            return Ok(await _reservationBusiness.CheckAvailabilityAsync(command));
        }

        /// <summary>
        /// Get my reservations
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetMyReservations()
        {
            return Ok(await _reservationBusiness.GetMyReservationsAsync());
        }
    }
}
