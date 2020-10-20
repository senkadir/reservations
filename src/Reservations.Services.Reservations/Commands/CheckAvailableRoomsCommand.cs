using System;

namespace Reservations.Services.Reservations.Commands
{
    public class CheckAvailableRoomsCommand
    {
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
