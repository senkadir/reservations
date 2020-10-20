using MassTransit;
using Microsoft.EntityFrameworkCore;
using Reservations.Services.Contracts.Requests;
using Reservations.Services.Contracts.Responds;
using Reservations.Services.Offices.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Reservations.Services.Offices.Business.Consumers
{
    public class CheckOfficeHoursAvailableConsumer : IConsumer<CheckOfficeHoursAvailable>
    {
        private readonly ApplicationContext _applicationContext;

        public CheckOfficeHoursAvailableConsumer(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task Consume(ConsumeContext<CheckOfficeHoursAvailable> context)
        {
            var available = await _applicationContext.Offices
                                                     .Where(x => x.Id == context.Message.OfficeId
                                                              && x.OpenTime <= context.Message.StartTime
                                                              && x.CloseTime >= context.Message.EndTime)
                                                     .AnyAsync();

            await context.RespondAsync<OfficeHoursAvailabilityRespond>(new
            {
                Available = available
            });
        }
    }
}
