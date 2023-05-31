using MassTransit;
using Microservices.Application.Common;
using Microservices.Integration.Configuration;
using Microservices.Persistence.Repository.EfCore.Migrator;
using Microservices.Persistence.Repository.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SagaStateMachineWorkerService;
using SagaStateMachineWorkerService.Configuration;
using SagaStateMachineWorkerService.Migrator;
using SagaStateMachineWorkerService.Models;
using System.Reflection;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;

        var databaseSetting = new DatabaseSettings();
        configuration.GetSection("DatabaseSettings").Bind(databaseSetting);

        services.Configure<DatabaseSettings>(configuration.GetSection("DatabaseSettings"));

        services.AddSingleton<IDataBaseSettings>(sp =>
        {
            return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
        });

        MassTransitOption massTransitOption = configuration.GetSection("MassTransit")
                                          .Get<MassTransitOption>();
        MassTransitBusOption massTransitBusOption = massTransitOption.SelectedMassTransitBusOption();

        services.AddSingleton(massTransitOption);


        services.AddMassTransit(cfg =>
        {
            cfg.AddSagaStateMachine<OrderStateMachine, OrderStateInstance>().EntityFrameworkRepository(opt =>
            {
                opt.AddDbContext<DbContext, OrderStateDbContext>((provider, builder) =>
                {
                    builder.UseNpgsql(databaseSetting.ConnectionString, m =>
                    {
                        m.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                    });
                });
            });

            cfg.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(configure =>
            {
                configure.Host(massTransitBusOption.HostName,
                                                          massTransitBusOption.VirtualHost,
                                                          hst =>
                                                          {
                                                              hst.Username(massTransitBusOption.UserName);
                                                              hst.Password(massTransitBusOption.Password);
                                                          });
               
                configure.ReceiveEndpoint(RabbitMQSettingsConst.OrderSaga, e =>
                {
                    e.ConfigureSaga<OrderStateInstance>(provider);
                });
            }));
        });

        //services.AddTransient<IEfCoreMigrator, OrderStateDbMigrator>();

        //services.AddPersistenceConfigurations(configuration);


        services.AddHostedService<Worker>();
       
        
        //var serviceProvider = services.BuildServiceProvider();
        //var migrator = serviceProvider.GetRequiredService<IEfCoreMigrator>() as OrderStateDbMigrator;
        //await migrator.MigrateDbContextAsync(serviceProvider);
    })
    .Build();


await host.RunAsync();
