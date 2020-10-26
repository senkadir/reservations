using System;

namespace Reservations.Services.Contracts.Responds
{
    public interface OfficeAvailabilityRespond
    {
        bool Available { get; set; }

        Guid OfficeId { get; set; }
    }
}
