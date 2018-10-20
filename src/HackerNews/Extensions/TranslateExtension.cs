using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using HackerNews.Infrastructure.Framework.Globalization;
using HackerNews.Infrastructure.Framework.Runtime;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HackerNews.Extensions
{
    // You exclude the 'Extension' suffix when using in XAML markup
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension
    {
        public string Text { get; set; }

        readonly CultureInfo Culture;
        const string ResourceId = "HackerNews.Infrastructure.Framework.Globalization.Texts";

        public TranslateExtension()
        {
            Culture = RuntimeContext.Current.Culture;
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (Text == null)
                return string.Empty;

            ResourceManager resmgr = new ResourceManager(ResourceId, typeof(Texts).GetTypeInfo().Assembly);

            var translation = resmgr.GetString(Text, Culture);

            if (translation == null)
            {
#if DEBUG
                throw new ArgumentException(String.Format("Key '{0}' was not found in resources '{1}' for culture '{2}'.", Text, ResourceId, Culture.Name), "Text");
#else
                translation = Text; // in release mode if the value for the provided key doesn't exist return the key instead
#endif
            }
            return translation;
        }
    }
}
