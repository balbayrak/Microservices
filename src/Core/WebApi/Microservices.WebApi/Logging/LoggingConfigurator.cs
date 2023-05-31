using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;

namespace Microservices.WebApi.Logging
{
    public class LoggingConfigurator
    {
        private const string ELASTIC_CONFIGURATION_NAME = "ElasticConfiguration";
        
        public static ILogger ConfigureLogging()
        {
            return GetLoggerConfiguration()
                .CreateLogger();
        }

        public static LoggerConfiguration GetLoggerConfiguration()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .Build();

            return new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                //.WriteTo.Debug()
                //.WriteTo.Console()
                .WriteTo.Elasticsearch(ConfigureElasticSearch(configuration, environment))
                .Enrich.WithProperty("Environment", environment)
                .ReadFrom.Configuration(configuration);

        }

        private static ElasticsearchSinkOptions ConfigureElasticSearch(IConfigurationRoot configuration, string environment)
        {
            var elasticConf = configuration.GetSection(ELASTIC_CONFIGURATION_NAME);
            if (elasticConf is null)
            {
                throw new Exception($"{ELASTIC_CONFIGURATION_NAME} section not found in appsettings");
            }
            return new ElasticsearchSinkOptions(new Uri(elasticConf["Uri"]))
            {
                AutoRegisterTemplate = true,
                IndexFormat = elasticConf["IndexName"] ??
                $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy:MM}"

                //ModifyConnectionSettings = p =>
                //{
                //    p.BasicAuthentication(elasticConf["Username"], elasticConf["Password"]);
                //    p.ServerCertificateValidationCallback(CertificateValidations.AllowAll);
                //    return p;
                //}
            };
        }
    }
}
