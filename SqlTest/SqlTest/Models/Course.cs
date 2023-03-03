using System;
using System.Collections.Generic;
using System.Text;

namespace SqlTest.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }
        public string Professor { get; set; }
        public string Description { get; set; }

    }
}
