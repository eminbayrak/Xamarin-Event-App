using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using FFImageLoading.Forms.Droid;
using FFImageLoading.Svg.Forms;
using Microsoft.WindowsAzure.MobileServices;
using Plugin.LocalNotifications;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace ParPorApp.Droid
{
    [Activity(Label = "SportiveParent", Icon = "@drawable/ic_launcher", Theme = "@style/MainTheme", MainLauncher = false,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            UserDialogs.Init(this);
            base.OnCreate(bundle);
            this.Window.AddFlags(WindowManagerFlags.TranslucentStatus);
            Forms.Init(this, bundle);
            LocalNotificationsImplementation.NotificationIconId = Resource.Drawable.ic_launcher;
            CachedImageRenderer.Init(true);
            
            var ignore = typeof(SvgCachedImage);
            //Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
            Forms.Init(this, bundle);
            CurrentPlatform.Init();
            LoadApplication(new App());
        }
    }
}