using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkTutorial.Models
{
    public class Enrollment
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public string Grade { get; set; }
    }
}
