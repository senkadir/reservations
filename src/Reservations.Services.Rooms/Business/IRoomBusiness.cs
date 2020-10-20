using Reservations.Services.Common.Types;
using Reservations.Services.Rooms.Commands;
using System.Threading.Tasks;

namespace Reservations.Services.Rooms.Business
{
    public interface IRoomBusiness : IBusinessBase
    {
        public Task CreateAsync(CreateRoomCommand command);
    }
}
