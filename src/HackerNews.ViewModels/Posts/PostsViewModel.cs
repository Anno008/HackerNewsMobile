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
            string title,
            ISchedulerService schedulerService = null,
            IViewStackService viewStackService = null,
            IHackerNewsService newsService = null)
            : base(schedulerService, viewStackService, title)
        {
            _newsService = newsService ?? Locator.Current.GetService<IHackerNewsService>();

            // Registering the get posts command
            GetPosts = ReactiveCommand
                .CreateFromObservable<int, Unit>(offset =>
                    _newsService.GetPosts(offset, postType),
                    outputScheduler: _schedulerService.TaskPoolScheduler);
            GetPosts.Subscribe();

            // Connecting the dynamic data source cache with the view model's list
            // Changes are displayed on the ui immediately after the service cache gets updated
            _newsService
                .Posts
                .Connect()
                .Transform(x => new PostCellViewModel(x))
                .ObserveOn(_schedulerService.MainScheduler)
                .Bind(out _posts)
                .DisposeMany()
                .Subscribe();

            // Handling the flag for activity indicator when the command is executing
            GetPosts
                .IsExecuting
                .SubscribeOn(_schedulerService.TaskPoolScheduler)
                .ToProperty(this, x => x.IsLoading, out _isLoading, true, scheduler: _schedulerService.MainScheduler);

            // When any of the Reactive Commands throw an error handle it here
            GetPosts
                .ThrownExceptions
                .ObserveOn(_schedulerService.MainScheduler)
                .Subscribe(e => this.ShowGenericError());
        }
    }
}
