using HealthChecks.UI.Client;
using Microservices.Basket.Application.Configuration;
using Microservices.Basket.Persistence.Configuration;
using Microservices.WebApi.Configuration;
using Microservices.WebApi.Logging;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(LoggingConfigurator.ConfigureLogging());
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistenceConfigurations(builder.Configuration);
builder.Services.AddApplicationConfigurations();

var cacheSettingOption = builder.Configuration.GetSection("CacheSettings").Get<CacheSetting>();

builder.Services.AddHealthChecks()
    .AddRedis(
    redisConnectionString: cacheSettingOption.Connection,
    name: "Redis Check",
    failureStatus: HealthStatus.Unhealthy | HealthStatus.Degraded,
    tags: new string[] { "redis" });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.UseGeneralExceptionMiddleware();
app.UseCorrelationIdMiddleware();


app.Run();
