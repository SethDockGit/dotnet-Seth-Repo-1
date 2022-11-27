using System;
using System.Collections.Generic;
using System.Text;

namespace PartOrderingApp.Models
{
    public class Cart
    {
        public List<Part> Parts { get; set; }
        public decimal Total { get; set; }
        public User User { get; set; }
        public UserCategory UserCategory { get; set; }

    }

    //methods to add a part, subtract a part, calculate total price, as long as parts are available in inventory. 
    //a "check out" method will finalize the order, writing the details of the order to where they need to go and updating the inventory and userdatabase.
    
    //Question: How will the distinction of premium vs. regular user be sorted? And when will it come into play?

    //I guess this class is like a temporary order. 

    //maybe this class should be passed in a manager, but it should pass the usercategory to the manager.
}
