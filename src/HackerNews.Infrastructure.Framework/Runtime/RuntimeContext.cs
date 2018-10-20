using System.Globalization;

namespace HackerNews.Infrastructure.Framework.Runtime
{
    public enum RuntimeEnvironment
    {
        Production,
        Development,
        RecordMode,
        Mocked,
        ContinousIntegration,
        Test,
        Review
    }

    public static class RuntimeContext
    {
        static RuntimeContext()
        {
            Current = new MemoryRuntimeContext()
            {
                Culture = new CultureInfo("en"),
            };
        }

        public static IRuntimeContext Current { get; set; }

        public static RuntimeEnvironment Environment { get; set; }
    }
}
