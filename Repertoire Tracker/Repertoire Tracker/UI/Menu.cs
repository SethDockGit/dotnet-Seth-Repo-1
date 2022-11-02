using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repertoire_Tracker.Data;

namespace Repertoire_Tracker.UI
{
    public class Menu
    {

        public void Run()
        {
            bool userExit = false;
            while (!userExit)
            {
                Console.Clear();

                Console.WriteLine("**Repertoire Tracker**");
                Console.WriteLine();
                Console.WriteLine("1. List my repertoire");
                Console.WriteLine("2. Add my Repertoire");
                Console.WriteLine();
                Console.Write("Enter a number to make a selection, or ESC to quit: ");

                ConsoleKeyInfo cki = Console.ReadKey(true);

                Workflow workflow = new Workflow();

                switch (cki.Key)
                {
                    case ConsoleKey.D1:
                        workflow.ListSongs();

                        break;
                    case ConsoleKey.D2:
                        Workflow workflow = new Workflow();
                        workflow.AddSong();
                        break;
                    case ConsoleKey.Escape:
                        break;



                }
            }
        }
    }
}
