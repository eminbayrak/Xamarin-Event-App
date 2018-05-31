using System;

namespace ParPorApp.Models
{
    public class GoogleUser
    {
        public string Token { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public Uri Picture { get; set; }
    }
}