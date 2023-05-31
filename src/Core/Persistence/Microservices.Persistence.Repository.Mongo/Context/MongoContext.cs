using Microservices.Application.Common;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Microservices.Persistence.Repository.Mongo.Context
{
    public class MongoContext : IMongoContext
    {
        private IMongoDatabase Database { get; set; }
        public IClientSessionHandle Session { get; set; }
        public MongoClient MongoClient { get; set; }
        private readonly List<Func<Task>> _commands;
        private readonly IConfiguration _configuration;
        private IDataBaseSettings _databaseSettings;
        public MongoContext(IConfiguration configuration, IDataBaseSettings databaseSettings)
        {
            _configuration = configuration;
            _commands = new List<Func<Task>>();
            _databaseSettings = databaseSettings;
        }

        public async Task<int> SaveChanges()
        {
            ConfigureMongo();

        //    using (Session = await MongoClient.StartSessionAsync())
        //    {
       //         Session.StartTransaction();

                var commandTasks = _commands.Select(c => c());

                await Task.WhenAll(commandTasks);

           //     await Session.CommitTransactionAsync();
           // }

            return _commands.Count;
        }

        private void ConfigureMongo()
        {
            if (MongoClient != null)
            {
                return;
            }
            // Configure mongo (You can inject the config, just to simplify)
            MongoClient = new MongoClient(_databaseSettings.ConnectionString);

            Database = MongoClient.GetDatabase(_databaseSettings.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            ConfigureMongo();

            return Database.GetCollection<T>(name);
        }

        public void Dispose()
        {
            Session?.Dispose();
            GC.SuppressFinalize(this);
        }

        public Task AddCommand(Func<Task> func)
        {
            _commands.Add(func);
            return Task.CompletedTask;
        }
    }
}
