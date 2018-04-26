using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

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
    }
}
