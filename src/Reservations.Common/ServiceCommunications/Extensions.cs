using Consul;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestEase;
using System;
using System.Net.Http;

namespace Reservations.Common.ServiceCommunications
{
    public static class Extensions
    {
        public static void AddServiceForwarder<T>(this IServiceCollection services, string serviceName) where T : class
        {
            IConfiguration configuration;
            IConsulClient consulClient;

            var serviceProvider = services.BuildServiceProvider();

            configuration = serviceProvider.GetService<IConfiguration>();
            consulClient = serviceProvider.GetService<IConsulClient>();

            var clientName = typeof(T).ToString();

            services.AddHttpClient(clientName, c =>
            {
                c.BaseAddress = new Uri("http://base:0");
            })
                    .AddHttpMessageHandler(c =>
                                                new ExternalServiceMessageHandler(
                                                    c.GetService<IConsulClient>(),
                                                    serviceName));

            services.AddTransient<T>(c => new RestClient(c.GetService<IHttpClientFactory>().CreateClient(clientName)).For<T>());
        }
    }
}
