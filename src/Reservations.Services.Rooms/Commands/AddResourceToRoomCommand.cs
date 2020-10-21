using System;

namespace Reservations.Services.Rooms.Commands
{
    public class AddResourceToRoomCommand
    {
        public Guid ResourceId { get; set; }

        public Guid RoomId { get; set; }

        public int Quantity { get; set; }
    }
}
