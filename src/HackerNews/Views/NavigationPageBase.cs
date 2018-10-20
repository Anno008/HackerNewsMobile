using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using System.Text;
using ReactiveUI;
using Sextant;
using Xamarin.Forms;

namespace HackerNews.Views
{
    public class NavigationPageBase : NavigationView, IViewFor
    {
        public static readonly BindableProperty FontTitleProperty =
            BindableProperty.Create(nameof(FontTitle), typeof(string), typeof(NavigationPageBase));

        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(nameof(FontSize), typeof(double), typeof(NavigationPageBase), Device.GetNamedSize(NamedSize.Default, typeof(Label)));

        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public string FontTitle
        {
            get { return (string)GetValue(FontTitleProperty); }
            set { SetValue(FontTitleProperty, value); }
        }

        public object ViewModel { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public NavigationPageBase(IScheduler mainScheduler, IScheduler backgroundScheduler, IViewLocator viewLocator) : base(mainScheduler, backgroundScheduler, viewLocator)
        {
            Style = Application.Current.Resources["defaultNavigationPageStyle"] as Style;
        }
    }
}
