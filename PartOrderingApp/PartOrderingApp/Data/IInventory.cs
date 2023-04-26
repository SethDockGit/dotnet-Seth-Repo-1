using System;
using System.Collections.Generic;
using System.Text;
using PartOrderingApp.Models;

namespace PartOrderingApp.Data
{
    public interface IInventory
    {

        public Dictionary<int, int> InvDictionary { get; set; } //key: productId, value: how many in stock
        public List<Part> Parts { get; set; }

        public void ReWriteFile();
    }  
}
