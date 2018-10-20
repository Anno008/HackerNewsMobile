
using Android.App;
using Android.Content.PM;
using Android.OS;
using Acr.UserDialogs;
using Splat;
using HackerNews.Infrastructure.Framework.Globalization;
using HackerNews.Droid.Infrastructure.Framework.Globalization;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Android.Runtime;
using Plugin.Permissions;
using System.Globalization;
using Java.Interop;

namespace HackerNews.Droid
{
    [Activity(Label = "News", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            UserDialogs.Init(this);
            Locator.CurrentMutable.Register(() => new AndroidLocalizationService(), typeof(ILocalizationService));

            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            LoadApplication(new NewsApp());
            Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
        }

        [Export("SetLocaleTo")]
        public void SetLocaleTo(string cultureName)
        {
            var localizarionService = Locator.Current.GetService<ILocalizationService>();
            var ci = new CultureInfo(cultureName);
            localizarionService.ChangeThreadCurrentCulture(ci);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}