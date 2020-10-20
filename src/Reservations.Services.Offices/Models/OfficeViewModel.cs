using System;

namespace Reservations.Services.Offices.Models
{
    public class OfficeViewModel
    {
        public Guid Id { get; set; }

        public string Location { get; set; }

        public TimeSpan OpenTime { get; set; }

        public TimeSpan CloseTime { get; set; }
    }
}
