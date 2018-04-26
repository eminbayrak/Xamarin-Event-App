using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acr.UserDialogs;
using ParPorApp.Models;
using ParPorApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParPorApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    
    public partial class EventsPage : ContentPage
    {
	    EventsViewModel eventsViewModel;
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


	    private async Task AddEvent_Clicked(object sender, EventArgs e)
	    {
			await Navigation.PushAsync(new AddEventPage());
		}

        //void ItemTapped(object sender, ItemTappedEventArgs e)
        //{
        //    Navigation.PushAsync(new EventDetailPage() { BindingContext = eventsViewModel = new EventsViewModel((Event)e.Item) });

        //    ((ListView)sender).SelectedItem = null;
        //}
        private async Task EventList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            using (UserDialogs.Instance.Loading("Loading...", null, null, true, MaskType.Black))
            {
                await Task.Delay(100);
                if (e.SelectedItem == null)
                    return;
                var item = e.SelectedItem as Event;
                await Navigation.PushAsync(new EventDetailPage(item));
                eventListView.SelectedItem = null;
            }
        }
    }
}