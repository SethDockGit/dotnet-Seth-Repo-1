using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int[] myNewArray = new int[] { 1, 2, 3 };

            foreach (int i in myNewArray)
            {
                myNewArray[i] = myNewArray[i] + myNewArray[i];
                Console.WriteLine($"{myNewArray[i]}");
                Console.ReadLine();
            }
        }
    }
}
