using System;

namespace Reservations.Services.Contracts.Responds
{
    public interface OfficeExistenceRespond
    {
        public Guid OfficeId { get; set; }

        public bool Exists { get; set; }
    }
}
