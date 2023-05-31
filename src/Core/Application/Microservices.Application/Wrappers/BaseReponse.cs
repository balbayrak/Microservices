using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Microservices.Application.Wrappers
{
    public class BaseReponse
    {

        [JsonIgnore]
        public bool IsSuccess { get; set; }

        public List<string> Errors { get; set; }
    }
}
