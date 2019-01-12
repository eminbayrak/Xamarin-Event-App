using Acr.UserDialogs;
using DarkSkyApi;
using DarkSkyApi.Models;
using ParPorApp.Models;
using Plugin.ExternalMaps;
using Plugin.Share;
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
    public partial class GameDetailPage : ContentPage
    {
        public GameDetailPage(Event item)
        {
            InitializeComponent();
            BindingContext = item ?? throw new ArgumentNullException();
            DarkSky();
        }

        public async void DarkSky ()
        {
            var client = new DarkSkyService(Constants.DarkSkyApi);
            Forecast result = await client.GetTimeMachineWeatherAsync(
                Convert.ToDouble(locationLatitude.Text),
                Convert.ToDouble(locationLongitude.Text),
                DateTimeOffset.Now
                );
            tempature.Text = Convert.ToString(result.Currently.Temperature);
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
    }
}