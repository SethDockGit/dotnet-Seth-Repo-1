using System;
using System.Collections.Generic;
using System.Text;

namespace Interface_demo
{
    interface IEmployee
    {
        public decimal Salary { get; set; }
        public decimal CurrentSavings { get; set; }


        public void GetPaid();

    }
}
