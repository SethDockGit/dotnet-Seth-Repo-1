using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int[] numbers = new int[5] { 1, 2, 3, 4, 5 };
            int result = AddArray(numbers);
            Console.WriteLine(result);
            Console.ReadLine(); 

        }
        static int AddArray(int[] arrayofnumbers)
        {
            int sum = 0;

            for (int i = 0; i < arrayofnumbers.Length; i++)
            {
                    sum = sum + arrayofnumbers[i];
            }
            return sum;
        }

    }
}
