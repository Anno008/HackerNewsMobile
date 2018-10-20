using System.Collections.Generic;
using System.Globalization;
using HackerNews.Infrastructure.Framework.Globalization;
using HackerNews.Infrastructure.Framework.Runtime;

namespace HackerNews.UWP.Infrastructure.Framework.Globalization
{
    public class UwpLocalizationService : LocalizationService, ILocalizationService
    {
        public UwpLocalizationService()
        {
            CultureNames = new Dictionary<string, string>()
            {
                {"en-US", "en-US"},
            };
        }

        readonly new CultureInfo DefaultCultureInfo = new CultureInfo("en-US");

        public void ChangeThreadCurrentCulture(CultureInfo ci)
        {
            RuntimeContext.Current.Culture = ci;
            CultureInfo.DefaultThreadCurrentUICulture = ci;
            CultureInfo.DefaultThreadCurrentCulture = ci;
            RuntimeContext.Current.Culture = ci;
            Texts.Culture = ci;
        }

        protected override string GetDeviceCurrentCultureInfo() => Windows.System.UserProfile.GlobalizationPreferences.Languages[0];
    }
}
