using Consul;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Reservations.Common.Consul
{
    public static class Extensions
    {
        public static IServiceCollection AddConsul(this IServiceCollection services)
        {
            IConfiguration configuration;

            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }

            services.Configure<ConsulOptions>(configuration.GetSection("consul"));

            ConsulOptions consulOptions = new ConsulOptions();

            configuration.GetSection("consul").Bind(consulOptions);

            return services.AddSingleton<IConsulClient>(x => new ConsulClient(cfg =>
            {
                cfg.Address = consulOptions.ServiceDiscoveryAddress;
            }));
        }

        public static IApplicationBuilder UseConsul(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var consulOptions = scope.ServiceProvider.GetService<IOptions<ConsulOptions>>();

            if (consulOptions.Value.ServiceDiscoveryAddress == null)
            {
                throw new ArgumentException("Consul address can not be empty.", nameof(consulOptions.Value.ServiceDiscoveryAddress));
            }

            var client = scope.ServiceProvider.GetService<IConsulClient>();

            var registration = new AgentServiceRegistration
            {
                Name = consulOptions.Value.ServiceName,
                ID = consulOptions.Value.ServiceName,
                Address = consulOptions.Value.ServiceAddress.Host,
                Port = consulOptions.Value.ServiceAddress.Port
            };

            client.Agent.ServiceDeregister(registration.ID).Wait();

            client.Agent.ServiceRegister(registration).Wait();

            return app;
        }
    }
}
