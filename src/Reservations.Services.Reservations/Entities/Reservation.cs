using NpgsqlTypes;
using System;

namespace Reservations.Services.Reservations.Entities
{
    public class Reservation
    {
        public Guid Id { get; set; }

        public Guid RoomId { get; set; }

        public int PersonCount { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public NpgsqlRange<DateTime> Duration { get; set; }
    }
}
