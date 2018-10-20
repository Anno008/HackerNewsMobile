using System;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Acr.UserDialogs;
using HackerNews.Infrastructure.Framework.Globalization;
using HackerNews.ViewModels;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.Forms;

namespace HackerNews.Views
{
    public class ContentPageBase<TViewModel> : ReactiveContentPage<TViewModel> where TViewModel : ViewModelBase
    {
        private Style originalNavigationStyle;

        public static readonly BindableProperty NavigationPageStyleProperty =
            BindableProperty.Create(nameof(NavigationPageStyle), typeof(Style), typeof(ContentPageBase<TViewModel>),
                propertyChanged: OnNavigationStylePropertyChanged);

        public Style NavigationPageStyle
        {
            get => (Style)GetValue(NavigationPageStyleProperty);
            set => SetValue(NavigationPageStyleProperty, value);
        }

        public ContentPageBase()
        {
            this.WhenActivated(disposables =>
            {
                this
                    .WhenAnyValue(x => x.ViewModel.IsRunning)
                    .DistinctUntilChanged()
                    .ObserveOn(RxApp.MainThreadScheduler)
                    .Subscribe(isRunning =>
                    {
                        if (isRunning)
                        {
                            UserDialogs.Instance.ShowLoading(Texts.Loading, MaskType.Gradient);
                        }
                        else
                        {
                            UserDialogs.Instance.HideLoading();
                        }
                    })
                    .DisposeWith(disposables);

                this
                    .ViewModel
                    .ShowSuccess
                    .RegisterHandler(
                        interaction =>
                        {
                            UserDialogs.Instance.HideLoading();
                            var toastConfig = new ToastConfig(interaction.Input);
                            toastConfig.SetDuration(3000);
                            var greenColor = (Color)Application.Current.Resources["greenToastColor"];
                            var whiteColor = (Color)Application.Current.Resources["whiteColor"];
                            toastConfig.SetBackgroundColor(greenColor);
                            toastConfig.SetMessageTextColor(whiteColor);
                            toastConfig.Position = ToastPosition.Top;
                            UserDialogs.Instance.Toast(toastConfig);
                            interaction.SetOutput(new Unit());
                        })
                    .DisposeWith(disposables);

                this
                    .ViewModel
                    .ShowError
                    .RegisterHandler(
                        interaction =>
                        {
                            UserDialogs.Instance.HideLoading();
                            var toastConfig = new ToastConfig(interaction.Input);
                            toastConfig.SetDuration(3000);
                            var redColor = (Color)Application.Current.Resources["redColor"];
                            var whiteColor = (Color)Application.Current.Resources["whiteColor"];
                            toastConfig.SetBackgroundColor(redColor);
                            toastConfig.SetMessageTextColor(whiteColor);
                            toastConfig.Position = ToastPosition.Top;
                            UserDialogs.Instance.Toast(toastConfig);
                            interaction.SetOutput(new Unit());
                        })
                    .DisposeWith(disposables);

                this
                    .ViewModel
                    .ShowAlert
                    .RegisterHandler(
                        async interaction =>
                        {
                            UserDialogs.Instance.HideLoading();
                            await UserDialogs
                                .Instance
                                .AlertAsync(interaction.Input);
                            interaction.SetOutput(new Unit());
                        })
                    .DisposeWith(disposables);

                this
                    .ViewModel
                    .ShowInfo
                    .RegisterHandler(
                        async interaction =>
                        {
                            UserDialogs.Instance.HideLoading();
                            await DisplayAlert(
                                interaction.Input.title,
                                interaction.Input.message,
                                Texts.Ok);

                            interaction.SetOutput(new Unit());
                        })
                    .DisposeWith(disposables);

                this
                    .ViewModel
                    .Confirm
                    .RegisterHandler(
                        async interaction =>
                        {
                            UserDialogs.Instance.HideLoading();
                            var isOk = await DisplayAlert(
                                Texts.Confirmation,
                                interaction.Input,
                                Texts.Ok,
                                Texts.Cancel);

                            interaction.SetOutput(isOk);
                        })
                    .DisposeWith(disposables);

                this
                    .ViewModel
                    .Options
                    .RegisterHandler(
                        async interaction =>
                        {
                            UserDialogs.Instance.HideLoading();
                            var options = interaction.Input?.ToArray();
                            if (options.Any())
                            {
                                var output = await UserDialogs
                                    .Instance
                                    .ActionSheetAsync(
                                        null,
                                        Texts.Cancel,
                                        null,
                                        null,
                                        options);
                                interaction.SetOutput(output);
                            }
                            else
                            {
                                await UserDialogs
                                    .Instance
                                    .AlertAsync(Texts.NoOptionsToShow);
                                interaction.SetOutput(null);
                            }
                        })
                    .DisposeWith(disposables);
            });
        }

        private static void OnNavigationStylePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (Application.Current.MainPage is NavigationPageBase currentNavigation)
            {
                (bindable as ContentPageBase<TViewModel>).originalNavigationStyle = currentNavigation.Style;
                currentNavigation.Style = newValue as Style;
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            if (originalNavigationStyle != null && Application.Current.MainPage is NavigationPageBase currentNavigation)
            {
                currentNavigation.Style = originalNavigationStyle;
                originalNavigationStyle = null;
            }
        }
    }
}