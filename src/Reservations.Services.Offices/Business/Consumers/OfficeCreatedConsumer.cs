using MassTransit;
using Microsoft.Extensions.Caching.Distributed;
using Reservations.Services.Contracts.Events.Offices;
using System.Threading.Tasks;

namespace Reservations.Services.Offices.Business.Consumers
{
    public class OfficeCreatedConsumer : IConsumer<OfficeCreated>
    {
        private readonly IDistributedCache _distributedCache;

        public OfficeCreatedConsumer(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public async Task Consume(ConsumeContext<OfficeCreated> context)
        {
            await _distributedCache.RemoveAsync(CacheKeys.Officess);
        }
    }
}
