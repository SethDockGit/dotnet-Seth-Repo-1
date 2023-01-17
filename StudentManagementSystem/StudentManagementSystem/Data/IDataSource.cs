using System;
using System.Collections.Generic;
using System.Text;
using StudentManagementSystem.Models;

namespace StudentManagementSystem.Data
{
    public interface IDataSource
    {
        public List<Student> Students { get; set; }
        public List<Student> GetStudents();
        public void RemoveStudent();
        public void AddStudent();
        List<Course> GetCourses();
    }


}
