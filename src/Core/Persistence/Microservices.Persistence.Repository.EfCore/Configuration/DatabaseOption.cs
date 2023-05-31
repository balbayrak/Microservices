namespace Microservices.Persistence.Repository.EfCore.Configuration
{
    public class DatabaseOption
    {
        public DatabaseType DatabaseType { get; set; }
        public string ConnectionString { get; set; }
    }
}
