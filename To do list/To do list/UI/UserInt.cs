using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_do_list.Data;
using To_do_list.Logic;

namespace To_do_list.UI
{
    public class UserInt
    {

        public void Run()
        {
            var userExit = false;
            while (!userExit)
            {

                Console.WriteLine("-----Your To-Do List-----");
                Console.WriteLine("\nOptions:\n E: Edit an entry \n A: Add an entry, \n D: Delete an entry \n Esc: Exit");
                Console.WriteLine();
                Console.WriteLine("-----Items-----");
                Console.WriteLine();

                Workflow workflow = new Workflow();

                workflow.ListItems();

                var cki = Console.ReadKey(true);


                switch (cki.Key)
                {

                    case ConsoleKey.A:

                        workflow.AddItem();

                        break;
                    case ConsoleKey.E:

                        Console.WriteLine("\nEnter the ID of an item to edit");
                        string edit = Console.ReadLine();
                        int editId;
                        bool success = int.TryParse(edit, out editId);  
                        while(!success)
                        {
                            workflow.ReturnToMain();
                        }
                        Library library = new Library();
                        if(editId > library.Items.Count)
                        {
                            workflow.ReturnToMain();
                        }
                        Console.WriteLine("\nEnter the new text for the item \n");
                        string newText = Console.ReadLine();
                        workflow.EditItem(newText, editId);

                        break;
                    case ConsoleKey.D:

                        Console.WriteLine("\nEnter the ID of an item to delete");
                        string delete = Console.ReadLine();
                        int deleteID;
                        bool successTwo = int.TryParse(delete, out deleteID);
                        while (!successTwo)
                        {
                            workflow.ReturnToMain();
                        }

                        workflow.DeleteItem(deleteID);
                        break;
                    case ConsoleKey.Escape:

                        userExit = true;

                        break;

                    default:
                        break;
                }
                Console.Clear();
            }
        }
    }
}
