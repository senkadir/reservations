using System;

namespace Reservations.Services.Contracts.Requests
{
    public interface CheckOfficeAvailability
    {
        public string Location { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}
