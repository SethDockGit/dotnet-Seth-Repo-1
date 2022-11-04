using System;
using System.Collections;

namespace Interface_demo
{
    class Program
    {
        static void Main(string[] args)
        {
            IEmployee employee1 = new Custodian(10000);
            IEmployee employee2 = new CEO(2000000);
            EmployeePayPortal(employee1);
        }

        static void EmployeePayPortal(IEmployee employee)
        {
            Console.WriteLine("Hello, welcome to the MoneyGettaz Incorporated portal.");
            Console.WriteLine($"\nYour current balance is {employee.CurrentSavings}. Press Y to get paid");

            var cki = Console.ReadKey(true);

            switch (cki.Key)
            {
                case ConsoleKey.Y:
                    
                    employee.GetPaid();
                    Console.WriteLine($"\nYour new balance is {employee.CurrentSavings}");

                break;

                default:
                    break;
            }
        }

    }
}
