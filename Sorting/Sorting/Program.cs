using System;
using System.Collections.Generic;

namespace Sorting
{
    internal class Program
    {
        static void Main(string[] args)
        {

            int[] array = new int[5] { 7, 11, 9, 4, 9};

            int[] sorted = SortArray(array);

            for(int i = 0; i < array.Length; i++)
            {
                Console.WriteLine(sorted[i]);
            }

            Console.ReadKey();


        }

        private static int[] SortArray(int[] array)
        {

            //int[] sorted = new int[array.Length];

            //int number = 999999;

            //int lowest = -1;


            //for(int i = 0; i < array.Length; i++)
            //{

            //    for(int j = 0; j < array.Length; j++)
            //    {

            //        if(array[j] < number && array[j] > lowest)
            //        {
            //            number = array[j];
            //        }

            //    }
            //    sorted[i] = number;
            //    lowest = number;                    
            //    number = 999999;

            //}

            for (int j = 0; j < array.Length -1; j++)
            {

                if (array[j] > array[j + 1])
                {
                    int temp = array[j];
                    array[j] = array[j + 1];
                    array[j + 1] = temp;

                    j = -1;
                }
            }
            return array;
        }
    }
}
