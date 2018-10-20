using System;
using HackerNews.Core.Models;
using HackerNews.Core.Rest.DTOs;

namespace HackerNews.Core.Rest.Mappers
{
    public class PostMapper : IMapper<Post, PostDto>
    {
        public Post ToDomainEntity(PostDto source)
        {
            return new Post
            {
                By = source.By,
                Deleted = source.Deleted,
                Descendants = source.Descendants,
                Id = source.Id,
                Kids = source.Kids,
                Score = source.Score,
                Text = source.Text,
                Time = source.Time,
                Title = source.Title,
                Type = source.Type.ToString(),
                Url = source.Url
            };
        }

        public PostDto ToRestEntity(Post source)
        {
            throw new NotImplementedException();
        }
    }
}
