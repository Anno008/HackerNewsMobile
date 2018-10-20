using System.Globalization;

namespace HackerNews.Infrastructure.Framework.Globalization
{
    public interface ILocalizationService
    {
        CultureInfo GetCurrentCultureInfo();
        void ChangeThreadCurrentCulture(CultureInfo ci);
    }
}
