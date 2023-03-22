using System;
using System.Collections.Generic;
using System.Text;

namespace BnbProject.Models
{
    public class Review
    {
        public int StayId { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; }
        public string Username { get; set; }
    }
}
