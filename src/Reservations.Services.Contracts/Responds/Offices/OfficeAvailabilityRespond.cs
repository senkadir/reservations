using System;
using System.Collections.Generic;

namespace Reservations.Services.Contracts.Responds
{
    public interface OfficeAvailabilityRespond
    {
        List<Guid> AvailableOffices { get; set; }
    }
}
