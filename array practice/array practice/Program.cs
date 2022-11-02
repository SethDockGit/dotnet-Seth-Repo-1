using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace array_practice
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = new int[5] { 1, 5, 13, 1351, 5 };

            //HINT:
            numbers[0] = 100;

            //now the array is { 100, 5, 12, 1351, 5 }
            //(we assigned the value at position 0 to be 100)

            int[] result = DoubleNumbers(numbers);

            for (int i = 0; i < result.Length; i++)
            { 
            Console.WriteLine($"{result[i]}");

            Console.ReadLine();
            }
        }

        static int[] DoubleNumbers(int[] nums)
        {

            for (int i = 0; i < nums.Length; i++)
            {
                nums[i] = nums[i] * 2;
            }

            return nums;

            //this method should take in an integer array, double each number within the array, then return the modified array
            //example: input- (2, 4, 6) ... output { 4, 8, 12 }
        }
    }
}
