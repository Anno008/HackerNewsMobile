using System.Globalization;

namespace HackerNews.Infrastructure.Framework.Runtime
{
    public class MemoryRuntimeContext : IRuntimeContext
    {
        public MemoryRuntimeContext()
        {
            Culture = new CultureInfo("en");
        }

        public CultureInfo Culture { get; set; }
    }
}
