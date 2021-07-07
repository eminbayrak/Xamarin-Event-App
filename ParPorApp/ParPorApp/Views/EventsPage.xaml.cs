using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParPorApp.Models;
using ParPorApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace ParPorApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventsPage : ContentPage
    {
        private readonly EventsViewModel eventsViewModel;
        public EventsPage()
        {
            InitializeComponent();
            BindingContext = eventsViewModel = new EventsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            eventsViewModel.GetEventsCommand.Execute(null);
        }
        private async void AddEvent_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddEventPage());
        }

        //navigating to event detail page
        private async void EventList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as Event;
            if (e.SelectedItem == null)
                return;

            //Training detail page
            if (item != null && item.EventType == "Training")
            {
                await Navigation.PushAsync(new TrainingDetailPage(item));
                
                ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#b1cfff");
                //((NavigationPage)Application.Current.MainPage).BarTextColor = Color.OrangeRed;
            }

            //Game detail page
            if (item != null && item.EventType == "Game")
            {
                await Navigation.PushModalAsync(new GameDetailPage(item));
                //await DisplayAlert("Games", "you tabbed on a game", "Ok");
            }
            eventListView.SelectedItem = null;
        }
    }
}