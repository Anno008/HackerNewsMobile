using System.Reactive.Concurrency;
using ReactiveUI;
using Xamarin.Forms;

namespace HackerNews.Views
{
    public class NavigationModalPageBase : NavigationPageBase
    {
        public NavigationModalPageBase(IScheduler mainScheduler, IScheduler backgroundScheduler, IViewLocator viewLocator)
            : base(mainScheduler, backgroundScheduler, viewLocator)
        {
            Style = Application.Current.Resources["lightNavigationPageStyle"] as Style;
        }
    }
}
