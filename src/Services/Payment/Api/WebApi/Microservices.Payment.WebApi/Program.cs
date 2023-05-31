using Microservices.Payment.Application.Configuration;
using Microservices.Payment.Persistence.Configuration;
using Microservices.WebApi.Configuration;
using Microservices.WebApi.Logging;
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


builder.Services.AddPaymentApplicationConfigurations();

builder.Services.AddPersistenceConfigurations(builder.Configuration);




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.UseGeneralExceptionMiddleware();
app.UseCorrelationIdMiddleware();
app.UseTraceIdMiddleware();

app.Run();
