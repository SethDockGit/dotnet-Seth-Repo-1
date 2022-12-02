using System;
using System.Collections.Generic;

namespace Dictionary
{
    internal class Program
    {
        static void Main(string[] args)
        {



            DateTime dateTime = DateTime.Now;

            System.TimeSpan duration = new System.TimeSpan(30, 0, 0, 0);
            System.DateTime plusThirty = dateTime.Add(duration);

            Console.WriteLine($"{plusThirty}");
            Console.ReadKey();



            //Dictionary<string, int> nameAndAge = new Dictionary<string, int>();

            //nameAndAge.Add("Doug", 25);
            //nameAndAge.Add("Sarah", 52);

            //var dougsAge = nameAndAge["Doug"];

            //nameAndAge["Doug"] += 1;

            //var names = nameAndAge.Keys;

            //foreach(var item in nameAndAge)
            //{
            //    Console.WriteLine($"Name: {item.Key}, Age: {item.Value}");
            //}




        }
    }
}
