using MassTransit;
using Reservations.Services.Contracts.Requests;
using Reservations.Services.Contracts.Responds;
using System.Threading.Tasks;

namespace Reservations.Services.Offices.Business.Consumers
{
    public class CheckOfficeExistenceConsumer : IConsumer<CheckOfficeExistence>
    {
        private readonly IOfficeBusiness _officeBusiness;

        public CheckOfficeExistenceConsumer(IOfficeBusiness officeBusiness)
        {
            _officeBusiness = officeBusiness;
        }

        public async Task Consume(ConsumeContext<CheckOfficeExistence> context)
        {
            var exists = await _officeBusiness.ExistsAsync(context.Message.OfficeId);

            await context.RespondAsync<OfficeExistenceRespond>(new
            {
                context.Message.OfficeId,
                Exists = exists
            });
        }
    }
}
