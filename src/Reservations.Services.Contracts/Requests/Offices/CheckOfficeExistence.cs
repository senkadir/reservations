using System;

namespace Reservations.Services.Contracts.Requests.Offices
{
    public interface CheckOfficeExistence
    {
        public Guid OfficeId { get; set; }
    }
}
