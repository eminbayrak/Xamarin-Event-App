using System;
using System.Collections.Generic;
using System.Text;

namespace ParPorApp.Models
{
    public class AccountEvent
    {
        public string AccountId { get; set; }        
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
