using System;

namespace Reservations.Services.Contracts.Requests
{
    public interface CheckOfficeExistence
    {
        public Guid OfficeId { get; set; }
    }
}
