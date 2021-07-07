using System;
using System.Collections.Generic;
using System.Text;

namespace ParPorApp.Models
{
    public class AccountStudent
    {
        public string AccountId { get; set; }        
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
