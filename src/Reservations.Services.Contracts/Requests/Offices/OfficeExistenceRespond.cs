using System;

namespace Reservations.Services.Contracts.Requests.Offices
{
    public interface OfficeExistenceRespond
    {
        public Guid OfficeId { get; set; }

        public bool Exists { get; set; }
    }
}
