using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_do_list.Data;
using To_do_list.Models;
using To_do_list.UI;

namespace To_do_list.Logic
{
    internal class Workflow
    {

        //create a method that takes in a key press, and if the key press is escape, it returns to main menu,
        //  ... and if it's anything else it does nothing

        private void ListSubsections()
        {

        }
        private void AddSubsection()
        {

        }
        internal void ListItems()
        {
            Library library = new Library();

            foreach (Item item in library.Items)
            {
                Console.WriteLine($"{item.ID}: {item.Text}");
            }
            //if there are no items in Items yet, is this a problem?
        }
        public void AddItem()
        {
            Console.Clear();
            Console.WriteLine("Please write the text for your new entry, then press enter");
            Console.WriteLine();

            string text = Console.ReadLine();

            Library library = new Library();
            library.AddItem(text);
            Console.Clear();
        }
        public void EditItem(string text, int id)
        {
            Library library = new Library();
            library.EditItem(text, id);
            Console.Clear();

        }
        public void DeleteItem(int Id)
        {
            //so for your to-do list, you could pass in an ID and a list to the edit or delete method, use linq to identify which ID to remove (where ID is not equal to ID)
            //.. and then return a new list to populate the library with.

            Library library = new Library();
            //by now I've instantiated the library twice, once in userInt with workflow.listitems, and then again here.
            library.DeleteItem(Id);
            Console.Clear();

        }
        public void ReturnToMain()
        {
            Console.Clear();
            UserInt userInt = new UserInt();
            userInt.Run();
        }
    }
}
