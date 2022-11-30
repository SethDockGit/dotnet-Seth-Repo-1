﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PartOrderingApp.Models
{
    public class User
    {
        public List<Order> Orders = new List<Order>(); //THIS WILL CHANGE!! ORDERS WILL BE INSTANTIATED
                                                        //WHEN THE DATASET IS UP AND RUNNING
        public string UserName { get; set; }
        public UserCategory Category { get; set; }
    }
}
