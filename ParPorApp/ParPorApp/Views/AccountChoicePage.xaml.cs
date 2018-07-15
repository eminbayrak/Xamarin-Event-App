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
	public partial class AccountChoicePage : ContentPage
	{
		public AccountChoicePage ()
		{
			InitializeComponent ();
		}

        private async void ButtonCoach_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CoachRegisterPage());

        }

        private async void ButtonParent_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());

        }
    }
}