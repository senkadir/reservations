using System;

namespace Reservations.Services.Offices.Commands
{
    public class CheckAvailableOfficesCommand
    {
        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}
