using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sapp.Core.Persistence;

namespace Sapp.Core.Workers
{
    public class JanitorWorker : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<JanitorWorker> _logger;

        public JanitorWorker(IServiceScopeFactory scopeFactory, ILogger<JanitorWorker> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                await DeleteInactiveRooms(stoppingToken);
            }
        }

        private async Task DeleteInactiveRooms(CancellationToken token = default)
        {
            _logger.LogInformation("Janitor has started.");

            using var scope = _scopeFactory.CreateScope();
            await using var apiContext = scope.ServiceProvider.GetRequiredService<ApiContext>();
            var moment = DateTimeOffset.UtcNow;

            var oldRooms = await apiContext
                .Rooms
                .Where(r => moment - r.UpdatedAt > TimeSpan.FromDays(1))
                .ToListAsync(token);

            apiContext.Rooms.RemoveRange(oldRooms);

            await apiContext.SaveChangesAsync(token);

            _logger.LogInformation("Janitor removed {0} rooms.", oldRooms.Count);
        }
    }
}