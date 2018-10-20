using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace HackerNews.Core.Rest.Constants
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PostType
    {
        Story,
        Comment,
        Poll,
        Pollopt,
        Job
    }
}
