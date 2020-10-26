using Reservations.Services.Reservations.Commands;
using Reservations.Services.Reservations.Models;
using RestEase;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reservations.Services.Reservations.ExternalServices
{
    public interface IRoomService
    {
        [Post("api/rooms/v1/rooms/by-offices")]
        Task<List<RoomViewModel>> GetRoomsByOfficeAsync([Body] GetAvailableRoomsByOfficeCommand command);
    }
}
