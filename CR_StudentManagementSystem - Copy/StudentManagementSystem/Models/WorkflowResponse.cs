using System;
using System.Collections.Generic;
using System.Text;

namespace StudentManagementSystem.Models
{
    public class WorkflowResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<Student> Students { get; set; }
        public List<Course> Courses { get; set; }
    }
}
