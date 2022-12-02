using System;
using System.Collections.Generic;
using System.Text;
using PartOrderingApp.Models;

namespace PartOrderingApp.Data
{
    public interface IUserData
    {
        public List<User> Users { get; set; }


        public User GetUser(string username);
    }

}
