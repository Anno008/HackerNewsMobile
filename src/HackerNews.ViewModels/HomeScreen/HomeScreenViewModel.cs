using System;
using System.Reactive.Linq;
using HackerNews.Infrastructure.Framework.Globalization;
using HackerNews.Infrastructure.Framework.Scheduler;
using HackerNews.ViewModels.Posts;
using News.Core.Models;
using ReactiveUI;
using ReactiveUI.Legacy;
using Sextant.Abstraction;

namespace HackerNews.ViewModels.HomeScreen
{
    public class HomeScreenViewModel : ViewModelBase
    {
        public override string Id => Texts.HomeScreen;

        public ReactiveList<HomeScreenCellViewModel> Items { get; set; }

        private HomeScreenCellViewModel _selectedItem;

        public HomeScreenCellViewModel SelectedItem
        {
            get => _selectedItem;
            set => this.RaiseAndSetIfChanged(ref _selectedItem, value);
        }

        public HomeScreenViewModel(
            ISchedulerService schedulerService = null,
            IViewStackService viewStackService = null) : base(schedulerService, viewStackService)
        {
            Items = new ReactiveList<HomeScreenCellViewModel>
            {
                new HomeScreenCellViewModel
                {
                    Name = Texts.Popular,
                    PageToPush = Observable.Defer(() => _viewStackService.PushPage(new PostsViewModel(PostType.PopularPosts)))
                },
                new HomeScreenCellViewModel
                {
                    Name =  Texts.New,
                    PageToPush = Observable.Defer(() => _viewStackService.PushPage(new PostsViewModel(PostType.NewestPosts)))
                },
                new HomeScreenCellViewModel
                {
                    Name = Texts.Jobs,
                    PageToPush = Observable.Defer(() => _viewStackService.PushPage(new PostsViewModel(PostType.JobPosts)))
                }
            };

            this.WhenAnyValue(x => x.SelectedItem)
                .Where(selected => selected != null)
                .ObserveOn(_schedulerService.MainScheduler)
                .Select(selected =>
                {
                    SelectedItem = null;
                    return selected.PageToPush;
                })
                .Switch()
                .Subscribe();
        }
    }
}
