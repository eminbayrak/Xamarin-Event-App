using Acr.UserDialogs;
using ParPorApp.Models;
using Plugin.ExternalMaps;
using Plugin.Notifications;
using Plugin.Share;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParPorApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventDetailPage : ContentPage
    {
        public bool IsRunning { get; private set; }

        public EventDetailPage(Event item)
        {
            InitializeComponent();
            BindingContext = item ?? throw new ArgumentNullException();
            IsRunning = !IsRunning;
            var list = CrossNotifications.Current.GetScheduledNotifications();
            Console.WriteLine(list);
            //var eTime = item.EventDate; // get EventDate in event model
            //double diff = (eTime - DateTime.Now).TotalHours; // subtract event's date with todays date
            //var evDate = DateTime.FromOADate(diff); // convert double to DateTime
            //var title = item.EventType; // assign EventType as the title
            //CrossLocalNotifications.Current.Show(title, title + " will start in 15 mins!", 1, evDate.AddMinutes(-15));
        }

        private async void Notification_onClickedAsync(object sender, EventArgs e)
        {
            var notId = Convert.ToInt16(notificationId.Text);
            var eDate = Convert.ToDateTime(evDate.Text);

            try
            {
                if (IsRunning)
                {
                    //CrossLocalNotifications.Current.Show(eventType.Text, eventType + " will start in 15 mins!", notId, eDate.AddMinutes(-15));
                    UserDialogs.Instance.Toast("You will be notified for this event");
                    Notification notification = new Notification
                    {
                        Id = notId,
                        Title = gameVS.Text + " - " + eventTime.Text,
                        Message = eventType.Text + " will start in 15 mins!",
                        Date = eDate,
                        Vibrate = true
                    };
                    await CrossNotifications.Current.Send(notification);
                    var list = await CrossNotifications.Current.GetScheduledNotifications();
                }
                else
                {
                    await CrossNotifications.Current.Cancel(notId);
                    UserDialogs.Instance.Toast("It's removed");
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.Toast(ex.Message);
            }
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