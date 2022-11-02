using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace _10_6_22
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = new int[5] { -1, -5, -13, -1351, -5 };

            int result = FindGreatest(numbers);

            Console.WriteLine(result);
            Console.ReadKey();
        }

        public static int FindGreatest(int[] numlist)
        {

            int bignum = numlist[0];

            for (int i = 0; i < numlist.Length; i++)
            {
                if (numlist[i] > bignum)
                {             
                    bignum = numlist[i];
                }
            }
            return bignum;
        }
        

    }
}
