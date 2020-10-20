using MassTransit;
using Reservations.Services.Contracts.Requests;
using Reservations.Services.Contracts.Responds;
using Reservations.Services.Offices.Commands;
using System.Threading.Tasks;

namespace Reservations.Services.Offices.Business.Consumers
{
    public class CheckOfficeAvailabilityConsumer : IConsumer<CheckOfficeAvailability>
    {
        private readonly IOfficeBusiness _officeBusiness;

        public CheckOfficeAvailabilityConsumer(IOfficeBusiness officeBusiness)
        {
            _officeBusiness = officeBusiness;
        }

        public async Task Consume(ConsumeContext<CheckOfficeAvailability> context)
        {
            var availableOffices = await _officeBusiness.AvailableOfficesAsync(new CheckAvailableOfficesCommand
            {
                StartTime = context.Message.StartTime,
                EndTime = context.Message.EndTime
            });

            await context.RespondAsync<OfficeAvailabilityRespond>(new
            {
                AvailableOffices = availableOffices
            });
        }
    }
}
