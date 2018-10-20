using System;

namespace HackerNews.Infrastructure.Framework.Globalization
{
    public class PlatformCulture
    {
        public string CultureString { get; }

        public string LanguageCode { get; }

        public string LocaleCode { get; }

        public PlatformCulture(string platformCultureString)
        {
            if (string.IsNullOrEmpty(platformCultureString))
            {
                throw new ArgumentException("Expected culture identifier", nameof(platformCultureString));
            }

            CultureString = platformCultureString.Replace("_", "-"); // .NET expects dash, not underscore
            var dashIndex = CultureString.IndexOf("-", StringComparison.Ordinal);
            if (dashIndex > 0)
            {
                var parts = CultureString.Split('-');
                LanguageCode = parts[0];
                LocaleCode = parts[1];
            }
            else
            {
                LanguageCode = CultureString;
                LocaleCode = "";
            }
        }

        public override string ToString()
        {
            return CultureString;
        }
    }
}