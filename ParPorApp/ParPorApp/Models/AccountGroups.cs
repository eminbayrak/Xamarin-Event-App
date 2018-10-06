using System;
using System.Collections.Generic;
using System.Text;

namespace ParPorApp.Models
{

    class AccountGroups
    {
        //private string _loadedUrl = "https://ui-avatars.com/api/?rounded=true&name=";
        private string _loadedUrl = "https://api.adorable.io/avatars/128/";
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public string AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        //public string ImageUrl => _loadedUrl + FirstName.Substring(0, 1) + LastName.Substring(0, 1);
        public string ImageUrl => _loadedUrl + Email + ".png";
        public string TeamName { get; set; }
        public string FullName => FirstName.Trim() + " " + LastName.Trim();
        
        
    }
}
