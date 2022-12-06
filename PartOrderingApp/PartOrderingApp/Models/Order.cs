using System;
using System.Collections.Generic;
using System.Text;

namespace PartOrderingApp.Models
{
    public class Order
    {

        public DateTime DateTime { get; set; } 
        public List<Part> Parts { get; set; } 
        public decimal Total { get; set; }  
        public bool PendingStatus { get; set; } = true; //FIX THIS ONCE DATA HAS BEEN SETUP!!!
        public int OrderID { get; set; }
        public bool ObsoleteID { get; set; }
    }

    
}
