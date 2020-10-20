using System;
using System.Collections.Generic;

namespace Reservations.Services.Reservations.Commands
{
    public class GetAvailableRoomsByOfficesCommand
    {
        public List<Guid> Offices { get; set; }
    }
}
