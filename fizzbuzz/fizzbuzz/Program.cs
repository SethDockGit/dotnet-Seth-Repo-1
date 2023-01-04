using System;

namespace fizzbuzz
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Enter any positive whole number then hit enter...");

            string input = Console.ReadLine();

            int total = 0;

            int number = int.Parse(input);

            while (number != 0)
            {
                int digit = number % 10;
                number = number / 10;
                total += digit;
            }


            Console.WriteLine(total);

            Console.ReadKey();
            
        }
    }
}
