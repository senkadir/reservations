using System;

namespace Reservations.Services.Reservations.Commands
{
    public class CheckReservationAvailableCommand
    {
        public Guid RoomId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
