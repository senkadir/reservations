using System;

namespace Reservations.Services.Rooms.Commands
{
    public class GetRoomResourcesCommand
    {
        public Guid RoomId { get; set; }
    }
}
