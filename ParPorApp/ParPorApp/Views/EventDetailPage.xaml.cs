using Acr.UserDialogs;
using FormsToolkit;
using ParPorApp.Helpers;
using ParPorApp.Models;
using ParPorApp.ViewModels;
using Plugin.Calendars;
using Plugin.Calendars.Abstractions;
using Plugin.ExternalMaps;
using Plugin.Geolocator;
using Plugin.Notifications;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Plugin.Share;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ParPorApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventDetailPage : ContentPage
    {
        public ObservableCollection<Grouping<string, Calendar>> _groupedCalendars;
        private Calendar calendar;

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
        private async void AddToMyCalAsync_Clicked(object sender, EventArgs e)
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Calendar);

                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Calendar))
                    {
                        await DisplayAlert("Need location", "Gunna need that location", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Calendar);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Calendar))
                        status = results[Permission.Calendar];
                }
                if (status == PermissionStatus.Granted)
                {
                    var calendars = await CrossCalendars.Current.GetCalendarsAsync();

                    CalendarEvent toAdd = new CalendarEvent();
                    CalendarEventReminder testReminder = new CalendarEventReminder();
                    testReminder.Method = CalendarReminderMethod.Default;
                    testReminder.TimeBefore = new TimeSpan(0, 15, 0);                    
                    toAdd.Name = eventType.Text;
                    toAdd.Description = gameVS.Text;
                    toAdd.Location = eventFullAddress.Text;
                    toAdd.Start = Convert.ToDateTime(evDate.Text);
                    toAdd.End = toAdd.Start.AddHours(1);
                    //toAdd.Reminders = await CrossCalendars.Current.AddEventReminderAsync();

                    Debug.WriteLine("First print");

                    await CrossCalendars.Current.AddOrUpdateEventAsync(calendars[1], toAdd);
                    Debug.WriteLine("Second print");
                }
                else if (status != PermissionStatus.Unknown)
                {
                    await DisplayAlert("Calendar access denied", "Can not continue, try again.", "OK");
                }
            }
            catch (Exception ex)
            {
                //UserDialogs.Instance.Toast(ex.Message);
                Console.WriteLine(ex.Message);
            }
        }

        private async void WhereAmI_OnClicked(object sender, EventArgs e)
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await DisplayAlert("Need location", "Gunna need that location", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Location);
                    //Best practice to always check that the key exists
                    if (results.ContainsKey(Permission.Location))
                        status = results[Permission.Location];
                }

                if (status == PermissionStatus.Granted)
                {
                    var results = await CrossGeolocator.Current.GetPositionAsync();
                    LabelGeolocation.Text = "Lat: " + results.Latitude + " Long: " + results.Longitude;
                }
                else if (status != PermissionStatus.Unknown)
                {
                    await DisplayAlert("Location Denied", "Can not continue, try again.", "OK");
                }
            }
            catch (Exception ex)
            {

                LabelGeolocation.Text = "Error: " + ex;
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