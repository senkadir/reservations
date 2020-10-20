using System;

namespace Reservations.Services.Contracts.Requests
{
    public interface CheckOfficeHoursAvailable
    {
        public Guid OfficeId { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}
