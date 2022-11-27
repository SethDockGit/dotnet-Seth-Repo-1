using System;
using System.Collections.Generic;
using System.Text;
using PartOrderingApp.Models;

namespace PartOrderingApp.Data
{
    public class Inventory
    {

            //this will be a text data source

            public Dictionary<int, int> InvDictionary { get; set; }
        
            List<Part> CurrentInv { get; set; }
    }
}
