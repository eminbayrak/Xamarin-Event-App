using System;
using System.Collections.Generic;
using System.Text;

namespace ParPorApp.Models
{
    class AccountGroups
    {
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public string AccountId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string TeamName { get; set; }

        public string FullName => this.FirstName + " " + this.LastName;
    }
}
