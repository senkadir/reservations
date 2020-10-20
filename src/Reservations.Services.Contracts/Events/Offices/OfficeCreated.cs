using System;

namespace Reservations.Services.Contracts.Events
{
    public interface OfficeCreated
    {
        public Guid OfficeId { get; set; }
    }
}
