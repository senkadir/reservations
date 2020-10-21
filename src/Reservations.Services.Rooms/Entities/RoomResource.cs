using System;

namespace Reservations.Services.Rooms.Entities
{
    public class RoomResource
    {
        public Guid Id { get; set; }

        public Guid RoomId { get; set; }

        public Guid ResourceId { get; set; }

        public int Quantity { get; set; }

        public Room Room { get; set; }

        public Resource Resource { get; set; }
    }
}
