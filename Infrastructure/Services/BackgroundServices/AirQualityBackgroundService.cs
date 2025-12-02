using Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Services.BackgroundServices
{
    public class AirQualityBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly TaskCompletionSource<bool> _taskCompletionSource;

        public AirQualityBackgroundService(IServiceScopeFactory scopeFactory, TaskCompletionSource<bool> taskCompletionSource)
        {
            _scopeFactory = scopeFactory;
            _taskCompletionSource = taskCompletionSource;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _taskCompletionSource.Task;

            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var service = scope.ServiceProvider.GetRequiredService<IAirQualityService>();

                await service.RefreshAirQualityData(); // update values in DB

                await Task.Delay(TimeSpan.FromMinutes(60), stoppingToken); // refresh every 60 min
            }
        }
    }
}
