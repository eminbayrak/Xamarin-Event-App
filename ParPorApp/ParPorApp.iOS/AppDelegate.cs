﻿using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

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
            // Set app theme
            UITabBar.Appearance.BarTintColor = UIColor.LightTextColor;
            UITabBar.Appearance.TintColor = UIColor.White;
            UIProgressView.Appearance.TintColor = UIColor.FromRGB(188, 75, 75);
            LoadApplication(new App());
            UINavigationBar.Appearance.BarTintColor = UIColor.FromRGB(255, 20, 153);
            UINavigationBar.Appearance.TintColor = UIColor.White;
            return base.FinishedLaunching(app, options);


        }
    }
}
