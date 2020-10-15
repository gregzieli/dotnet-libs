using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Libs.Hosting.BackgroundQueue
{
    public class ScopedBackgroundTaskQueue : IScopedBackgroundTaskQueue
    {
        private readonly ConcurrentQueue<Func<CancellationToken, IServiceScope, Task>> _workItems =
            new ConcurrentQueue<Func<CancellationToken, IServiceScope, Task>>();

        private readonly SemaphoreSlim _signal = new SemaphoreSlim(0);

        public void QueueBackgroundWorkItem(Func<CancellationToken, IServiceScope, Task> workItem)
        {
            if (workItem == null)
            {
                throw new ArgumentNullException(nameof(workItem));
            }

            _workItems.Enqueue(workItem);
            _signal.Release();
        }

        public async Task<Func<CancellationToken, IServiceScope, Task>> DequeueAsync(CancellationToken cancellationToken)
        {
            await _signal.WaitAsync(cancellationToken);
            _workItems.TryDequeue(out var workItem);

            return workItem;
        }
    }
}
