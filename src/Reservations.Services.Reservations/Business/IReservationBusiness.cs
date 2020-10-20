using Reservations.Services.Common.Types;
using Reservations.Services.Reservations.Commands;
using Reservations.Services.Reservations.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Reservations.Services.Reservations.Business
{
    public interface IReservationBusiness : IBusinessBase
    {
        public Task<List<RoomViewModel>> CheckAvailabilityAsync(CheckAvailableRoomsCommand command);
    }
}
