using System;
using System.Collections.Generic;
using System.Text;

namespace PartOrderingApp.Models
{
    public class Part
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public decimal Price { get; set; }
    }
}
