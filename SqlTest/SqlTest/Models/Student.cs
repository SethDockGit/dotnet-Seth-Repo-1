using System;
using System.Collections.Generic;
using System.Text;

namespace SqlTest.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public List<Course> Courses { get; set; }
    }
}
