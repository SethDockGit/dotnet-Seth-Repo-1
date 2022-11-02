using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grocery_Cart
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Choice myChoice = ReceiveItem();

                if (myChoice == Choice.Invalid)
                {
                    Console.WriteLine("Sorry, we don't have that");
                    Console.ReadKey();
                    Console.Clear();
                }
                else
                {
                    Console.WriteLine($"OK, here is {myChoice}");
                    Console.ReadKey();
                    Console.Clear();
                }
            }

            //use an enum to encode various grocery items

            //provide the user with a list of items in stock (the enum values)

            //prompt them to pick an item and tell them what they picked
        }
            
        public static Choice ReceiveItem()
        {
            Console.WriteLine("In stock today is bananas, stinkfruit, mayostick, and meatlog");
            Console.WriteLine("Please pick your choice");
            string userChoice = Console.ReadLine();

            Choice myChoice = Choice.Invalid;
           
            switch (userChoice)
            {
                case "bananas":
                    myChoice = Choice.Bananas;
                    break;

                case "stinkfruit":
                    myChoice = Choice.Stinkfruit;
                    break;

                case "mayostick":
                    myChoice = Choice.Mayostick;
                    break;

                case "meatlog":
                    myChoice = Choice.Bananas;
                    break;

                default:
                    myChoice = Choice.Invalid;
                    break;
            }
            return myChoice;           
        }
    }
}
