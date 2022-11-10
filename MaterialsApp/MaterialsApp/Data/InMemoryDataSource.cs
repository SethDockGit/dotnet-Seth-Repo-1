using MaterialsApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
        public int DepositWood(User user, int amount)
        {
            user.WoodCount += amount;
            return user.WoodCount;
        }
        public int DepositStone(User user, int amount)
        {
            user.StoneCount += amount;
            return user.StoneCount;
        }
        public int DepositIron(User user, int amount)
        {
            user.IronCount += amount;
            return user.IronCount;
        }
        public int DepositGold(User user, int amount)
        {
            user.GoldCount += amount;
            return user.GoldCount;
        }
        public int WithdrawWood(User user, int amount)
        {
            if (user.WoodCount >= amount)
            {
                user.WoodCount -= amount;
            }
            else
            {
                return -1;
            }
            return user.WoodCount;
            
        }
        public int WithdrawStone(User user, int amount)
        {
            if (user.StoneCount >= amount)
            {
                user.StoneCount -= amount;
            }
            else
            {
                return -1;
            }
            return user.StoneCount;
        }
        public int WithdrawIron(User user, int amount)
        {
            if (user.IronCount >= amount)
            {
                user.IronCount -= amount;
            }
            else
            {
                return -1;
            }
            return user.IronCount;
        }
        public int WithdrawGold(User user, int amount)
        {
            if (user.GoldCount >= amount)
            {
                user.GoldCount -= amount;
                    return user.GoldCount;
            }
            else
            {
                return -1;
            }
        }
        public User Authenticate(string username)
        {
            User user = Users.SingleOrDefault(user => user.Username == username);
            return user;
        }
    }
}
