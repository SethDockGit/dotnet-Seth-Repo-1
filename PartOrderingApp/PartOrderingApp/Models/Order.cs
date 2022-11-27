using System;
using System.Collections.Generic;
using System.Text;

namespace PartOrderingApp.Models
{
    public interface Order
    {
        public Cart Cart { get; set; }

        public string Date { get; set; }

        public int ID { get; set; }

        public User User { get; set; }
    }

    
}
