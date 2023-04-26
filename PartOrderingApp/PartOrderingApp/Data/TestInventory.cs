using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PartOrderingApp.Models;

namespace PartOrderingApp.Data
{
    public class TestInventory : IInventory
    {
        public Dictionary<int, int> InvDictionary { get; set; } //key: productId, value: how many in stock
        public List<Part> Parts { get; set; }

        public TestInventory()
        {
            Parts = new List<Part>()
            {
                new Part()
                {
                    Id = 1,
                    Name = "GTX 5020",
                    Category = PartCategory.GPU,
                    Cost = 900.40m
                },
                new Part()
                {
                    Id = 2,
                    Name = "GTX 4020",
                    Category = PartCategory.GPU,
                    Cost = 400.23m
                },
                new Part()
                {
                    Id = 3,
                    Name = "Horizon 3050",
                    Category = PartCategory.CPU,
                    Cost = 500.53m
                },
                new Part()
                {
                    Id = 4,
                    Name = "Horizon 2050",
                    Category = PartCategory.CPU,
                    Cost = 200.74m
                },
                new Part()
                {
                    Id = 5,
                    Name = "Aero Case",
                    Category = PartCategory.Case,
                    Cost = 152.81m
                },
                new Part()
                {
                    Id = 6,
                    Name = "Cardboard Box",
                    Category = PartCategory.Case,
                    Cost = 3.33m
                },
                new Part()
                {
                    Id = 7,
                    Name = "AMX 1050",
                    Category = PartCategory.Motherboard,
                    Cost = 329.35m
                },
                new Part()
                {
                    Id = 8,
                    Name = "AMX 2040",
                    Category = PartCategory.Motherboard,
                    Cost = 632.05m
                },
            };

            InvDictionary = new Dictionary<int, int>() 
            {
                {1, 52 }, 
                {2, 26 },
                {3, 0 },
                {4, 12 },
                {5, 2 },
                {6, 1 },
                {7, 126 },
                {8, 32 }
            };
        }

        public void ReWriteFile()
        {
            //Not Applicable
        }
    }
}
