using System;
using System.Collections.Generic;
using System.Text;

namespace PartOrderingApp.Models
{
    public class Part
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public PartCategory Category { get; set; }
        public decimal Cost { get; set; }
        public bool IsAvailable { get; set; }
        public int SerialNumber { get; set; }

    }
}
