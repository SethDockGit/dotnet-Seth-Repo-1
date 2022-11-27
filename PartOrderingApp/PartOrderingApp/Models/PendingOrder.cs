using System;
using System.Collections.Generic;
using System.Text;

namespace PartOrderingApp.Models
{
    public class PendingOrder : Order
    {
        public Cart Cart { get; set; }
        public string Date { get; set; }
        public int ID { get; set; }
        public User User { get; set; }
    }
    //methods: print the order, which sends all order info to UI
}
