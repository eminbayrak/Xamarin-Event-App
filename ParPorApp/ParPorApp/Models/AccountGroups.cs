using ParPorApp.ViewModels;
using ParPorApp.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParPorApp.Models
{

    public class AccountGroups
    {
        private string _loadedUrl = "https://ui-avatars.com/api/?background=4A384A&color=fff&rounded=true&name=";
        //private string _loadedUrl = "https://api.adorable.io/avatars/128/";        
        //private string _loadedUrl = "https://robohash.org/";        
        //public string ImageUrl => _loadedUrl + Email + "/set_set3/bgset_bg1/?size=128x128";
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public string AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        //public string ImageUrl => _loadedUrl + FirstName.Substring(0, 1) + LastName.Substring(0, 1);
        public string ImageUrl => FirstName.Substring(0, 1) + LastName.Substring(0, 1);
        //public string ImageUrl => _loadedUrl + Email + ".png";
        public string TeamName { get; set; }
        public string TeamCode { get; set; }
        public string FullName => FirstName.Trim() + " " + LastName.Trim();

    }

    
}
