using System;

namespace Reservations.Services.Contracts.Requests
{
    public interface CheckRoomCapacity
    {
        public Guid RoomId { get; set; }

        public int PersonCount { get; set; }
    }
}
