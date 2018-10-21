﻿using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using DynamicData;
using HackerNews.Core;
using HackerNews.Core.Models;
using HackerNews.Infrastructure.Framework.Scheduler;
using ReactiveUI;
using Sextant.Abstraction;
using Splat;

namespace HackerNews.ViewModels.Posts
{
    public class PostDetailsViewModel : ViewModelBase
    {
        private readonly IHackerNewsService _hackerNewsService;

        private ReadOnlyObservableCollection<PostCellViewModel> _comments;
        public ReadOnlyObservableCollection<PostCellViewModel> Comments => _comments;

        ObservableAsPropertyHelper<bool> _commentsAreLoading;
        public bool CommentsAreLoading => _commentsAreLoading.Value;

        private Post _post;
        public Post Post
        {
            get => _post;
            set => this.RaiseAndSetIfChanged(ref _post, value);
        }

        public ReactiveCommand<int, Unit> GetComments { get; protected set; }
        public ReactiveCommand<int, Unit> GetPost { get; protected set; }

        public PostDetailsViewModel(
            int postId,
            ISchedulerService schedulerService = null,
            IViewStackService viewStackService = null,
            IHackerNewsService hackerNewsService = null)
            : base(schedulerService, viewStackService)
        {
            _hackerNewsService = hackerNewsService ?? Locator.Current.GetService<IHackerNewsService>();

            // Command for acquiring the post details
            GetPost = ReactiveCommand
                .CreateFromObservable<int, Unit>(id =>
                    _hackerNewsService.GetPost(id),
                    outputScheduler: _schedulerService.TaskPoolScheduler);
            GetPost.Subscribe();

            // Command for acquiring the post comments
            GetComments = ReactiveCommand
                .CreateFromObservable<int, Unit>(offset =>
                    _hackerNewsService.GetPostComments(postId, offset),
                    outputScheduler: _schedulerService.TaskPoolScheduler);
            GetComments.Subscribe();

            // Handle the acr user dialog spinner while loading the details
            GetPost
                .IsExecuting
                .SubscribeOn(_schedulerService.TaskPoolScheduler)
                .ObserveOn(_schedulerService.MainScheduler)
                .Subscribe(isExecuting => IsRunning = isExecuting);

            this.WhenAnyValue(vm => vm.Post)
                .Where(post => post != null)
                .Select(_ => 0)
                .SubscribeOn(_schedulerService.TaskPoolScheduler)
                .ObserveOn(_schedulerService.TaskPoolScheduler)
                .InvokeCommand(GetComments);

            // Once the post is acquired assign it to the VM's property
            _hackerNewsService
                .Post
                .Where(p => p != null)
                .SubscribeOn(_schedulerService.TaskPoolScheduler)
                .ObserveOn(_schedulerService.MainScheduler)
                .Subscribe(p => Post = p);

            // Bind the comments related to the post to the _comments property
            _hackerNewsService
               .Posts
               .Connect()
               .Transform(x => new PostCellViewModel(x))
               .ObserveOn(_schedulerService.MainScheduler)
               .Bind(out _comments)
               .DisposeMany()
               .Subscribe();

            // Handling the flag for activity indicator when the command is executing
            GetComments
                .IsExecuting
                .SubscribeOn(_schedulerService.TaskPoolScheduler)
                .ToProperty(this, x => x.CommentsAreLoading, out _commentsAreLoading, true, scheduler: _schedulerService.MainScheduler);

            // When any of the Reactive Commands throw an error handle it here
            Observable
                .Merge(
                    GetComments.ThrownExceptions,
                    GetPost.ThrownExceptions)
                .ObserveOn(_schedulerService.MainScheduler)
                .SelectMany(ex => ShowGenericError(null, ex))
                .Subscribe();

            Observable.Return(postId).InvokeCommand(GetPost);
        }
    }
}