using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Foundation;
using HackerNews.Infrastructure.Framework.Globalization;
using HackerNews.Infrastructure.Framework.Runtime;

namespace HackerNews.iOS.Infrastructure.Framework.Globalization
{
    public class IosLocalizationService : LocalizationService, ILocalizationService
    {
        public IosLocalizationService()
        {
            CultureNames = new Dictionary<string, string>()
            {
                {"en-US", "en-US"},
                {"en", "en-US"},
            };
        }

        public void ChangeThreadCurrentCulture(CultureInfo ci)
        {
            RuntimeContext.Current.Culture = ci;
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
            CultureInfo.DefaultThreadCurrentUICulture = ci;
            CultureInfo.DefaultThreadCurrentCulture = ci;
            RuntimeContext.Current.Culture = ci;
            Texts.Culture = ci;
        }

        protected override string GetDeviceCurrentCultureInfo()
        {
            string deviceLanguageName = string.Empty;

            if (NSLocale.PreferredLanguages.Length > 0)
            {
                deviceLanguageName = NSLocale.PreferredLanguages[0];
            }

            return deviceLanguageName;
        }
    }
}