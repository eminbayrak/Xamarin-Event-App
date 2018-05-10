using System;
using System.Collections.Generic;
using System.Linq;
using FFImageLoading.Forms.Touch;
using FFImageLoading.Svg.Forms;
using Foundation;
using Lottie.Forms.iOS.Renderers;
using UIKit;

namespace ParPorApp.iOS
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            AnimationViewRenderer.Init();
            UIApplication.Main(args, null, "AppDelegate");
            CachedImageRenderer.Init();
            var ignore = typeof(SvgCachedImage);
        }
    }
}
