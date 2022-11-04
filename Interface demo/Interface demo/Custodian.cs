using System;
using System.Collections.Generic;
using System.Text;

namespace Interface_demo
{
    class Custodian : IEmployee
    {
        public decimal Salary { get; set; }
        public decimal CurrentSavings  { get; set; }

        public Custodian(decimal currentSavings)
        {
            Salary = 75000;

            CurrentSavings = currentSavings;
        }

        public void GetPaid()
        {
            CurrentSavings += Salary;
        }
    }


}
