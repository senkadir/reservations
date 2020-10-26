using System;
using System.Collections.Generic;

namespace Reservations.Services.Rooms.Commands
{
    public class GetAvailableRoomsByOfficeCommand
    {
        public Guid OfficeId { get; set; }
    }
}
