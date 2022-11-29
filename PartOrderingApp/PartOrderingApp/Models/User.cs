using System;
using System.Collections.Generic;
using System.Text;

namespace PartOrderingApp.Models
{
    public class User
    {
        public List<Order> Orders { get; set; }
        public string UserName { get; set; }

        public UserCategory Category { get; set; }


    }
}
