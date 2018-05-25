using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using Android;
using Android.App;
using Android.App.Usage;
using Android.Widget;
using ParPorApp.Helpers;
using ParPorApp.Models;
using ParPorApp.ViewModels;
using Plugin.ExternalMaps;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.LocalNotifications;
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
		    
		    //var eTime = item.EventDate; // get EventDate in event model
		    //double diff = (eTime - DateTime.Now).TotalHours; // subtract event's date with todays date
		    //var evDate = DateTime.FromOADate(diff); // convert double to DateTime
		    //var title = item.EventType; // assign EventType as the title 
		    //CrossLocalNotifications.Current.Show(title, title + " will start in 15 mins!", 1, evDate.AddMinutes(-15));
        }


        private void TakeMeThere_Clicked(object sender, EventArgs e)
        {
            var latitude = Convert.ToDouble(locationLatitude.Text);
            var longitude = Convert.ToDouble(locationLongitude.Text);

            try
            {

                CrossExternalMaps.Current.NavigateTo("", latitude, longitude);
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Toast(ex.Message);
            }
        }

        private void Share_onClicked(object sender, EventArgs e)
        {
            CrossShare.Current.Share(new Plugin.Share.Abstractions.ShareMessage
	        {
	            Title = "We have a " + eventType.Text,
	            Text = "Location: " + eventFullAddress.Text + "\nDate: " + eventDate.Text + " " + eventTime.Text
            });
	    }

	    private async Task Weather_OnClicked(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new WeatherWebPage());
	    }

	    private async Task ClosePageIcon_Tabbed(object sender, EventArgs e)
	    {
	        await Navigation.PopModalAsync(true);
	    }

	    private async Task AddCommentButton_Clicked(object sender, EventArgs e)
	    {
	        await Navigation.PushAsync(new AddCommentPage());
	    }

        
    }
}