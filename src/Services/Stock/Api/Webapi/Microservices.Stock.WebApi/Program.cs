
using HealthChecks.UI.Client;
using Microservices.DistributedLock.Configuration;
using Microservices.Persistence.Repository.Settings;
using Microservices.Stock.Application.Configuration;
using Microservices.Stock.Persistence.Configuration;
using Microservices.WebApi.Configuration;
using Microservices.WebApi.Logging;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());

var logger = LoggingConfigurator.ConfigureLogging();
var loggerFactory = new LoggerFactory().AddSerilog(logger);

builder.Host.UseSerilog(logger);



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowedOriginPolicy",
                      builder =>
                      {
                          builder.AllowAnyHeader()
                                 .AllowAnyMethod()
                                 .AllowAnyOrigin();
                      });
});


builder.Services.AddApplicationConfigurations();

builder.Services.AddPersistenceConfigurations(builder.Configuration);

builder.Services.AddDistributionLockConfigurations(builder.Configuration, loggerFactory);


var databaseSettingOption = builder.Configuration.GetSection("DatabaseSettings").Get<DatabaseSettings>();

builder.Services.AddHealthChecks()

    //.AddEventStore(
    //eventStoreConnection: builder.Configuration["ConnectionStrings:EventStore"],
    //name: "EventStore Check",
    //failureStatus: HealthStatus.Unhealthy | HealthStatus.Degraded,
    //tags: new string[] { "eventStore" })

    .AddNpgSql(
    npgsqlConnectionString: databaseSettingOption.ConnectionString,
    name: "Stock Db Check",
    failureStatus: HealthStatus.Unhealthy | HealthStatus.Degraded,
    tags: new string[] { "stockdb-postgresql" });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

#region Middlewares

app.UseGeneralExceptionMiddleware();
app.UseCorrelationIdMiddleware();
app.UseTraceIdMiddleware();

#endregion

app.UseCors("AllowedOriginPolicy");

#region Db Migrator

await app.RunMigratorAsync();

#endregion


app.Lifetime.DisposeLockFactory();

app.MapControllers();


app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
