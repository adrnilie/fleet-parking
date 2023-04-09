using FleetParking.Business;
using System.Reflection;
using FleetParking.Worker;
using NServiceBus;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddEnvironmentVariables()
    .AddUserSecrets(Assembly.GetExecutingAssembly(), true);

builder.Host
    .UseNServiceBus(context =>
    {
        var endpointConfiguration = new EndpointConfiguration("FleetParking");
        var transport = endpointConfiguration.UseTransport(new LearningTransport());

        return endpointConfiguration;
    });

// Add services to the container.
builder.Services
    .AddFleetParkingCore()
    .AddHostedService<DomainEventsProcessor>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
