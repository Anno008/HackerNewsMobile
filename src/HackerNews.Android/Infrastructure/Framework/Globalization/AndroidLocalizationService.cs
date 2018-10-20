using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using HackerNews.Infrastructure.Framework.Globalization;
using HackerNews.Infrastructure.Framework.Runtime;

namespace HackerNews.Droid.Infrastructure.Framework.Globalization
{
    public class AndroidLocalizationService : LocalizationService, ILocalizationService
    {
        public AndroidLocalizationService()
        {
            CultureNames = new Dictionary<string, string>()
            {
                {"en-US", "en-US"},
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
            return Java.Util.Locale.Default.ToString().Replace("_", "-");
        }
    }
}