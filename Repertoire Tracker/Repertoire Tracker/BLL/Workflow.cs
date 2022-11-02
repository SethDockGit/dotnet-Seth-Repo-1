using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repertoire_Tracker.Data
{
    class Workflow
    {
        public void AddSong()
        {
            Console.Clear();

            Console.Write("Please enter the name of the song to add:");

            string songName = Console.ReadLine();   

            if (songName == "")
            {
                songName = "Untitled";
            }
            Library library = new Library();
            library.AddSong(songName);
        }
    }
}
