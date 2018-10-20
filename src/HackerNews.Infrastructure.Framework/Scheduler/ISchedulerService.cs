using System.Reactive.Concurrency;

namespace HackerNews.Infrastructure.Framework.Scheduler
{
    public interface ISchedulerService
    {
        IScheduler DefaultScheduler { get; }

        IScheduler CurrentThreadScheduler { get; }

        IScheduler ImmediateScheduler { get; }

        IScheduler MainScheduler { get; }

        IScheduler TaskPoolScheduler { get; }
    }
}
