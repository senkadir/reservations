using System;

namespace Reservations.Common
{
    public class ConsulOptions
    {
        public Uri ServiceDiscoveryAddress { get; set; }

        public string ServiceName { get; set; }

        public Uri ServiceAddress { get; set; }
    }
}
