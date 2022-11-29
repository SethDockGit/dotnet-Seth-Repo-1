using System;
using System.Collections.Generic;

namespace Dictionary
{
    internal class Program
    {
        static void Main(string[] args)
        {


            Dictionary<string, int> nameAndAge = new Dictionary<string, int>();

            nameAndAge.Add("Doug", 25);
            nameAndAge.Add("Sarah", 52);

            var dougsAge = nameAndAge["Doug"];

            nameAndAge["Doug"] += 1;

            var names = nameAndAge.Keys;

            var a = 1;

            nameAndAge.Add("Doug", 40);

            foreach(var item in nameAndAge)
            {
                Console.WriteLine($"Name: {item.Key}, Age: {item.Value}");
            }




        }
    }
}
