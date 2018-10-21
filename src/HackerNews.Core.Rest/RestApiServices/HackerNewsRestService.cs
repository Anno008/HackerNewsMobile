using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading.Tasks;
using HackerNews.Core.Models;
using HackerNews.Core.Rest.Mappers;
using HackerNews.Core.RestRepository.Rest;
using HackerNews.Infrastructure.Framework.Device;
using News.Core.Models;
using Splat;

namespace HackerNews.Core.Rest.RestApiServices
{
    public class HackerNewsRestService : IHackerNewsRestService
    {
        private readonly IApiService _apiService;
        private const int PageSize = 25;

        public HackerNewsRestService(
            IApiService apiService = null,
            IConnectivity connectivity = null)
        {
            _apiService = apiService ?? Locator.Current.GetService<IApiService>();
        }

        public IObservable<IEnumerable<Post>> GetPosts(int offset, PostType postType) =>
                GetPostsAsync(offset, postType).ToObservable();

        public IObservable<Post> GetPost(int storyId)
        {
            var mapper = new PostMapper();

            return _apiService
                        .GetHttpClient()
                        .GetPostAsync(storyId)
                        .ToObservable()
                        .Select(x => mapper.ToDomainEntity(x));
        }

        public IObservable<IEnumerable<Post>> GetPostComments(int postId, int offset)
        {
            return GetPost(postId).SelectMany(p => GetCommentsAsync(p.Kids, offset).ToObservable());
        }

        private async Task<IEnumerable<Post>> GetCommentsAsync(List<int> commentIds, int offset)
        {
            var mapper = new PostMapper();

            var ids = commentIds ?? new List<int>();

            var tasks = ids.Skip(offset)
                            .Take(PageSize)
                            .Select(id => _apiService.GetHttpClient().GetPostAsync(id));

            var acquiredPosts = await Task.WhenAll(tasks);
            return new List<Post>(acquiredPosts.Select(x => mapper.ToDomainEntity(x)));
        }

        private async Task<IEnumerable<Post>> GetPostsAsync(int offset, PostType type)
        {
            var mapper = new PostMapper();
            var postIds = new List<int>();

            switch (type)
            {
                case PostType.PopularPosts:
                    postIds = await _apiService.GetHttpClient().GetTopPostsAsync();
                    break;
                case PostType.TopPosts:
                    postIds = await _apiService.GetHttpClient().GetTopPostsAsync();
                    break;
                case PostType.JobPosts:
                    postIds = await _apiService.GetHttpClient().GetJobPostsAsync();
                    break;
                case PostType.NewestPosts:
                    postIds = await _apiService.GetHttpClient().GetNewPostsAsync();
                    break;
                default:
                    throw new NotImplementedException($"Not implemented support for {type} requests");
            }

            var tasks = postIds.Skip(offset)
                                .Take(PageSize)
                                .Select(postId => _apiService.GetHttpClient().GetPostAsync(postId));

            var acquiredPosts = await Task.WhenAll(tasks);
            return new List<Post>(acquiredPosts.Select(x => mapper.ToDomainEntity(x)));
        }
    }
}
