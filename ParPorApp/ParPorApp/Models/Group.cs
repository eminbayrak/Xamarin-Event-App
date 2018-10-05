using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ParPorApp.Models
{
    public class Group
    {
        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("DistrictId")]
        public string DistrictId { get; set; }

        [JsonProperty("Id")]
        public long Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("UserId")]
        public string UserId { get; set; }

        [JsonProperty("Picture")]
        public string Picture { get; set; }

        [JsonProperty("gameIcon")]
        public string gameIcon { get; set; }

        [JsonProperty("postDate")]
        public string postDate { get; set; }

        
        public ICollection<GroupEvent> GroupEvents { get; set; }
        public ICollection<GroupStudent> GroupStudents { get; set; }
        
    }

}
