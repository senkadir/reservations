using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Reservations.Common.Cache
{
    public static class Extensions
    {
        public static IServiceCollection AddCaching(this IServiceCollection services)
        {
            using (var serviceProvider = services.BuildServiceProvider())
            {
                var configuration = serviceProvider.GetService<IConfiguration>();

                services.AddStackExchangeRedisCache(opt =>
                {
                    opt.Configuration = configuration.GetConnectionString("Redis");
                });

                services.AddTransient((provider) => new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(configuration.GetValue<double>("cache:AbsoluteExpiration")),
                    SlidingExpiration = TimeSpan.FromSeconds(configuration.GetValue<double>("cache:SlidingExpiration"))
                });
            }

            return services;
        }
    }
}
