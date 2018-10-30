using ParPorApp.Views;
using ParPorApp.Helpers;
using ParPorApp.ViewModels;
using Xamarin.Forms;
using System;
using System.Globalization;

//using Com.OneSignal;

namespace ParPorApp
{
    public partial class App : Application
    {
        public static bool IsInBackgrounded { get; private set; }
        public App()
        {
            
            InitializeComponent();
            SetMainPage();
            
            //OneSignal.Current.StartInit("9fde7b73-f47b-459e-aae9-39756cccebf1").EndInit();
            //Background color
            MainPage.SetValue(NavigationPage.BarBackgroundColorProperty, Color.FromHex("#4A384A"));

            //Title color
            //MainPage.SetValue(NavigationPage.BarTextColorProperty, Color.FromHex("#43b05c"));

        }

        private void SetMainPage()
        {
            if (!string.IsNullOrEmpty(Settings.AccessToken))
            {
                if (Settings.AccessTokenExpirationDate < DateTime.UtcNow.AddHours(1))
                {
                    var loginViewModel = new LoginViewModel();
                    loginViewModel.LoginCommand.Execute(null);
                }
                MainPage = new NavigationPage(new MainPage()){
                    //BarBackgroundColor = Color.Accent
                };
            }
            else if (!string.IsNullOrEmpty(Settings.Email)
                  && !string.IsNullOrEmpty(Settings.Password))
            {
                MainPage = new NavigationPage(new WelcomePage());
            }
            else
            {
                MainPage = new NavigationPage(new WelcomePage());
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
            App.IsInBackgrounded = true;
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            App.IsInBackgrounded = false;
        }
    }
}
