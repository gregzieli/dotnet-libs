using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Libs.Hosting.BackgroundQueue
{
    public class ScopedQueuedHostedService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ScopedQueuedHostedService> _logger;

        public ScopedQueuedHostedService(
            IServiceProvider serviceProvider,
            IScopedBackgroundTaskQueue taskQueue,
            ILogger<ScopedQueuedHostedService> logger)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            TaskQueue = taskQueue;
        }

        public IScopedBackgroundTaskQueue TaskQueue { get; }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Scoped Queued Hosted Service is starting.");

            while (!stoppingToken.IsCancellationRequested)
            {
                var workItem = await TaskQueue.DequeueAsync(stoppingToken);

                try
                {
                    using var scope = _serviceProvider.CreateScope();
                    await workItem(stoppingToken, scope);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error occured executing {nameof(workItem)}");
                }
            }

            _logger.LogInformation("Scoped Queued Hosted Service is stopping.");
        }
    }
}
