﻿using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using DynamicData;
using Splat;
using HackerNews.Core.Models;
using HackerNews.Core.Rest.RestApiServices;
using News.Core.Models;

namespace HackerNews.Core
{
    public class HackerNewsService : IHackerNewsService
    {
        private readonly IHackerNewsRestService _hackerNewsRepository;

        private readonly Subject<Post> _post = new Subject<Post>();
        public IObservable<Post> Post => _post.AsObservable();

        private readonly SourceCache<Post, int> _posts = new SourceCache<Post, int>(x => x.Id);
        public IObservableCache<Post, int> Posts => _posts;

        public HackerNewsService(IHackerNewsRestService hackerNewsRepository = null)
        {
            _hackerNewsRepository = hackerNewsRepository ?? Locator.Current.GetService<IHackerNewsRestService>();
            _posts = new SourceCache<Post, int>(x => x.Id);
        }

        public IObservable<Unit> GetPosts(int offset, PostType postType)
        {
            return _hackerNewsRepository
                        .GetPosts(offset, postType)
                        .Select(result =>
                        {
                            _posts.AddOrUpdate(result);
                            return Unit.Default;
                        });
        }

        public IObservable<Unit> GetPost(int postId)
        {
            return _hackerNewsRepository
                        .GetPost(postId)
                        .Select(post =>
                        {
                            _post.OnNext(post);
                            return Unit.Default;
                        });
        }
    }
}