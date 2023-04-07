using System;
using System.Collections.Generic;
using System.Linq;
using BC = BCrypt.Net.BCrypt;

namespace PracticeAndReview
{
    internal class Program
    {
        static void Main(string[] args)
        {

            List<int> nums = new List<int>();

            var a = 1;
            var b = 2;
            var c = 3;

            nums.Add(a);
            nums.Add(b);
            nums.Add(c);

            var d = nums.SingleOrDefault(n => n == 3);

            d = 4;

            if(c == 3)
            {
                var e = 5;
            }
            else
            {
                var f = 6;
            }

        }
    }
}
