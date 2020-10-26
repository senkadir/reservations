using System;

namespace Reservations.Services.Reservations.Entities
{
    public class Resource
    {
        public Guid Id { get; set; }

        public Guid ReservationId { get; set; }

        public Guid ResourceId { get; set; }

        public Reservation Reservation { get; set; }
    }
}
