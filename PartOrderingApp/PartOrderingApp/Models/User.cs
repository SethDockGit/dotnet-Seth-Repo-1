using System;
using System.Collections.Generic;
using System.Text;

namespace PartOrderingApp.Models
{
    public class User
    {
        public Account Account { get; set; }

        public List<PendingOrder> PendingOrders { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int UserId { get; set; } 
    }
}
