using System.Reactive.Concurrency;
using System.Threading;
using Rx = System.Reactive.Concurrency;

namespace HackerNews.Infrastructure.Framework.Scheduler
{
    public class SchedulerService : ISchedulerService
    {
        public SchedulerService() =>
            MainScheduler = new SynchronizationContextScheduler(SynchronizationContext.Current ?? new SynchronizationContext());

        public IScheduler MainScheduler { get; }

        public IScheduler DefaultScheduler => Rx.DefaultScheduler.Instance;

        public IScheduler CurrentThreadScheduler => Rx.CurrentThreadScheduler.Instance;

        public IScheduler ImmediateScheduler => Rx.ImmediateScheduler.Instance;

        public IScheduler TaskPoolScheduler => Rx.TaskPoolScheduler.Default;
    }
}
