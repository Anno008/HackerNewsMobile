using System;
using System.Net.Http;
using Refit;

namespace HackerNews.Core.RestRepository.Rest
{
    public class ApiService : IApiService
    {
        public string ApiBaseAddress
        {
            get;
            set;
        }

        public IRestApi GetHttpClient()
        {
            ApiBaseAddress = "https://hacker-news.firebaseio.com/v0/";

            var client = new HttpClient
            {
                BaseAddress = new Uri(ApiBaseAddress)
            };

            return RestService.For<IRestApi>(client);
        }
    }
}
