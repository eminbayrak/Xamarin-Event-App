using System;
using System.Collections.Generic;
using System.Text;

namespace ParPorApp.Models
{
    class Register
    {
        public User User { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TeamName { get; set; }
        public string TeamCode { get; set; }
        public string EnteredCode { get; set; }
        public string Id { get; set; }
        public DateTime AccountDate { get; set; }
    }
}
