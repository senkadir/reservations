using System;

namespace Reservations.Services.Rooms.Models
{
    public class RoomResourceViewModel
    {
        public Guid RoomId { get; set; }

        public string RoomName { get; set; }

        public Guid ResourceId { get; set; }

        public string ResourceName { get; set; }

        public int ResourceTotalQuantity { get; set; }

        public int ResourceUsedQuantity { get; set; }
    }
}
