using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParPorApp.Models;
using ParPorApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

#if __ANDROID__
using Xamarin.Forms.Platform.Android;
using Parpor.Android;
using Android.Views;
#endif

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
            AddNativeAndroidControls();
            eventsViewModel.GetEventsCommand.Execute(null);
        }

        private void AddNativeAndroidControls()
        {
#if __ANDROID__
var fab = new CheckableFab(Forms.Context)
{
  UseCompatPadding = true
};
           
fab.SetImageResource(Droid.Resource.Drawable.ic_fancy_fab_icon);
fab.Click += async (sender, e) =>
{
  await Task.Delay(3000);
  await MainPage.DisplayAlert("Native FAB Clicked", 
                                            "Whoa!!!!!!", "OK");
};
            
stack.Children.Add(fab);
absolute.Children.Add(stack);
 
// Overlay the FAB in the bottom-right corner
AbsoluteLayout.SetLayoutFlags(stack, AbsoluteLayoutFlags.PositionProportional);
AbsoluteLayout.SetLayoutBounds(stack, new Rectangle(1f, 1f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
#endif
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
            ((NavigationPage) Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#b1cfff");
            ((NavigationPage) Application.Current.MainPage).BarTextColor = Color.OrangeRed;

            eventListView.SelectedItem = null;
        }
    }
}