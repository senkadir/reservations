using System;

namespace Reservations.Services.Rooms.Commands
{
    public class CreateRoomCommand
    {
        public Guid OfficeId { get; set; }

        public string Name { get; set; }

        public int Capacity { get; set; }

        public int ChairCount { get; set; }
    }
}
