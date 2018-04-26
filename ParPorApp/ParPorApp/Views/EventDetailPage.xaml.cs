using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Android.App.Usage;
using ParPorApp.Helpers;
using ParPorApp.Models;
using ParPorApp.ViewModels;
using Plugin.ExternalMaps;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.Share;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParPorApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EventDetailPage : ContentPage
	{
	    public EventDetailPage (Event item)
		{
            InitializeComponent ();
            BindingContext = item ?? throw new ArgumentNullException();
        }

        

	    //public ICommand TakeMeThereCommand
     //   {
	    //    get
	    //    {
	    //        return new Command(async () =>
	    //        {
	    //            var latitude = fullAddress.Text;
	                
     //               Console.WriteLine(latitude);


	    //        });
	    //    }
	    //}
        private void TakeMeThere_Clicked(object sender, EventArgs e)
        {
            var longitude = fullAddress.Text;
            string[] words = longitude.Split(',');
            foreach (var word in words)
            {
                System.Console.WriteLine($"<{word}>");
            }

            try
            {

                //CrossExternalMaps.Current.NavigateTo(longitude);
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Toast(ex.Message);
            }
        }

        //private double lat = -122.3491;
        //private async Task TakeMeThere_Clicked(Event nav)
        //{
        //       await CrossExternalMaps.Current.NavigateTo(nav.Name, "Adress", "City", "State", "ZipCode", "Country", "Country");
        //}
        private void Share_onClicked(object sender, EventArgs e)
	    {
	        CrossShare.Current.Share(new Plugin.Share.Abstractions.ShareMessage
	        {
	            Text = "We have a game!",
	            Title = "GAME"
	        });
	    }

	    private async Task Weather_OnClicked(object sender, EventArgs e)
	    {
	        await Navigation.PushModalAsync(new WeatherWebPage());
	    }
    }
}