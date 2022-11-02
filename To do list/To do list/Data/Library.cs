using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_do_list.Models;
using System.IO;
using To_do_list.Logic;


namespace To_do_list.Data
{
    internal class Library
    {
        //ideally I would set up diff folders for each subsection, but if it's too advanced to...
        // ... create the folders in library, I'll stick with one txt file for items
        string SaveFolder { get; set; }
        string SaveFile { get; set; }
        public List<Subsection> Subsections { get; set; }
        public List<Item> Items { get; set; }
         
        public Library()
        {
            SaveFile = @"C:\Data\Todolist\data.txt";
            Items = new List<Item>();

            if (File.Exists(SaveFile))
            {
                PopulateItems();
            }
            else
            {
                File.Create(SaveFile).Close();
            }
        }

        private void PopulateItems()
        {
            using(StreamReader sr = File.OpenText(SaveFile))
            {
                string line = "";

                while((line = sr.ReadLine()) != null)
                {
                    string[] splitline = line.Split(',');



                    Item item = new Item()      //at this next point, there is an exception/error where input string was not in a correct format.
                    {
                        ID = int.Parse(splitline[0]),
                        Text = splitline[1]
                    };

                    Items.Add(item);
                }
            }
        }

        public void AddItem(string itemText)
        {
            Item item = new Item();
            int newID;

            if(Items.Count > 0)
            {
                newID = Items
                    .OrderByDescending(s => s.ID)
                    .First()
                    .ID + 1;
            }
            else
            {
                newID = 1;
            }

            using(StreamWriter sw = File.AppendText(SaveFile))
            {
                if(newID != 1)
                {
                    sw.Write("\n");
                }
                sw.Write($"{newID},{itemText}");
            }
        }
        public void DeleteItem(int id)
        {
            if (id > Items.Count)
            {
                Workflow workflow = new Workflow();
                workflow.ReturnToMain();
            }
            else
            {

                var items = GetItems();

                var toRemove = items.Single(item => item.ID == id);

                Items.Remove(toRemove);


                File.Delete(SaveFile);

                File.Create(SaveFile).Close();

                //here I am removing the unwanted item from the list, deleting the save file, creating a new one, then adding items to the file based on my list of items....



                foreach (var item in Items)
                {
                    if (Items.Count > 0)
                    {
                        using (StreamWriter sw = File.AppendText(SaveFile))
                        {
                            if (item.ID != 1)
                            {
                                sw.Write("\n");
                            }
                            sw.Write($"{item.ID},{item.Text}");
                        }
                    }
                }
            }
        }
        public void EditItem(string text, int id)
        {

            var items = GetItems();

            var toEdit = items.Single(item => item.ID == id);

            toEdit.ID = id;
            toEdit.Text = text;

            File.Delete(SaveFile);

            File.Create(SaveFile).Close();

            foreach (var item in Items)
            {
                if (Items.Count > 0)
                {
                    using (StreamWriter sw = File.AppendText(SaveFile))
                    {
                        if (item.ID != 1)
                        {
                            sw.Write("\n");
                        }
                        sw.Write($"{item.ID},{item.Text}");
                    }
                }
            }
        }
        public List<Item> GetItems()
        {
            return Items;
        }
    }
}
