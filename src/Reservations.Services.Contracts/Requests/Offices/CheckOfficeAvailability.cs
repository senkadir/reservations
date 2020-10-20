using System;

namespace Reservations.Services.Contracts.Requests
{
    public interface CheckOfficeAvailability
    {
        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}
