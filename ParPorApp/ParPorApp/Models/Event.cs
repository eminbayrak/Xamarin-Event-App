using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace ParPorApp.Models
{
    public class Event
    {
        [JsonProperty("Note")] public string Note { get; set; }

        [JsonProperty("LocationAddress")] public string LocationAddress { get; set; }

        [JsonProperty("UserId")] public int UserId { get; set; }

        [JsonProperty("Id")] public int Id { get; set; }

        public string NotificationId => Convert.ToString(this.Id);

        [JsonProperty("PlaceId")] public string PlaceId { get; set; }

        [JsonProperty("EventDate")] public DateTime EventDate { get; set; }

        [JsonProperty("EventType")] public string EventType { get; set; }

        [JsonProperty("EventIcon")] public string EventIcon { get; set; }

        [JsonProperty("LocationLatitude")] public string LocationLatitude { get; set; }

        [JsonProperty("LocationLongitude")] public string LocationLongitude { get; set; }

        [JsonProperty("TeamName")] public string TeamName { get; set; }

        [JsonProperty("OpponentTeamName")] public string OpponentTeamName { get; set; }
        public string GameVS => TeamName + " vs " + OpponentTeamName;

        public string GroupDate => EventDate.Date.Day == DateTime.Now.Date.Day ? "Today" : EventDate.ToString("dddd");

        //This can be implemented for the user in defferent regions
        public string EventFullDate => this.EventDate.ToString(CultureInfo.InvariantCulture);

        //Could be use for to show event post datetime
        public static string TimeAgo(DateTime dateTime)
        {
            string result;
            var timeSpan = DateTime.Now.Subtract(dateTime);

            if (timeSpan <= TimeSpan.FromSeconds(60))
            {
                result = $"{timeSpan.Seconds} seconds ago";
            }
            else if (timeSpan <= TimeSpan.FromMinutes(60))
            {
                result = timeSpan.Minutes > 1 ? $"about {timeSpan.Minutes} minutes ago"
                    :
                    "about a minute ago";
            }
            else if (timeSpan <= TimeSpan.FromHours(24))
            {
                result = timeSpan.Hours > 1 ? $"about {timeSpan.Hours} hours ago"
                    :
                    "about an hour ago";
            }
            else if (timeSpan <= TimeSpan.FromDays(30))
            {
                result = timeSpan.Days > 1 ? $"about {timeSpan.Days} days ago"
                    :
                    "yesterday";
            }
            else if (timeSpan <= TimeSpan.FromDays(365))
            {
                result = timeSpan.Days > 30 ? $"about {timeSpan.Days / 30} months ago"
                    :
                    "about a month ago";
            }
            else
            {
                result = timeSpan.Days > 365 ? $"about {timeSpan.Days / 365} years ago"
                    :
                    "about a year ago";
            }

            return result;
        }
        public class Weather
        {
            public string Icon { get; set; }
            public double Tempature { get; set; }
        }
    }

}
