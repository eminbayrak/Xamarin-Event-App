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
	public partial class WeatherWebPage : ContentPage
	{
		public WeatherWebPage ()
		{
			InitializeComponent ();
		    NavigationPage.SetHasBackButton(this, true);
        }

	    private async void NavigateBack_ClickedAsync(object sender, EventArgs e)
	    {
	        await Navigation.PopAsync();
	    }
	}
}