using System;
using System.Collections.Generic;
using System.Text;

namespace BnbProject.Models
{
    public class UserAccount
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public List<Listing> Listings { get; set; }
        public List<int> Favorites { get; set; }
        public List<Stay> Stays { get; set; }

    }
}
