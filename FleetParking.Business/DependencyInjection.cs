using FleetParking.Business.Services;
using FleetParking.Business.Storage;
using Microsoft.Extensions.DependencyInjection;

namespace FleetParking.Business;

public static class DependencyInjection
{
    public static IServiceCollection AddFleetParkingCore(this IServiceCollection services)
    {
        services
            .AddDbContext<FleetParkingDbContext>();

        return services
            .AddScoped<IFleetParkingDbContext>(sp => sp.GetRequiredService<FleetParkingDbContext>())
            .AddScoped<IFleetParkingService, FleetParkingService>();
    }
}