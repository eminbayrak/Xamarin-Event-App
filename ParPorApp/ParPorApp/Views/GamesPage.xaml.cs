using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParPorApp.Models;
using ParPorApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParPorApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GamesPage : ContentPage
    {
        private readonly EventsViewModel eventsViewModel;
        public GamesPage()
        {
            InitializeComponent();
            BindingContext = eventsViewModel = new EventsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            eventsViewModel.GetGamessCommand.Execute(null);
        }
        private async Task AddEvent_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddEventPage());
        }

        private async Task EventList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            var item = e.SelectedItem as Event;
            await Navigation.PushAsync(new EventDetailPage(item));
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#b1cfff");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.OrangeRed;

            eventListView.SelectedItem = null;
        }
    }
}