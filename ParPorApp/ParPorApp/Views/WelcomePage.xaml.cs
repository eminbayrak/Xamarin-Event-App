using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParPorApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomePage : ContentPage
    {
        public WelcomePage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            Title = "Welcome!";
            SignInFunction();
        }

        private async void ButtonLogin_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());

        }

        private async void ButtonRegister_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AccountChoicePage());

        }

        void SignInFunction()
        {
            signInLbl.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() => { Application.Current.MainPage = new LoginPage(); })
            });
        }

       
    }
}
