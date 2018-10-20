using System;
using System.Collections.Generic;
using System.Globalization;

namespace HackerNews.Infrastructure.Framework.Globalization
{
    public class LocalizationService
    {
        protected readonly CultureInfo DefaultCultureInfo = new CultureInfo("en-US");
        protected readonly CultureInfo UnsupportedCultureInfo = new CultureInfo("af-ZA");

        protected Dictionary<string, string> CultureNames
        {
            get;
            set;
        }

        protected virtual bool IsDebug
        {
            get
            {
#if DEBUG
                return true;
#else
                return false;
#endif
            }
        }

        public LocalizationService(Dictionary<string, string> cultureNames = null)
        {
            CultureNames = cultureNames ?? new Dictionary<string, string>();
        }

        protected virtual string GetDeviceCurrentCultureInfo()
        {
            throw new NotImplementedException();
        }

        public CultureInfo GetCurrentCultureInfo()
        {
            CultureInfo result = null;

            var deviceLanguageName = GetDeviceCurrentCultureInfo();

            if (CultureNames.ContainsKey(deviceLanguageName))
            {
                result = new CultureInfo(CultureNames[deviceLanguageName]);
            }
            else
            {
                if (IsDebug)
                {
                    result = UnsupportedCultureInfo;
                }
                else
                {
                    result = DefaultCultureInfo;
                }
            }

            return result;
        }
    }
}