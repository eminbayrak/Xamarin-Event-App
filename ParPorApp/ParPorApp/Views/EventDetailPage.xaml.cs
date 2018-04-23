using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Android.Icu.Text;
using Android.Locations;
using ParPorApp.Models;
using ParPorApp.ViewModels;
using Plugin.ExternalMaps;
using Plugin.ExternalMaps.Abstractions;
using Plugin.Share;
using ScnPage.Plugin.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParPorApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EventDetailPage : ContentPage
	{
	    EventsViewModel eventsViewModel;
        public EventDetailPage ()
		{
            InitializeComponent ();
		    BindingContext = eventsViewModel = new EventsViewModel();
		    
		    //Debug.WriteLine(_event);
		}

	    //public string NavName
     //   {
     //       get => Event.Name;
     //   }

        protected override void OnAppearing()
	    {
	        base.OnAppearing();
	        eventsViewModel.GetEventsCommand.Execute(null);
	        
	    }
        

        //private double lat = -122.3491;
        private async void TakeMeThere_Clicked(object sender, EventArgs e)
        {
            var _event = new Event();
            BindingContext = _event;
            double longt = Convert.ToDouble(_event.Longitude);
            var loc = string.Format("{0},{1}", _event.Latitude, _event.Longitude);
            await CrossExternalMaps.Current.NavigateTo(_event.Name, longt, Helpers.Settings.LongitudeKeySettings);
            //await CrossExternalMaps.Current.NavigateTo(_event.Name, _event.EndDateTime, "City", "State", "ZipCode", "Country", "Country");
        }
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