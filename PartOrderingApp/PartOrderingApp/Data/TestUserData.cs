using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PartOrderingApp.Models;

namespace PartOrderingApp.Data
{
    public class TestUserData : IUserData
    {
        public User User { get; set; } //irrelevant, added to satisfy implementation, property is used in txtdatasource

        public List<User> Users { get; set; }

        //for an in-memory data source like this, do I need to update this data source when an order is being created? It doesn't have any affect on functionality...

        public TestUserData()
        {
            Users = new List<User>()
            {
                new User()
                {
                    Username = "s",
                    Category = UserCategory.Premium,

                },
                new User()
                {
                    Username = "d",
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
        public void WriteOrderToFile(Order order)
        {
            //nothing happens, but that is bad code...
            //I guess when something happens in the manager it should pass to the data layer
            //to do whatever is necessary...
        }
        public void ReWriteFile()
        {
            //nothing happens
        }
    }
}
