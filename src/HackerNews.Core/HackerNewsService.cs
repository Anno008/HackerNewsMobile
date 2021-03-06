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
        private readonly IHackerNewsRestService _hackerNewsRestService;

        private readonly Subject<Post> _post = new Subject<Post>();
        public IObservable<Post> Post => _post.AsObservable();

        private readonly SourceCache<Post, int> _posts = new SourceCache<Post, int>(x => x.Id);
        public IObservableCache<Post, int> Posts => _posts;

        private readonly SourceCache<Post, int> _comments = new SourceCache<Post, int>(x => x.Id);
        public IObservableCache<Post, int> Comments => _comments;

        public HackerNewsService(IHackerNewsRestService hackerNewsRestService = null)
        {
            _hackerNewsRestService = hackerNewsRestService ?? Locator.Current.GetService<IHackerNewsRestService>();
        }

        public IObservable<Unit> GetPosts(int offset, PostType postType)
        {
            return _hackerNewsRestService
                        .GetPosts(offset, postType)
                        .Select(result =>
                        {
                            _posts.AddOrUpdate(result);
                            return Unit.Default;
                        });
        }

        public IObservable<Unit> GetPost(int postId)
        {
            return _hackerNewsRestService
                        .GetPost(postId)
                        .Select(post =>
                        {
                            _post.OnNext(post);
                            return Unit.Default;
                        });
        }

        public IObservable<Unit> GetPostComments(int postId, int offset)
        {
            return _hackerNewsRestService
                        .GetPostComments(postId, offset)
                        .Select(result =>
                        {
                            _comments.AddOrUpdate(result);
                            return Unit.Default;
                        });
        }
    }
}
