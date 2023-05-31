using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Integration.Configuration
{
    public class MassTransitBusOption
    {
        public MassTransitBrokerTypes BrokerType { get; set; }
        public string BrokerName { get; set; }
        public string HostName { get; set; }
        public string VirtualHost { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public enum MassTransitBrokerTypes
    {
        RabbitMq = 1,
    }
}
