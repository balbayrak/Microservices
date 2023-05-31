using Microservices.Application.Common;

namespace Microservices.Persistence.Repository.Settings

{
    public class DatabaseSettings : IDataBaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
