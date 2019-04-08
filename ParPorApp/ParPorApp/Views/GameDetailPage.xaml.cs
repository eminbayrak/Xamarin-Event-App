using Acr.UserDialogs;
using DarkSkyApi;
using DarkSkyApi.Models;
using ParPorApp.Models;
using Plugin.Calendars;
using Plugin.Calendars.Abstractions;
using Plugin.ExternalMaps;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Plugin.Share;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            tempTitle.Text = result.Currently.Icon.ToUpper().Replace("-", " ");
            tempIcon.Source = result.Currently.Icon.Replace("-", "") + ".png";
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

        private async void AddToMyCalAsync_Clicked(object sender, EventArgs e)
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Calendar);

                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Calendar))
                    {
                        await DisplayAlert("Calendar", "Need permission to access your primary calendar", "Accept");
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
    }
}