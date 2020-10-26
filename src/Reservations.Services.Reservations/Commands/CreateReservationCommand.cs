using System;
using System.Collections.Generic;

namespace Reservations.Services.Reservations.Commands
{
    public class CreateReservationCommand
    {
        public Guid OfficeId { get; set; }

        public Guid RoomId { get; set; }

        public int PersonCount { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public List<Guid> Resources { get; set; }
    }
}
