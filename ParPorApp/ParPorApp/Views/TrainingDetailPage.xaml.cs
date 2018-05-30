using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acr.UserDialogs;
using ParPorApp.Models;
using Plugin.ExternalMaps;
using System.Threading.Tasks;
using Plugin.Share;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParPorApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TrainingDetailPage : ContentPage
	{
		public TrainingDetailPage (Event item)
        {
			InitializeComponent ();
		    BindingContext = item ?? throw new ArgumentNullException();
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