using System;

namespace Reservations.Services.Offices.Commands
{
    public class CheckOfficeAvailailityCommand
    {
        public string Location { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}
