﻿using System.Globalization;

using Foundation;
using HackerNews.Infrastructure.Framework.Globalization;
using HackerNews.iOS.Infrastructure.Framework.Globalization;
using Splat;
using UIKit;

namespace HackerNews.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false);

            Rg.Plugins.Popup.Popup.Init();
            Locator.CurrentMutable.Register(() => new IosLocalizationService(), typeof(ILocalizationService));

            LoadApplication(new HackerNewsApp());
            return base.FinishedLaunching(app, options);
        }

        [Export("SetLocaleTo:")]
        public void SetLocaleTo(string cultureName)
        {
            var localizarionService = Locator.Current.GetService<ILocalizationService>();
            var ci = new CultureInfo(cultureName);
            localizarionService.ChangeThreadCurrentCulture(ci);
        }
    }
}
