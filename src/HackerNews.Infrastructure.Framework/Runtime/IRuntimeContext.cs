using System.Globalization;

namespace HackerNews.Infrastructure.Framework.Runtime
{
    public interface IRuntimeContext
    {
        CultureInfo Culture { get; set; }
    }
}
