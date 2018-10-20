using System;
using System.Reactive;
using HackerNews.Core.Models;
using DynamicData;
using News.Core.Models;

namespace HackerNews.Core
{
    public interface IHackerNewsService
    {
        IObservable<Post> Post { get; }
        IObservableCache<Post, int> Posts { get; }
        IObservable<Unit> GetPosts(int offset, PostType postType);
        IObservable<Unit> GetPost(int postId);
    }
}
