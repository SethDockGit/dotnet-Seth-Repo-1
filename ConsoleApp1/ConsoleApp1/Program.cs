using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int result = ReturnBiggerNum(1, 2);
            Console.WriteLine($"{result} is bigger or equal");
            Console.ReadLine();
        }
        static void GetAName()
        {
            Console.WriteLine("what is your name?");
            string nametogive = Console.ReadLine();
            Console.WriteLine("hello " + nametogive);
            Console.ReadLine();
        }
        static int NumbersAdded(int numone, int numtwo)
        {
            int result = numone + numtwo;
            return result;
        }
        static int ReturnBiggerNum(int firstnum, int secnum)
        {
            if (firstnum > secnum)
            {
                Console.WriteLine("the first numb was greater cool");
                Console.ReadKey();
                return firstnum;
            }
            else if (secnum > firstnum)
            {
                Console.WriteLine("the second numb was greater cool");
                Console.ReadKey();
                return secnum;
            }
            else
            {
                return firstnum;
            }
        }

    }
}

