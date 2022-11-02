using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repertoire_Tracker.Model;

namespace Repertoire_Tracker.Data
{
    class Library
    {
        string SaveFile { get; set; }
        List<Song> Songs { get; set; }


        public Library()
        {
            SaveFile = @"C:\Data\RepertoireTracker\data.txt";
            Songs = new List<Song>();

            if(File.Exists(SaveFile))
            {
                PopulateSongs();
               
                //checking to see if save file exists, if it doesn't go to else and create a file
            }
            else
            {
                File.Create(SaveFile);
            }
        }
        private void PopulateSongs()
        {
            using(StreamReader sr = File.OpenText(SaveFile))
            {
                string line = "";

                while((line = sr.ReadLine()) != null)
                { 
                    string[] splitLine = line.Split(',');

                    Song song = new Song()
                    {
                    Id = int.Parse(splitLine[0]),
                    Title = splitLine[1],
                    };

                    Songs.Add(song);
                }
            }
        }

        public void AddSong(string songName)
        {

        }

        public List<Song> GetAllSongs()
        {
            return Songs;   
        }
    }
}
