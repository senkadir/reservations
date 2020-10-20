using System;

namespace Reservations.Services.Offices.Commands
{
    public class CreateOfficeCommand
    {
        public string Location { get; set; }

        public DateTime OpenTime { get; set; }

        public DateTime CloseTime { get; set; }
    }
}
