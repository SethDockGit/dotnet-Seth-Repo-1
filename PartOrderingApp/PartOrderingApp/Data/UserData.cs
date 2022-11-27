using System;
using System.Collections.Generic;
using System.Text;
using PartOrderingApp.Models;

namespace PartOrderingApp.Data
{
    public class UserData
    {
        public User User { get; set; }
        public List<Order> Orders { get; set; }
    }
}
