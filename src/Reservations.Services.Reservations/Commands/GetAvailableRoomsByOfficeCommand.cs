using System;

namespace Reservations.Services.Reservations.Commands
{
    public class GetAvailableRoomsByOfficeCommand
    {
        public Guid OfficeId { get; set; }
    }
}
