using FleetParking.Business.Storage;
using FleetParking.Business.Storage.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace FleetParking.Worker
{
    public sealed class DomainEventsProcessor : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public DomainEventsProcessor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var timer = new PeriodicTimer(TimeSpan.FromSeconds(5));
            while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
            {
                await ProcessOutboxMessages(stoppingToken);
            }
        }

        private async Task ProcessOutboxMessages(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<IFleetParkingDbContext>();
            var messageSession = scope.ServiceProvider.GetRequiredService<IMessageSession>();

            var messages = await context
                .OutboxMessages
                .Where(m => m.ProcessedOnUtc == null)
                .Take(20)
                .ToListAsync(stoppingToken);

            foreach (var message in messages)
            {
                var domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(message.Data, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });

                if (domainEvent is null)
                {
                    continue;
                }

                await messageSession.Publish(domainEvent, stoppingToken);

                message.ProcessedOnUtc = DateTime.UtcNow;
            }

            await context.SaveChangesAsync(stoppingToken);
        }
    }
}