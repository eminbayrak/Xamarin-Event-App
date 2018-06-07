using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParPorApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            //Init();
        }

        //void Init()
        //{
        //    LogoIcon.HeightRequest = Constants.LogoIconHeight;
        //}

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());

        }

        private async void ReturnWelcome_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();

        }
    }
}