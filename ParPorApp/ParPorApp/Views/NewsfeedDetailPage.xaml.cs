using System;
using System.Threading.Tasks;
using ParPorApp.Models;
using Plugin.ExternalMaps;
using Plugin.Share;
using Plugin.Share.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParPorApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsfeedDetailPage : ContentPage
    {
	    
		public NewsfeedDetailPage(Models.Newsfeed contact)
        {
            InitializeComponent();
            BindingContext = contact ?? throw new ArgumentNullException();

        }

	    private void TakeMeThere_Clicked(object sender, EventArgs e)
	    {
			CrossExternalMaps.Current.NavigateTo("Milford", "5735 Wolfpen Pleasant Hill Rd", "Milford", "OH", "45150", "USA", "USA");
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