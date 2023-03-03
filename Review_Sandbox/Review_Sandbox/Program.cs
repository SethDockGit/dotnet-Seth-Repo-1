using System;

namespace Review_Sandbox
{
    internal class Program
    {

        //15.
        //Create a method that takes in two integers, and returns an array with the range of numbers between them
        //Ex: inputs 2, 5  -----> returns an int[] with {2, 3, 4, 5}
        //(Hint- first you need to determine the size of the array; how can you calculate that value based on the two integers provided?)

        static void Main(string[] args)
        {
            int[] rangeDisplay = getRange(5, 10);

            for(int i = 0; i < rangeDisplay.Length; i++)
            {
                Console.Write($"{rangeDisplay[i]}, ");
            }
            Console.ReadKey();
        }
        public static int[] getRange(int one, int two)
        {
            int range = two - one;

            int[] result = new int[range +1];

            for(int i = 0; i < range + 1; i++)
            {
                result[i] = one + i;
            }
            return result;  
        }
    }
}
