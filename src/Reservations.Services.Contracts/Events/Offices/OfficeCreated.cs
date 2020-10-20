using System;

namespace Reservations.Services.Contracts.Events.Offices
{
    public interface OfficeCreated
    {
        public Guid OfficeId { get; set; }
    }
}
