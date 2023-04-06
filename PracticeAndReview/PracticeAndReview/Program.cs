using System;
using BC = BCrypt.Net.BCrypt;

namespace PracticeAndReview
{
    internal class Program
    {
        static void Main(string[] args)
        {


            string mypass = "mypass";
            string hashedPass = BC.HashPassword(mypass);
            var a = 1;
            
        }
    }
}
