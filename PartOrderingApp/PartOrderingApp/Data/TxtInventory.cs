using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using PartOrderingApp.Models;

namespace PartOrderingApp.Data
{
    public class TxtInventory : IInventory
    {
        public Dictionary<int, int> InvDictionary { get; set; }
        public List<Part> Parts { get; set; }
        private string SaveFile { get; set; }
        public TxtInventory()
        {
            SaveFile = @"C:\Data\PartOrderingApp\Inventory.txt";
            InvDictionary = new Dictionary<int, int>();
            Parts = new List<Part>();

            PopulateItems();
        }
        private void PopulateItems()
        {
            using (StreamReader sr = File.OpenText(SaveFile))
            {
                string line = "";

                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split('*');

                    for(int i = 1; i < parts.Length; i++) 
                    {
                        Part part = new Part();

                        string[] partProps = parts[i].Split('#');

                        part.Id = int.Parse(partProps[0]); 

                        Part revised = SetPartProps(part.Id, part);

                        int stock = int.Parse(partProps[1]);

                        InvDictionary.Add(part.Id, stock);

                        Parts.Add(revised);

                    }
                }
            }
        }
        private Part SetPartProps(int id, Part part)
        {
            if (id == 1)
            {
                part.Name = "GTX 5020";
                part.Category = PartCategory.GPU;
                part.Cost = 900.40m;
            }
            if (id == 2)
            {
                part.Name = "GTX 4020";
                part.Category = PartCategory.GPU;
                part.Cost = 400.23m;
            }
            if (id == 3)
            {
                part.Name = "Horizon 3050";
                part.Category = PartCategory.CPU;
                part.Cost = 500.53m;
            }
            if (id == 4)
            {
                part.Name = "Horizon 2050";
                part.Category = PartCategory.CPU;
                part.Cost = 200.74m;
            }
            if (id == 5)
            {
                part.Name = "Aero Case";
                part.Category = PartCategory.Case;
                part.Cost = 152.81m;
            }
            if (id == 6)
            {
                part.Name = "Cardboard Box";
                part.Category = PartCategory.Case;
                part.Cost = 3.33m;
            }
            if (id == 7)
            {
                part.Name = "AMX 1050";
                part.Category = PartCategory.Motherboard;
                part.Cost = 329.35m;
            }
            if (id == 8)
            {
                part.Name = "AMX 2040";
                part.Category = PartCategory.Motherboard;
                part.Cost = 632.05m;
            }

            return part;
        }
        public void ReWriteFile()
        {
            File.Delete(SaveFile);

            File.Create(SaveFile).Close();

            foreach (Part part in Parts)
            {
                using (StreamWriter sw = File.AppendText(SaveFile))
                {
                    if (part != Parts.First())
                    {
                        sw.Write("\n");
                    }
                    sw.Write($"*{part.Id}#{InvDictionary[part.Id]}");

                }
            }
        }
    }
}
