using System;
using System.Collections.Generic;

namespace Reservations.Services.Rooms.Commands
{
    public class GetAvailableRoomsByOfficesCommand
    {
        public List<Guid> Offices { get; set; }
    }
}
