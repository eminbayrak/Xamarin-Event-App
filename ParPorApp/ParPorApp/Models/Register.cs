using System;
using System.Collections.Generic;
using System.Text;

namespace ParPorApp.Models
{
    class Register
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TeamName { get; set; }
        public string TeamCode
        {
            get
            {
                return TeamName.GetHashCode().ToString();
            }
            set
            {
                TeamCode.ToString();
            }
        }
    }
}
