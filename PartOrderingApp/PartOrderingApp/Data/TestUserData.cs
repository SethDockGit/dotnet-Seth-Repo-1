using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PartOrderingApp.Models;

namespace PartOrderingApp.Data
{
    public class TestUserData : IUserData
    {
        public List<User> Users { get; set; }


        public TestUserData()
        {
            Users = new List<User>()
            {
                new User()
                {
                    Username = "Seth",
                    Category = UserCategory.Premium,

                },
                new User()
                {
                    Username = "Dave",
                    Category = UserCategory.Regular
                }
            };
        }
        public User GetUser(string username)
        {
            if (Users.Any(u => u.Username == username))
            {
                User user = Users.Single(u => u.Username == username);
                return user;
            }
            else
            {
                return null;
            }
        }
        public void ReWriteFile()
        {
            //Not Applicable
        }
    }
}
