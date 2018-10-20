using System;
using HackerNews.Infrastructure.Framework.Globalization;
using HackerNews.ViewModels.HomeScreen;
using HackerNews.Views;
using Newtonsoft.Json;
using ReactiveUI;
using Sextant;
using Sextant.Abstraction;
using Splat;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HackerNews
{
    public partial class NewsApp : Application
    {
        public NewsApp()
        {
            InitializeComponent();
            SetupLocalization();

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var bootstrapper = new Bootstrapper();
            bootstrapper.RegisterServices();
            bootstrapper.RegisterViews();

            MainPage = Initialise();
        }

        private NavigationPage Initialise()
        {
            var bgScheduler = RxApp.TaskpoolScheduler;
            var mScheduler = RxApp.MainThreadScheduler;
            var vLocator = Locator.Current.GetService<IViewLocator>();

            var navigationView = new NavigationPageBase(mScheduler, bgScheduler, vLocator);
            var viewStackService = new ViewStackService(navigationView);

            Locator.CurrentMutable.Register<IViewStackService>(() => viewStackService);
            navigationView.PushPage(new HomeScreenViewModel(), null, true, false).Subscribe();

            return navigationView;
        }

        public void SetupLocalization()
        {
            var localizarionService = Locator.Current.GetService<ILocalizationService>();
            var ci = localizarionService.GetCurrentCultureInfo();
            localizarionService.ChangeThreadCurrentCulture(ci);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
