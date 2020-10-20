using MassTransit;
using Microsoft.EntityFrameworkCore;
using Reservations.Services.Contracts.Requests;
using Reservations.Services.Contracts.Responds;
using Reservations.Services.Rooms.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Reservations.Services.Rooms.Business.Consumers
{
    public class CheckRoomCapacityConsumer : IConsumer<CheckRoomCapacity>
    {
        private readonly ApplicationContext _applicationContext;

        public CheckRoomCapacityConsumer(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task Consume(ConsumeContext<CheckRoomCapacity> context)
        {
            var available = await _applicationContext.Rooms
                                                     .Where(x => x.Id == context.Message.RoomId
                                                              && x.Capacity >= context.Message.PersonCount)
                                                     .AnyAsync();

            await context.RespondAsync<RoomCapacityRespond>(new
            {
                Available = available
            });
        }
    }
}
