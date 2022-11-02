using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Management.Instrumentation;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace more_array
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] resultingArray = PrintTheRange(2, 5);
            for (int i = 0; i < resultingArray.Length; i++)
            {
                Console.Write(resultingArray[i]);
            }
            Console.ReadLine();
        }

        //15.
        //Create a method that takes in two integers, and returns an array with the range of numbers between them
        //Ex: inputs 2, 5  -----> returns an int[] with {2, 3, 4, 5}
        //(Hint- first you need to determine the size of the array; how can you calculate that value based on the two integers provided?)

        public static int[] PrintTheRange(int numOne, int numTwo)
        {
            int length = numTwo - numOne + 1;
            int[] finalArray = new int[length];
            finalArray[0] = numOne;
            for (int i = 1; i < finalArray.Length; i++)
            {
                finalArray[i] = numOne + i;
            }
            return finalArray;
        }
    }
}
