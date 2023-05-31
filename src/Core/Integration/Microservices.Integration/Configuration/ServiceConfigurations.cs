using MassTransit;
using MassTransit.Transports;
using Microservices.Application.Common;
using Microservices.Application.Tracing;
using Microservices.Integration.Correlation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microservices.Integration.Configuration
{
    public static class ServiceConfigurations
    {
        public static void AddIntegrationConfigurations(this IServiceCollection services, IConfiguration configuration, string massTransitConfigKey,Action<IRegistrationConfigurator> configureConsumers ,Action<IBusControl,IServiceProvider> consumerBindAction)
        {
            services.AddTransient<IIntegrationEventPublisher, IntegrationEventPublisher>();


            MassTransitOption massTransitOption = configuration.GetSection(massTransitConfigKey)
                                                      .Get<MassTransitOption>();
            MassTransitBusOption massTransitBusOption = massTransitOption.SelectedMassTransitBusOption();

            services.AddSingleton(massTransitOption);


            services.AddOptions<MassTransitHostOptions>()
               .Configure(options =>
               {
                    // if specified, waits until the bus is started before
                    // returning from IHostedService.StartAsync
                    // default is false
                    options.WaitUntilStarted = true;

                    // if specified, limits the wait time when starting the bus
                    options.StartTimeout = TimeSpan.FromSeconds(15);

                    // if specified, limits the wait time when stopping the bus
                    options.StopTimeout = TimeSpan.FromSeconds(30);
               });

            services.AddMassTransit(configurator =>
            {
                configurator.SetKebabCaseEndpointNameFormatter();

                configureConsumers?.Invoke(configurator);

                void ConfigureMassTransit(IBusFactoryConfigurator cfg)
                {
                    cfg.UseConcurrencyLimit(massTransitOption.ConcurrencyLimit);
                    cfg.UseRetry(retryConfigurator => retryConfigurator.SetRetryPolicy(filter => filter.Incremental(massTransitOption.RetryLimitCount, TimeSpan.FromSeconds(massTransitOption.InitialIntervalSeconds), TimeSpan.FromSeconds(massTransitOption.IntervalIncrementSeconds))));
                }

                configurator.AddBus(provider =>
                {
                    IHost host = null;
                    IBusControl busControl = massTransitBusOption.BrokerType switch
                    {
                        MassTransitBrokerTypes.RabbitMq
                            => Bus.Factory.CreateUsingRabbitMq(cfg =>
                            {
                                cfg.Host(massTransitBusOption.HostName,
                                                   massTransitBusOption.VirtualHost,
                                                   hst =>
                                                   {
                                                       hst.Username(massTransitBusOption.UserName);
                                                       hst.Password(massTransitBusOption.Password);
                                                   });
                                ConfigureMassTransit(cfg);

                                MessageCorrelation.UseCorrelationId<ICorrelated>(p => p.CorrelationId);
                            }),
                        _ => throw new ArgumentOutOfRangeException()
                    };

                    //  BindConsumer(busControl, provider);
                    consumerBindAction.Invoke(busControl, provider);

                    foreach (IConsumeObserver observer in provider.GetServices<IConsumeObserver>())
                    {
                        host.ConnectConsumeObserver(observer);
                    }

                    foreach (ISendObserver observer in provider.GetServices<ISendObserver>())
                    {
                        host.ConnectSendObserver(observer);
                    }

                    foreach (IPublishObserver observer in provider.GetServices<IPublishObserver>())
                    {
                        host.ConnectPublishObserver(observer);
                    }

                    return busControl;
                });
            });

            if (massTransitOption.UseAutomaticCorrelation)
            {
                services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
                services.AddTransient<ICorrelationIdHttpContextAccessor, CorrelationIdHttpContextAccessor>();
                services.AddTransient(provider => new Lazy<ICorrelationIdHttpContextAccessor>(provider.GetService<ICorrelationIdHttpContextAccessor>));
                services.AddTransient<ICorrelationIdConsumeContextAccessor, CorrelationIdConsumeContextAccessor>();
                services.AddTransient(provider => new Lazy<ICorrelationIdConsumeContextAccessor>(provider.GetService<ICorrelationIdConsumeContextAccessor>));
                services.AddTransient<ICorrelationIdProvider, CorrelationIdProvider>();
            }
            else
            {
                services.AddTransient<ICorrelationIdProvider, NullCorrelationIdProvider>();
            }
        }
    }
}
