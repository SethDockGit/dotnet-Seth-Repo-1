using MaterialsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaterialsApp.Data
{
    class InMemoryDataSource : IDataSource
    {
        private List<User> Users { get; set; }
        
        public InMemoryDataSource()
        {
            Users = new List<User>()
            {
                new User()
                {
                    Username = "Timmy",
                    WoodCount = 0,
                    StoneCount = 0,
                    IronCount = 0,
                    GoldCount = 0
                },
                new User()
                {
                    Username = "Doug",
                    WoodCount = 5000,
                    StoneCount = 1000,
                    IronCount = 3000,
                    GoldCount = 100000
                }
            };
        }

        public User CheckResources(User user)
        {
            return user;
        }
        public int DepositResource(User user, int resourceKey, int deposit)
        {

            int newCount;

            switch (resourceKey)
            {
                case 1:
                    user.WoodCount = user.WoodCount + deposit;
                    newCount = user.WoodCount;
                    break;

                case 2:
                    user.StoneCount = user.StoneCount + deposit;
                    newCount = user.StoneCount;
                    break;

                case 3:
                    user.IronCount = user.IronCount + deposit;
                    newCount = user.IronCount;
                    break;

                case 4:
                    user.GoldCount = user.GoldCount + deposit;
                    newCount = user.GoldCount;
                    break;

                default:
                    throw new Exception("An incorrect case was entered into the DepositResource method");
            }

            return newCount;

        }
        public int WithdrawResource(User user, int resourceKey, int withdrawal)
        {
            int newCount;

            switch (resourceKey)
            {
                case 1:
                    user.WoodCount = user.WoodCount - withdrawal;
                    newCount = user.WoodCount;
                    break;

                case 2:
                    user.StoneCount = user.StoneCount - withdrawal;
                    newCount = user.StoneCount;
                    break;

                case 3:
                    user.IronCount = user.IronCount - withdrawal;
                    newCount = user.IronCount;
                    break;

                case 4:
                    user.GoldCount = user.GoldCount - withdrawal;
                    newCount = user.GoldCount;
                    break;

                default:
                    throw new Exception("An incorrect case was entered into the DepositResource method");
            }

            //if newCount is >= 0, return newcount, otherwise return zero and error message."
            return newCount;

        }
        public User Authenticate(string username)
        {
            User user = Users.SingleOrDefault(user => user.Username == username);
            return user;
        }
    }
}
