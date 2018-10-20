using System;
using System.Collections.Generic;
using HackerNews.Core.Models;
using News.Core.Models;

namespace HackerNews.Core.Rest.RestApiServices
{
    public interface IHackerNewsRestService
    {
        IObservable<IEnumerable<Post>> GetPosts(int offset, PostType postType);
        IObservable<Post> GetPost(int storyId);
    }
}
