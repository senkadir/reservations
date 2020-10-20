using Consul;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Reservations.Common.ServiceCommunications
{
    public class ExternalServiceMessageHandler : DelegatingHandler
    {
        private readonly IConsulClient _consulClient;
        private readonly string _serviceName;

        public ExternalServiceMessageHandler(IConsulClient consulClient,
                                             string serviceName)
        {
            _consulClient = consulClient;
            _serviceName = serviceName;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            Uri requestUri = FindServiceUri(_serviceName, request.RequestUri);

            request.RequestUri = requestUri;

            return await base.SendAsync(request, cancellationToken);
        }

        private Uri FindServiceUri(string serviceName, Uri requestUri)
        {
            var servicesResult = _consulClient.Agent.Services().Result;

            if (servicesResult.Response == null)
            {
                throw new ArgumentNullException($"Service not found in service discovery. Service name: {serviceName}");
            }

            var service = servicesResult.Response
                                              .Where(s => s.Value.ID == serviceName)
                                              .Select(x => x.Value)
                                              .FirstOrDefault();

            if (service == null)
            {
                throw new ArgumentNullException($"Service not found in service discovery. Service name: {serviceName}");
            }

            UriBuilder uriBuilder = new UriBuilder(requestUri)
            {
                Host = service.Address,
                Port = service.Port
            };

            return uriBuilder.Uri;
        }
    }
}
