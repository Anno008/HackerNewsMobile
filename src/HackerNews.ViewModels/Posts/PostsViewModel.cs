using System.Reactive;
using System.Reactive.Linq;
using HackerNews.Infrastructure.Framework.Scheduler;
using ReactiveUI;
using Sextant.Abstraction;
using Splat;
using System;
using System.Collections.ObjectModel;
using DynamicData;
using HackerNews.Core;
using News.Core.Models;

namespace HackerNews.ViewModels.Posts
{
    public class PostsViewModel : ViewModelBase
    {
        private readonly IHackerNewsService _newsService;

        private ReadOnlyObservableCollection<PostCellViewModel> _posts;
        public ReadOnlyObservableCollection<PostCellViewModel> Posts => _posts;

        ObservableAsPropertyHelper<bool> _isLoading;
        public bool IsLoading => _isLoading.Value;

        public ReactiveCommand<int, Unit> GetPosts { get; protected set; }

        public PostsViewModel(
            PostType postType,
            ISchedulerService schedulerService = null,
            IViewStackService viewStackService = null,
            IHackerNewsService newsService = null)
            : base(schedulerService, viewStackService)
        {
            _newsService = newsService ?? Locator.Current.GetService<IHackerNewsService>();

            GetPosts = ReactiveCommand
                .CreateFromObservable<int, Unit>(offset =>
                    _newsService.GetPosts(offset, postType),
                    outputScheduler: _schedulerService.TaskPoolScheduler);
            GetPosts.Subscribe();

            _newsService
                .Posts
                .Connect()
                .Transform(x => new PostCellViewModel(x))
                .ObserveOn(_schedulerService.MainScheduler)
                .Bind(out _posts)
                .DisposeMany()
                .Subscribe();

            GetPosts
                .IsExecuting
                .SubscribeOn(_schedulerService.TaskPoolScheduler)
                .ToProperty(this, x => x.IsLoading, out _isLoading, true, scheduler: _schedulerService.MainScheduler);

            GetPosts
                .ThrownExceptions
                .Subscribe(e => this.ShowGenericError());
        }
    }
}
