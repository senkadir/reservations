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
            var officeId = await _officeBusiness.AvailableOfficesAsync(new CheckOfficeAvailailityCommand
            {
                Location = context.Message.Location,
                StartTime = context.Message.StartTime,
                EndTime = context.Message.EndTime
            });

            await context.RespondAsync<OfficeAvailabilityRespond>(new
            {
                Available = officeId != null,
                OfficeId = officeId
            });
        }
    }
}
