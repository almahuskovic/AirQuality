using Infrastructure.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Services.BackgroundServices
{
    public class ImportCities : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly TaskCompletionSource<bool> _taskCompletionSource;
        public ImportCities(IServiceScopeFactory scopeFactory, TaskCompletionSource<bool> taskCompletionSource)
        {
            _scopeFactory = scopeFactory;
            _taskCompletionSource = taskCompletionSource;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var service = scope.ServiceProvider.GetRequiredService<ICityService>();

                service.ImportCitiesInDB(); // update values in DB
                if (!_taskCompletionSource.Task.IsCompleted)
                {
                    _taskCompletionSource.SetResult(true); // Signaliziraj samo ako zadatak nije već završen
                }
            }
        }
    }
}
