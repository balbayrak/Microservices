using MassTransit;
using Microservices.EventSourcing.Configuration;
using Microservices.Integration.Configuration;
using Microservices.Stock.EventStore;
using Microservices.Stock.StockMotionWorker;
using System.Reflection;

IHost host = Host.CreateDefaultBuilder(args)
     .ConfigureServices((hostContext, services) =>
     {
         IConfiguration configuration = hostContext.Configuration;

         services.AddEventStore(configuration, "EventStore");

        services.AddSingleton<IStockMotionEventStore, StockMotionEventStore>();

        services.AddIntegrationConfigurations(configuration, "MassTransit",
                (IRegistrationConfigurator configurator) =>
                {
                   // configurator.AddConsumers(Assembly.GetExecutingAssembly());
                },
                (IBusControl busControl, IServiceProvider provider) =>
                {
                   

                });

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
