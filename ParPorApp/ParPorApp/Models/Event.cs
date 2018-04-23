using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ParPorApp.Models
{
    public class Event
    {
	    [JsonProperty("Description")]
		public string Description { get; set; }

	    [JsonProperty("StartDateTime")]
	    public string StartDateTime { get; set; }

        [JsonProperty("GroupId")]
		public string GroupId { get; set; }

	    [JsonProperty("Id")]
		public int Id { get; set; }

	    [JsonProperty("LocationId")]
		public string LocationId { get; set; }

	    [JsonProperty("EndDateTime")]
	    public string EndDateTime { get; set; }

        [JsonProperty("Name")]
		public string Name { get; set; }

        [JsonProperty("First")]
        public  string First { get; set; }

        [JsonProperty("ImageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("PostDate")]
        public string PostDate { get; set; }

        [JsonProperty("LocationName")]
        public string LocationName { get; set; }

        [JsonProperty("Longitude")]
        public string Longitude { get; set; }

        [JsonProperty("Latitude")]
        public string Latitude { get; set; }
    }
}
