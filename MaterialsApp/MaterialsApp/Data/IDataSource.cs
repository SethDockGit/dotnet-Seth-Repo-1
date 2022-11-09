using MaterialsApp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MaterialsApp.Data
{
    interface IDataSource
    {
        public User CheckResources(User user);
        public int DepositResource(User user, int key, int deposit);
        public int WithdrawResource(User user, int key, int withdrawal);
        public User Authenticate(string username);

    }
}
