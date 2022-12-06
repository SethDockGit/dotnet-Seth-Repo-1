using System;
using System.Collections.Generic;
using System.Text;
using PartOrderingApp.Models;

namespace PartOrderingApp.Data
{
    public interface IInventory
    {

        public Dictionary<int, int> Inventory { get; set; } //key: productId, value: how many in stock
        public List<Part> Parts { get; set; }

        public void ReWriteFile();
    }
   
}
