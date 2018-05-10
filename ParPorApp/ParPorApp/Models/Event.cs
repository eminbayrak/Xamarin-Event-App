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

        [JsonProperty("GroupId")] public string GroupId { get; set; }

        [JsonProperty("Id")] public int Id { get; set; }

        [JsonProperty("PlaceId")] public string PlaceId { get; set; }

        [JsonProperty("EventDate")] public DateTime EventDate { get; set; }

        [JsonProperty("EventType")] public string EventType { get; set; }

        [JsonProperty("EventIcon")] public string EventIcon { get; set; }

        [JsonProperty("EventTime")] public string EventTime { get; set; }

        [JsonProperty("LocationLatitude")] public string LocationLatitude { get; set; }

        [JsonProperty("LocationLongitude")] public string LocationLongitude { get; set; }

        [JsonProperty("TeamName")] public string TeamName { get; set; }

        [JsonProperty("OpponentTeamName")] public string OpponentTeamName { get; set; }

        public string GroupDate => EventDate.Date.Day == DateTime.Now.Date.Day ? "Today" : EventDate.ToString("dddd");
        public string EventFullDate => this.EventDate.ToString(CultureInfo.InvariantCulture) + " " + this.EventTime;

        public static string TimeAgo(DateTime dateTime)
        {
            string result;
            var timeSpan = DateTime.Now.Subtract(dateTime);

            if (timeSpan <= TimeSpan.FromSeconds(60))
            {
                result = string.Format("{0} seconds ago", timeSpan.Seconds);
            }
            else if (timeSpan <= TimeSpan.FromMinutes(60))
            {
                result = timeSpan.Minutes > 1 ?
                    String.Format("about {0} minutes ago", timeSpan.Minutes) :
                    "about a minute ago";
            }
            else if (timeSpan <= TimeSpan.FromHours(24))
            {
                result = timeSpan.Hours > 1 ?
                    String.Format("about {0} hours ago", timeSpan.Hours) :
                    "about an hour ago";
            }
            else if (timeSpan <= TimeSpan.FromDays(30))
            {
                result = timeSpan.Days > 1 ?
                    String.Format("about {0} days ago", timeSpan.Days) :
                    "yesterday";
            }
            else if (timeSpan <= TimeSpan.FromDays(365))
            {
                result = timeSpan.Days > 30 ?
                    String.Format("about {0} months ago", timeSpan.Days / 30) :
                    "about a month ago";
            }
            else
            {
                result = timeSpan.Days > 365 ?
                    String.Format("about {0} years ago", timeSpan.Days / 365) :
                    "about a year ago";
            }

            return result;
        }
        
    }

}
