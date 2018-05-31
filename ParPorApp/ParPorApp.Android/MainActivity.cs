using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.Content.Res;
using Android.Gms.Auth.Api;
using Android.Gms.Auth.Api.SignIn;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using FFImageLoading.Forms.Droid;
using FFImageLoading.Svg.Forms;
using Lottie.Forms.Droid;
using Microsoft.WindowsAzure.MobileServices;
using ParPorApp.Services;
using Plugin.LocalNotifications;
using Xamarin.Facebook;
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

            FacebookSdk.SdkInitialize(this);

            UserDialogs.Init(this);
            base.OnCreate(bundle);
            this.Window.AddFlags(WindowManagerFlags.TranslucentStatus);
            Forms.Init(this, bundle);
            LocalNotificationsImplementation.NotificationIconId = Resource.Drawable.ic_launcher;
            CachedImageRenderer.Init(true);
            AnimationViewRenderer.Init();
            var ignore = typeof(SvgCachedImage);
            //Microsoft.WindowsAzure.MobileServices.CurrentPlatform.Init();
            Forms.Init(this, bundle);
            CurrentPlatform.Init();

            DependencyService.Register<IGoogleManager, GoogleManager>();
            DependencyService.Register<IFacebookManager, FacebookManager>();

            LoadApplication(new App());
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Android.Content.Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == 1)
            {
                var result = Auth.GoogleSignInApi.GetSignInResultFromIntent(data);
                GoogleManager.Instance.OnAuthCompleted(result);
            }

            var manager = DependencyService.Get<IFacebookManager>();
            (manager as FacebookManager)?._callbackManager.OnActivityResult(requestCode, (int)resultCode, data);
        }
    }
}