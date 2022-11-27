using System;
using System.Collections.Generic;
using System.Text;
using PartOrderingApp.Data;
using PartOrderingApp.Models;

namespace PartOrderingApp.Logic
{
    public class Manager
    {
        public Userbase Userbase { get; set; }

        public User Authenticate(string username, string password)
        {

            //this method will acess the userbase to return a user, and you can delete this temporary user
            return new User()
            {
                UserName = username,
                UserId = 1,
            };

        }
    }
}
