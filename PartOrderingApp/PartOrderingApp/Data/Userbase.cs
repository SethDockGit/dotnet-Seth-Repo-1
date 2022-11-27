using System;
using System.Collections.Generic;
using System.Text;
using PartOrderingApp.Models;

namespace PartOrderingApp.Data
{
    public class Userbase
    {
        public List<User> Users { get; set; }
        public Dictionary<string, int> UserDictionary { get; set; } //string username, int userID

        public Userbase()
        {
            Users = GetUsers();
        }
        private List<User> GetUsers()
        {
            throw new NotImplementedException();
        }
    }
}
