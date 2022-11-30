using System;
using System.Collections.Generic;
using System.Text;

namespace PartOrderingApp.Models
{
    public class Order
    {

        public DateTime DateTime { get; set; } //date time should be set with the aDate.Now function or whatever it is. To be stored as a datetime and displayed as a string (tostring) when accessed.

        public List<Part> Parts { get; set; } 

        public int NumOfParts { get; set; }
        public decimal Total { get; set; }  

        public Dictionary<Part, int> Dictionary = new Dictionary<Part, int>();

        public bool PendingStatus { get; set; }
    }

    
}
