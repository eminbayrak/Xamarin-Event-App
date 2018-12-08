using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace ParPorApp.Models
{

    public class User
    {
        private string _loadedUrl = "https://ui-avatars.com/api/?background=4A384A&color=fff&rounded=true&name=";
        //private string _loadedUrl = "https://robohash.org/";
        public string ImageUrl => _loadedUrl + FirstName.Substring(0, 1) + LastName.Substring(0, 1);
        //public string ImageUrl => _loadedUrl + Email + "/set_set3/bgset_bg1/?size=128x128";
        [JsonProperty("Email")] public string Email { get; set; }

        [JsonProperty("FirstName")] public string FirstName { get; set; }

        [JsonProperty("LastName")] public string LastName { get; set; }

        [JsonProperty("Children")] public string Children { get; set; }

        [JsonProperty("HasRegistered")] public bool HasRegistered { get; set; }

        [JsonProperty("LoginProvider")] public string LoginProvider { get; set; }
        [JsonProperty("TeamName")] public string TeamName { get; set; }
        [JsonProperty("TeamCode")] public string TeamCode { get; set; }
        [JsonProperty("IsAdmin")] public string IsAdmin { get; set; }
        public string IsAdminTrue => IsAdmin.Trim();

        public bool IsAdminVisible => IsAdmin.Trim().Contains("True");

        [JsonProperty("Avatar")] public string Avatar { get; set; }

        public string FullName => FirstName + " " + LastName;

        [JsonProperty("Id")] public string Id { get; set; }
    }
}
