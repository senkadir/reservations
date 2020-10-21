using System;

namespace Reservations.Services.Offices.Commands
{
    public class CreateOfficeCommand
    {
        public string Location { get; set; }

        public TimeSpan OpenTime { get; set; }

        public TimeSpan CloseTime { get; set; }
    }
}
