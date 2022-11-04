using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice
{
    internal class Program
    {

        //use linq to manipulate a list of songs that have attributes such as key
        //an enum is like any other class property, except it has a limited definition!! instead of int or string
        //which has endless possibility, enum is a category that can be 1 of a certain group
        static void Main(string[] args)
        {
            List<song> songs = GetSongs();

            List<song> songsInD = songs.Where(s => s.Key == Key.D).ToList();

            foreach (song song in songsInD)
            {
                Console.WriteLine($"{song.Name}");
            }
            Console.ReadLine(); 

        }

        public static List<song> GetSongs()
        {
            return new List<song>()
            {
                new song()
                {
                    Name = "Life of sin",
                    Artist = "Sturgill Simpson",
                    Key = Key.E,
                },
                new song()
                {
                    Name = "Livin the Dream",
                    Artist = "Sturgill Simpson",
                    Key = Key.F,
                },
                new song()
                {
                    Name = "Voices",
                    Artist = "Sturgill Simpson",
                    Key = Key.D,
                },
                new song()
                {
                    Name = "Breaker's Roar",
                    Artist = "Sturgill Simpson",
                    Key = Key.D,
                },
            };
        }
    }
}
