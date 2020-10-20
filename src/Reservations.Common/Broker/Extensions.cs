using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Reservations.Common.Broker
{
    public static class Extensions
    {
        public static IServiceCollection AddBroker(this IServiceCollection services,
                                                   Action<IServiceCollectionBusConfigurator> busConfigurator = null,
                                                   Action<IRabbitMqBusFactoryConfigurator, IBusRegistrationContext> factoryConfigurator = null)
        {
            IConfiguration configuration;
            IHostEnvironment hostEnvironment;

            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
                hostEnvironment = serviceProvider.GetRequiredService<IHostEnvironment>();
            }

            services.Configure<BrokerOptions>(configuration.GetSection("broker"));

            BrokerOptions brokerOptions = new BrokerOptions();

            configuration.GetSection("broker").Bind(brokerOptions);

            services.AddMassTransit(config =>
            {
                busConfigurator?.Invoke(config);

                config.AddBus(context => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host(new Uri(brokerOptions.Uri), cfg =>
                    {
                        cfg.Username(brokerOptions.UserName);
                        cfg.Password(brokerOptions.Password);
                    });

                    factoryConfigurator?.Invoke(cfg, context);
                }));
            });

            services.AddSingleton<IHostedService, BusHostedService>();

            return services;
        }
    }
}
