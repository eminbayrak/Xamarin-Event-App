using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace ParPorApp.Models
{

    public class User
    {
        [JsonProperty("Email")] public string Email { get; set; }

        [JsonProperty("FirstName")] public string FirstName { get; set; }

        [JsonProperty("LastName")] public string LastName { get; set; }

        [JsonProperty("Children")] public string Children { get; set; }

        [JsonProperty("HasRegistered")] public bool HasRegistered { get; set; }

        [JsonProperty("LoginProvider")] public string LoginProvider { get; set; }
        [JsonProperty("TeamName")] public string TeamName { get; set; }

        [JsonProperty("Avatar")] public string Avatar { get; set; }
        public string FullName => this.FirstName + " " + this.LastName;
    }
}
