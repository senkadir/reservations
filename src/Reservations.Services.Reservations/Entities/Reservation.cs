using NpgsqlTypes;
using System;

namespace Reservations.Services.Reservations.Entities
{
    public class Reservation
    {
        public Guid Id { get; set; }

        public Guid RoomId { get; set; }

        public Guid CreatedBy { get; set; }

        public int PersonCount { get; set; }

        public NpgsqlRange<DateTime> Duration { get; set; }
    }
}
