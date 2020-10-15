using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Libs.Hosting.BackgroundQueue
{
    public interface IScopedBackgroundTaskQueue
    {
        void QueueBackgroundWorkItem(Func<CancellationToken, IServiceScope, Task> workItem);

        Task<Func<CancellationToken, IServiceScope, Task>> DequeueAsync(CancellationToken cancellationToken);
    }
}
