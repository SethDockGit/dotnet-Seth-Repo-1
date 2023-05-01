using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkTutorial.Models
{
    public class Student
    {
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}
