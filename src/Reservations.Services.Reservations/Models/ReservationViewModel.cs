using System;
using System.Collections.Generic;

namespace Reservations.Services.Reservations.Models
{
    public class ReservationViewModel
    {
        public Guid Id { get; set; }

        public Guid CreatedBy { get; set; }

        public Guid RoomId { get; set; }

        public int PersonCount { get; set; }

        public DateTime Start { get; set; }

        public DateTime End { get; set; }

        public List<ReservationResourceViewModel> Resources { get; set; }
    }
}
