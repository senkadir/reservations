using System;
using System.Collections.Generic;

namespace Reservations.Services.Rooms.Entities
{
    public class Room
    {
        public Guid Id { get; set; }

        public Guid OfficeId { get; set; }

        public string Name { get; set; }

        public int Capacity { get; set; }

        public int ChairCount { get; set; }

        public List<RoomResource> RoomResources { get; set; }
    }
}
