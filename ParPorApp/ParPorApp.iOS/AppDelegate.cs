using System;
using System.Collections.Generic;
using System.Linq;
using Facebook.CoreKit;
using FFImageLoading.Forms.Touch;
using Foundation;
using Google.SignIn;
using ParPorApp.Services;
using UIKit;
using UserNotifications;
using Xamarin.Forms;

namespace ParPorApp.iOS
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

            //google auth config
            DependencyService.Register<IGoogleManager, GoogleManager>();
            var googleServiceDictionary = NSDictionary.FromFile("auth-google.plist");
            SignIn.SharedInstance.ClientID = googleServiceDictionary["CLIENT_ID"].ToString();

            DependencyService.Register<IFacebookManager, FacebookManager>();

            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                // Ask the user for permission to get notifications on iOS 10.0+
                UNUserNotificationCenter.Current.RequestAuthorization(
                    UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound,
                    (approved, error) => { });
                UNUserNotificationCenter.Current.Delegate = new UserNotificationCenterDelegate();



            }
            else if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
            {
                // Ask the user for permission to get notifications on iOS 8.0+
                var settings = UIUserNotificationSettings.GetSettingsForTypes(
                    UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
                    new NSSet());

                UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            }
            // Set app theme
            UITabBar.Appearance.BarTintColor = UIColor.LightTextColor;
            UITabBar.Appearance.TintColor = UIColor.White;
            UIProgressView.Appearance.TintColor = UIColor.FromRGB(255, 255, 255);
            CachedImageRenderer.Init();
            UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(255, 255, 255);
            UINavigationBar.Appearance.TintColor = UIColor.White;
            LoadApplication(new App());
            return base.FinishedLaunching(app, options);
            
        }

        public override bool OpenUrl(UIApplication application, NSUrl url, string sourceApplication, NSObject annotation)
        {
            //return base.OpenUrl(application, url, sourceApplication, annotation);
            return ApplicationDelegate.SharedInstance.OpenUrl(application, url, sourceApplication, annotation);
        }

        public override bool OpenUrl(UIApplication app, NSUrl url, NSDictionary options)
        {
            var openUrlOptions = new UIApplicationOpenUrlOptions(options);
            return SignIn.SharedInstance.HandleUrl(url, openUrlOptions.SourceApplication, openUrlOptions.Annotation);
        }
    }
}
