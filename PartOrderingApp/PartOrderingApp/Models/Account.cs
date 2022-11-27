using System;
using System.Collections.Generic;
using System.Text;
using PartOrderingApp.Data;

namespace PartOrderingApp.Models
{
    public class Account
    {
        public User User { get; set; }
        public List<PendingOrder> PendingOrders { get; set; }
        public UserData UserData { get; set; }

        public Account(User user)
        {
            User = user;
        }

        //this class might be useless, I'm not sure yet. However, this seems to be the only class where I store UserData.
        //When the user asks the interface for their pending order history, it should get set in the account.
    }
}
