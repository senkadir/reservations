using System;

namespace Reservations.Services.Reservations.Entities
{
    public class Reservation
    {
        public Guid RoomId { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
