using System;
using System.Security.Cryptography;

namespace InheritanceDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            GermanShepherd germanShepherd = new GermanShepherd(false, "Rufus");

            Console.WriteLine($"I love my dog {germanShepherd.Name}");
            Console.ReadKey();
            Console.WriteLine($"OK {germanShepherd.Name}, I am going to leave this tasty ham on the table");

            while(germanShepherd.IsAGoodBoy)
            {
                Console.WriteLine($"{germanShepherd.Name} sat obediently and did not eat the food off the table");
                Console.ReadKey();
            }
            Console.WriteLine($"{germanShepherd.Name} ate the food. BAD BOY!!!!! >:(");
            Console.ReadKey();


            
        }
    }
}
