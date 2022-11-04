using System;
using System.Collections.Generic;
using System.Text;

namespace Interface_demo
{
    class CEO : IEmployee
    {
        public decimal CurrentSavings { get; set; }
        public decimal Salary { get; set; }

        public CEO(decimal currentSavings)
        {
            CurrentSavings = currentSavings;
            Salary = 74000;
        }
        public void GetPaid()
        {
            CurrentSavings += Salary;
        }
    }
}
