using HackerNews.Infrastructure.Framework.Globalization;
using HackerNews.UWP.Infrastructure.Framework.Globalization;
using Splat;

namespace HackerNews.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            Locator.CurrentMutable.Register(() => new UwpLocalizationService(), typeof(ILocalizationService));

            LoadApplication(new HackerNewsApp());
        }
    }
}
