using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microservices.Basket.Persistence.Configuration
{
    public class CacheSetting
    {
        public string BaseUrl { get; set; }
        public int Port { get; set; }
        public string Connection => $"{BaseUrl}:{Port}";
    }
}
