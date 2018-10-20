using System.Collections.Generic;
using System.Threading.Tasks;
using HackerNews.Core.Rest.DTOs;
using Refit;

namespace HackerNews.Core.RestRepository.Rest
{
    [Headers("Content-Type: application/json")]
    public interface IRestApi
    {
        [Get("/topstories.json")]
        Task<List<int>> GetTopPostsAsync();

        [Get("/newstories.json")]
        Task<List<int>> GetNewPostsAsync();

        [Get("/beststories.json")]
        Task<List<int>> GetBestPostsAsync();

        [Get("/jobstories.json")]
        Task<List<int>> GetJobPostsAsync();

        [Get("/item/{storyId}.json")]
        Task<PostDto> GetPostAsync(int storyId);
    }
}
