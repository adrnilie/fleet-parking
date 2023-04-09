using FleetParking.Business.Services;
using FleetParking.Business.Storage;
using FleetParking.Business.Storage.Intercceptors;
using Microsoft.Extensions.DependencyInjection;

namespace FleetParking.Business;

public static class DependencyInjection
{
    public static IServiceCollection AddFleetParkingCore(this IServiceCollection services)
    { 
        services
            .AddSingleton<ConvertDomainEventsToOutboxMessagesInterceptor>()
            .AddScoped<IFleetParkingDbContext>(sp => sp.GetRequiredService<FleetParkingDbContext>())
            .AddScoped<IFleetParkingService, FleetParkingService>()
            .AddDbContext<FleetParkingDbContext>();

        return services;
    }
}